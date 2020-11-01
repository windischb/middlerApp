﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace middlerApp.IDP.Library.Services
{
    public class MCustomTokenValidator: DefaultCustomTokenValidator
    {
        protected ILocalUserService LocalUserService { get; }

        public MCustomTokenValidator(ILocalUserService localUserService)
        {
            LocalUserService = localUserService;
        }

        public override Task<TokenValidationResult> ValidateIdentityTokenAsync(TokenValidationResult result)
        {
            return base.ValidateIdentityTokenAsync(result);
        }

        public override async Task<TokenValidationResult> ValidateAccessTokenAsync(TokenValidationResult result)
        {

            var subjectId = result.ReferenceToken.SubjectId;
            var user = await LocalUserService.GetUserBySubjectAsync(subjectId);


            var tempClaims = result.Claims.Where(c => c.Type != "role").ToList();
            foreach (var role in user.Roles)
            {
                tempClaims.Add(new Claim("role", role.Name));
            }

            foreach (var roleClaim in user.Claims.Where(c => c.Type == "role"))
            {
                tempClaims.Add(new Claim("role", roleClaim.Value));
            }

            

            result.Claims = tempClaims;
            return result;
        }
    }
}