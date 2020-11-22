// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using middlerApp.API.Attributes;
using middlerApp.API.Controllers.IdP.Account.ViewModels;
using middlerApp.API.Helper;
using middlerApp.API.Providers;
using middlerApp.IDP.DataAccess.Entities.Models;
using middlerApp.IDP.Library.Services;
using Reflectensions.ExtensionMethods;

namespace middlerApp.API.Controllers.IdP.Account
{
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    [SecurityHeaders]
    [Route("idp/account")]
    [IdPController]
    public class AccountController : Controller
    {
        private readonly ILocalUserService _localUserService;
        private readonly AuthenticationProviderContextService _authenticationProvider;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IUsersService _usersService;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IUsersService usersService,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            ILocalUserService localUserService,
            AuthenticationProviderContextService authenticationProvider)
        {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)
            _localUserService = localUserService
                                ?? throw new ArgumentNullException(nameof(localUserService));
            _authenticationProvider = authenticationProvider;

            _interaction = interaction;
            _clientStore = clientStore;
            _usersService = usersService;
            _schemeProvider = schemeProvider;
            _events = events;
        }

        [HttpGet("silentRefresh")]
        [AllowAnonymous]
        public IActionResult SilentRefresh()
        {

            return Content(
                "<html><body><script>parent.postMessage(location.hash, location.origin);</script></body></html>",
                "text/html");
        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet("login")]
        [GenerateAntiForgeryToken]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // build a model so we know what to show on the login page
            var vm = await BuildLoginViewModelAsync(returnUrl);
            return Ok(vm);
        }

        [HttpPost("login-external")]
        
        public async Task<IActionResult> LoginExternal([FromBody] ExternalLoginModel model)
        {
            var resultmodel = new LoginResultModel();
            resultmodel.ReturnUrl = model.ReturnUrl;

            // we will issue the external cookie and then redirect the
            // user back to the external callback, in essence, treating windows
            // auth the same as any other external authentication mechanism
            var props = new AuthenticationProperties()
            {
                RedirectUri = model.ReturnUrl,
                Items =
                {
                    { "returnUrl", model.ReturnUrl },
                    { "scheme", model.Scheme },
                }
            };

            
            // see if windows auth has already been requested and succeeded
            AuthenticateResult result = await HttpContext.AuthenticateAsync(model.Scheme);
            if (result.Principal != null)
            {
                var authHandler = _authenticationProvider.GetHandler(model.Scheme);

                var factory = authHandler.GetUserFactory(result.Principal);
                
                var subject = factory.GetSubject();

                var mUser = await _localUserService.GetUserBySubjectAsync(subject);
                if (mUser == null)
                {
                    mUser = factory.BuildUser();

                    await _localUserService.AddUserAsync(mUser);
                    
                }
                else
                {
                    factory.UpdateClaims(mUser);
                    await _usersService.UpdateUserAsync(mUser);
                }

                await HttpContext.SignInAsync(new IdentityServerUser(mUser.Subject)
                {
                    DisplayName = mUser.UserName,
                    IdentityProvider = model.Scheme,
                    AuthenticationTime = DateTime.Now
                });

                await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);


                if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Ok(resultmodel.WithStatus(Status.Ok));
                }

                resultmodel.ReturnUrl = "/";
                return Ok(resultmodel.WithStatus(Status.Ok));

            }
            else
            {
                // trigger windows auth
                // since windows auth don't support the redirect uri,
                // this URL is re-triggered when we call challenge
                return Challenge(model.Scheme);
            }
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginInputModel model, string button = "login")
        {
            // check if we are in the context of an authorization request
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            LoginResultModel resultmodel;

            resultmodel = await LoginInternal(context, model);

            if (resultmodel.Status == Status.Ok)
                return Ok(resultmodel);

            foreach (var resultmodelError in resultmodel.Errors)
            {
                ModelState.AddModelError("", resultmodelError.Message);
            }


            //var vm = await BuildLoginViewModelAsync(model.ReturnUrl);

            return Ok(resultmodel);

        }

        private async Task<LoginResultModel> LoginInternal(AuthorizationRequest context, LoginInputModel model)
        {
            var resultmodel = new LoginResultModel();
            resultmodel.ReturnUrl = model.ReturnUrl;

            if (await _localUserService.ValidateCredentialsAsync(model.Username, model.Password))
            {
                var user = await _localUserService.GetUserByUserNameOrEmailAsync(model.Username);
                await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Subject, user.UserName, clientId: context?.Client.ClientId));

                // only set explicit expiration here if user chooses "remember me". 
                // otherwise we rely upon expiration configured in cookie middleware.
                AuthenticationProperties props = null;
                if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                {
                    props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                    };
                };

                // issue authentication cookie with subject ID and username
                var isuser = new IdentityServerUser(user.Subject)
                {
                    DisplayName = user.UserName
                };

                await HttpContext.SignInAsync(isuser, props);

                if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                {
                    return resultmodel.WithStatus(Status.Ok);
                }

                resultmodel.ReturnUrl = "/";
                return resultmodel.WithStatus(Status.Ok);
            }

            return resultmodel.WithStatus(Status.Error).WithError("Invalid login attempt.");
        }


        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet("logout")]
        [GenerateAntiForgeryToken]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await BuildLogoutViewModelAsync(logoutId);

            var resultModel = new LogOutResultModel();
            resultModel.ShowLogoutPrompt = vm.ShowLogoutPrompt;
            resultModel.LogoutId = vm.LogoutId;


            if (vm.ShowLogoutPrompt == false)
            {
                await HttpContext.SignOutAsync();
                // no need to show prompt
                return await Logout(resultModel);
            }

            resultModel.Status = LogOutStatus.Prompt;

            return Ok(resultModel);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout([FromBody]LogOutResultModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Page("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            model.AutomaticRedirectAfterSignOut = vm.AutomaticRedirectAfterSignOut;
            model.ClientName = vm.ClientName;
            model.PostLogoutRedirectUri = vm.PostLogoutRedirectUri;
            model.SignOutIframeUrl = vm.SignOutIframeUrl;
            model.LogoutId = vm.LogoutId;
            model.Status = LogOutStatus.LoggedOut;

            return Ok(model);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return Ok();
        }


        /*****************************************/
        /* helper APIs for the AccountController */
        /*****************************************/
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Username = context?.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.Client.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }
    }
}
