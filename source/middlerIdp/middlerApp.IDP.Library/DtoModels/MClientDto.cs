﻿using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using middlerApp.IDP.DataAccess.Entities.Entities;
using ClientClaim = middlerApp.IDP.DataAccess.Entities.Entities.ClientClaim;

namespace middlerApp.IDP.Library.DtoModels
{
    public class MClientDto
    {

        public Guid Id { get; set; }
        public bool Enabled { get; set; } = true;
        public string ClientId { get; set; }
        public string ProtocolType { get; set; } = "oidc";
        public List<SecretDto> ClientSecrets { get; set; }
        public bool RequireClientSecret { get; set; } = true;
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public bool RequireConsent { get; set; } = false;
        public bool AllowRememberConsent { get; set; } = true;
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        public List<string> AllowedGrantTypes { get; set; }
        public bool RequirePkce { get; set; } = true;
        public bool AllowPlainTextPkce { get; set; }
        public bool RequireRequestObject { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> PostLogoutRedirectUris { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;
        public string BackChannelLogoutUri { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; } = true;
        public bool AllowOfflineAccess { get; set; }
        public List<MScopeDto> AllowedScopes { get; set; } = new List<MScopeDto>();
        public int IdentityTokenLifetime { get; set; } = 300;
        public string AllowedIdentityTokenSigningAlgorithms { get; set; }
        public int AccessTokenLifetime { get; set; } = 3600;
        public int AuthorizationCodeLifetime { get; set; } = 300;
        public int? ConsentLifetime { get; set; } = null;
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;
        public TokenUsage RefreshTokenUsage { get; set; } = TokenUsage.OneTimeOnly;
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public TokenExpiration RefreshTokenExpiration { get; set; } = TokenExpiration.Absolute;
        public AccessTokenType AccessTokenType { get; set; } = AccessTokenType.Jwt;
        public bool EnableLocalLogin { get; set; } = true;
        public List<string> IdentityProviderRestrictions { get; set; }
        public bool IncludeJwtId { get; set; }
        public List<ClientClaim> Claims { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public string ClientClaimsPrefix { get; set; } = "client_";
        public string PairWiseSubjectSalt { get; set; }
        public List<string> AllowedCorsOrigins { get; set; }
        public List<ClientProperty> Properties { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }
        public int? UserSsoLifetime { get; set; }
        public string UserCodeType { get; set; }
        public int DeviceCodeLifetime { get; set; } = 300;
        public bool NonEditable { get; set; }
    }

}
