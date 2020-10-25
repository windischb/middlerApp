using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using middlerApp.API.Helper;
using middlerApp.API.IDP.DtoModels;
using middlerApp.API.IDP.Models;
using middlerApp.API.IDP.Services;
using middlerApp.API.IDP.Storage.Entities;
using Client = middlerApp.API.IDP.Storage.Entities.Client;

namespace middlerApp.API.IDP
{
    public class DefaultResourcesManager
    {
        public IDPDbContext DbContext { get; }



        public DefaultResourcesManager(IDPDbContext dbContext)
        {
            DbContext = dbContext;
            
        }


        public void EnsureAllResourcesExists()
        {

            EnsureAdminClientExists(null);
        }

        public async Task EnsureAdminClientExists(string redirectUri)
        {

            var adminClient =  DbContext.Clients
                .Include(c => c.RedirectUris)
                .Include(c => c.AllowedCorsOrigins)
                .FirstOrDefault(c => c.Id == IdpDefaultIdentifier.IdpClient);

            if (adminClient != null)
            {
                UpdateAdminClient(adminClient);
                return;
            }


            await EnsureDefaultScopesExists();

            var client = new Client();
            client.Id = IdpDefaultIdentifier.IdpClient;
            client.ClientId = "mAdmin";
            client.ClientName = "middler Admin UI";
            client.Description = "Administration UI for middler & IdentityServer";
            client.Enabled = true;
            client.RequireClientSecret = false;
            client.AllowedGrantTypes = new List<ClientGrantType>
            {
                new ClientGrantType
                {
                    ClientId = client.Id,
                    GrantType = "authorization_code"
                }
            };
            client.AccessTokenType = (int)AccessTokenType.Reference;
            SetUris(client, redirectUri);
            client.AllowedScopes = new List<ClientScope>()
            {
                new ClientScope()
                {
                    ClientId = client.Id,
                    ScopeId = IdpDefaultResources.Scope_OpenID.Id
                },
                new ClientScope()
                {
                    ClientId = client.Id,
                    ScopeId = IdpDefaultResources.Scope_Roles.Id
                },
                new ClientScope()
                {
                    ClientId = client.Id,
                    ScopeId = IdpDefaultResources.Scope_IdentityServerApi.Id
                },
                
            };

            client.AllowOfflineAccess = true;


            await DbContext.Clients.AddAsync(client);
            await DbContext.SaveChangesAsync();

        }

        private void UpdateAdminClient(Client client)
        {

            SetUris(client, null);
            
            DbContext.SaveChanges();

        }


        private void SetUris(Client client, string redirectUri)
        {

            if (!String.IsNullOrWhiteSpace(redirectUri))
            {
                client.RedirectUris.Add(new ClientRedirectUri
                {
                    ClientId = client.Id,
                    RedirectUri = redirectUri
                });

                client.AllowedCorsOrigins.Add(new ClientCorsOrigin
                {
                    ClientId = client.Id,
                    Origin = redirectUri
                });
            }
            else
            {
                SetRedirectUris(client);
                SetCorsUris(client);
            }

            
        }
        private string GenerateIdpRedirectUri()
        {
            var conf = Static.StartUpConfiguration.IdpSettings;
            var idpListenIp = IPAddress.Parse(conf.ListeningIP);
            var isLocalhost = IPAddress.IsLoopback(idpListenIp) || idpListenIp.ToString() == IPAddress.Any.ToString();

            if (isLocalhost)
            {
                return conf.HttpsPort == 443 ? $"https://localhost" : $"https://localhost:{conf.HttpsPort}";
            }
            else
            {
                return conf.HttpsPort == 443
                    ? $"https://{conf.ListeningIP}"
                    : $"https://{conf.ListeningIP}:{conf.HttpsPort}";
            }
        }

        private string GenerateAdminRedirectUri()
        {
            var conf = Static.StartUpConfiguration.AdminSettings;
            var idpListenIp = IPAddress.Parse(conf.ListeningIP);
            var isLocalhost = IPAddress.IsLoopback(idpListenIp) || idpListenIp.ToString() == IPAddress.Any.ToString();

            if (isLocalhost)
            {
                return conf.HttpsPort == 443 ? $"https://localhost" : $"https://localhost:{conf.HttpsPort}";
            }
            else
            {
                return conf.HttpsPort == 443
                    ? $"https://{conf.ListeningIP}"
                    : $"https://{conf.ListeningIP}:{conf.HttpsPort}";
            }
        }

        private void SetRedirectUris(Client client)
        {
            var uris = client.RedirectUris.Select(u => u.RedirectUri).ToList();
            var idpUri = GenerateIdpRedirectUri();

            if (!uris.Contains(idpUri))
            {
                client.RedirectUris.Add(new ClientRedirectUri
                {
                    ClientId = client.Id,
                    RedirectUri = idpUri
                });
            }

            var admUri = GenerateAdminRedirectUri();
            if (!uris.Contains(admUri))
            {
                client.RedirectUris.Add(new ClientRedirectUri
                {
                    ClientId = client.Id,
                    RedirectUri = admUri
                });
            }
        }

        private void SetCorsUris(Client client)
        {
            var uris = client.AllowedCorsOrigins.Select(u => u.Origin).ToList();
            var idpUri = GenerateIdpRedirectUri();

            if (!uris.Contains(idpUri))
            {
                client.AllowedCorsOrigins.Add(new ClientCorsOrigin
                {
                    ClientId = client.Id,
                    Origin = idpUri
                });
            }

            var admUri = GenerateAdminRedirectUri();
            if (!uris.Contains(admUri))
            {
                client.AllowedCorsOrigins.Add(new ClientCorsOrigin
                {
                    ClientId = client.Id,
                    Origin = admUri
                });
            }
        }


        public async Task<MRole> EnsureAdminRoleExists()
        {
            var adminRole = await DbContext
                .Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == IdpDefaultIdentifier.Role_IdentityServer_Administrators);
            if (adminRole == null)
            {
                await DbContext.Roles.AddAsync(IdpDefaultResources.Role_Idp_Administrator);
                await DbContext.SaveChangesAsync();
            }
            else
            {
                return adminRole;
            }

            return await EnsureAdminRoleExists();
        }

       

        public async Task<bool> AtLeastOneAdminUserExistsAsync()
        {
            var adminRole = await EnsureAdminRoleExists();
            return adminRole.Users.Any();

        }

        public async Task EnsureDefaultScopesExists()
        {

            await EnsureScopeExists(IdpDefaultResources.Scope_OpenID);
            await EnsureScopeExists(IdpDefaultResources.Scope_Roles);
            await EnsureScopeExists(IdpDefaultResources.Scope_IdentityServerApi);
            await DbContext.SaveChangesAsync();

        }

        private async Task EnsureScopeExists(Scope scope)
        {
            var foundScope = await DbContext.Scopes.AsQueryable().FirstOrDefaultAsync(s => s.Name == scope.Name);
            if (foundScope == null)
            {
                await DbContext.Scopes.AddAsync(scope);
            }
        }


    }
}
