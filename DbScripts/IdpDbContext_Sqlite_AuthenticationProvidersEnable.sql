CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "ApiResources" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ApiResources" PRIMARY KEY,
    "Enabled" INTEGER NOT NULL,
    "Name" TEXT NOT NULL,
    "DisplayName" TEXT NULL,
    "Description" TEXT NULL,
    "AllowedAccessTokenSigningAlgorithms" TEXT NULL,
    "ShowInDiscoveryDocument" INTEGER NOT NULL,
    "Created" TEXT NOT NULL,
    "Updated" TEXT NULL,
    "LastAccessed" TEXT NULL,
    "NonEditable" INTEGER NOT NULL
);

CREATE TABLE "Clients" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Clients" PRIMARY KEY,
    "Enabled" INTEGER NOT NULL,
    "ClientId" TEXT NOT NULL,
    "ProtocolType" TEXT NOT NULL,
    "RequireClientSecret" INTEGER NOT NULL,
    "ClientName" TEXT NULL,
    "Description" TEXT NULL,
    "ClientUri" TEXT NULL,
    "LogoUri" TEXT NULL,
    "RequireConsent" INTEGER NOT NULL,
    "AllowRememberConsent" INTEGER NOT NULL,
    "AlwaysIncludeUserClaimsInIdToken" INTEGER NOT NULL,
    "RequirePkce" INTEGER NOT NULL,
    "AllowPlainTextPkce" INTEGER NOT NULL,
    "RequireRequestObject" INTEGER NOT NULL,
    "AllowAccessTokensViaBrowser" INTEGER NOT NULL,
    "FrontChannelLogoutUri" TEXT NULL,
    "FrontChannelLogoutSessionRequired" INTEGER NOT NULL,
    "BackChannelLogoutUri" TEXT NULL,
    "BackChannelLogoutSessionRequired" INTEGER NOT NULL,
    "AllowOfflineAccess" INTEGER NOT NULL,
    "IdentityTokenLifetime" INTEGER NOT NULL,
    "AllowedIdentityTokenSigningAlgorithms" TEXT NULL,
    "AccessTokenLifetime" INTEGER NOT NULL,
    "AuthorizationCodeLifetime" INTEGER NOT NULL,
    "ConsentLifetime" INTEGER NULL,
    "AbsoluteRefreshTokenLifetime" INTEGER NOT NULL,
    "SlidingRefreshTokenLifetime" INTEGER NOT NULL,
    "RefreshTokenUsage" INTEGER NOT NULL,
    "UpdateAccessTokenClaimsOnRefresh" INTEGER NOT NULL,
    "RefreshTokenExpiration" INTEGER NOT NULL,
    "AccessTokenType" INTEGER NOT NULL,
    "EnableLocalLogin" INTEGER NOT NULL,
    "IncludeJwtId" INTEGER NOT NULL,
    "AlwaysSendClientClaims" INTEGER NOT NULL,
    "ClientClaimsPrefix" TEXT NULL,
    "PairWiseSubjectSalt" TEXT NULL,
    "Created" TEXT NOT NULL,
    "Updated" TEXT NULL,
    "LastAccessed" TEXT NULL,
    "UserSsoLifetime" INTEGER NULL,
    "UserCodeType" TEXT NULL,
    "DeviceCodeLifetime" INTEGER NOT NULL,
    "NonEditable" INTEGER NOT NULL
);

CREATE TABLE "DeviceFlowCodes" (
    "UserCode" TEXT NOT NULL CONSTRAINT "PK_DeviceFlowCodes" PRIMARY KEY,
    "DeviceCode" TEXT NOT NULL,
    "SubjectId" TEXT NULL,
    "SessionId" TEXT NULL,
    "ClientId" TEXT NOT NULL,
    "Description" TEXT NULL,
    "CreationTime" TEXT NOT NULL,
    "Expiration" TEXT NOT NULL,
    "Data" TEXT NOT NULL
);

CREATE TABLE "PersistedGrants" (
    "Key" TEXT NOT NULL CONSTRAINT "PK_PersistedGrants" PRIMARY KEY,
    "Type" TEXT NOT NULL,
    "SubjectId" TEXT NULL,
    "SessionId" TEXT NULL,
    "ClientId" TEXT NOT NULL,
    "Description" TEXT NULL,
    "CreationTime" TEXT NOT NULL,
    "Expiration" TEXT NULL,
    "ConsumedTime" TEXT NULL,
    "Data" TEXT NOT NULL
);

CREATE TABLE "Roles" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Roles" PRIMARY KEY,
    "Name" TEXT NULL,
    "DisplayName" TEXT NULL,
    "Description" TEXT NULL,
    "BuiltIn" INTEGER NOT NULL
);

CREATE TABLE "Scopes" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Scopes" PRIMARY KEY,
    "Enabled" INTEGER NOT NULL,
    "Name" TEXT NOT NULL,
    "DisplayName" TEXT NULL,
    "Description" TEXT NULL,
    "Required" INTEGER NOT NULL,
    "Emphasize" INTEGER NOT NULL,
    "ShowInDiscoveryDocument" INTEGER NOT NULL,
    "Type" TEXT NULL,
    "Created" TEXT NOT NULL,
    "Updated" TEXT NULL,
    "NonEditable" INTEGER NOT NULL
);

CREATE TABLE "UserConsents" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_UserConsents" PRIMARY KEY,
    "SubjectId" TEXT NULL,
    "ClientId" TEXT NULL,
    "Scopes" TEXT NULL,
    "CreationTime" TEXT NOT NULL,
    "Expiration" TEXT NULL
);

CREATE TABLE "Users" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY,
    "Subject" TEXT NOT NULL,
    "UserName" TEXT NULL,
    "Email" TEXT NULL,
    "FirstName" TEXT NULL,
    "LastName" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "ExpiresOn" TEXT NULL,
    "Password" TEXT NULL,
    "Active" INTEGER NOT NULL,
    "SecurityCode" TEXT NULL,
    "SecurityCodeExpirationDate" TEXT NOT NULL,
    "ConcurrencyStamp" TEXT NULL
);

CREATE TABLE "ApiResourceClaims" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ApiResourceClaims" PRIMARY KEY,
    "ApiResourceId" TEXT NOT NULL,
    "Type" TEXT NOT NULL,
    CONSTRAINT "FK_ApiResourceClaims_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES "ApiResources" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ApiResourceProperties" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ApiResourceProperties" PRIMARY KEY,
    "ApiResourceId" TEXT NOT NULL,
    "Key" TEXT NOT NULL,
    "Value" TEXT NOT NULL,
    CONSTRAINT "FK_ApiResourceProperties_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES "ApiResources" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ApiResourceSecrets" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ApiResourceSecrets" PRIMARY KEY,
    "ApiResourceId" TEXT NOT NULL,
    "Description" TEXT NULL,
    "Value" TEXT NOT NULL,
    "Expiration" TEXT NULL,
    "Type" TEXT NOT NULL,
    "Created" TEXT NOT NULL,
    CONSTRAINT "FK_ApiResourceSecrets_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES "ApiResources" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ClientClaims" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ClientClaims" PRIMARY KEY,
    "Type" TEXT NOT NULL,
    "Value" TEXT NOT NULL,
    "ClientId" TEXT NOT NULL,
    CONSTRAINT "FK_ClientClaims_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ClientCorsOrigins" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ClientCorsOrigins" PRIMARY KEY,
    "Origin" TEXT NOT NULL,
    "ClientId" TEXT NOT NULL,
    CONSTRAINT "FK_ClientCorsOrigins_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ClientGrantTypes" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ClientGrantTypes" PRIMARY KEY,
    "GrantType" TEXT NOT NULL,
    "ClientId" TEXT NOT NULL,
    CONSTRAINT "FK_ClientGrantTypes_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ClientIdPRestrictions" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ClientIdPRestrictions" PRIMARY KEY,
    "Provider" TEXT NOT NULL,
    "ClientId" TEXT NOT NULL,
    CONSTRAINT "FK_ClientIdPRestrictions_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ClientPostLogoutRedirectUris" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ClientPostLogoutRedirectUris" PRIMARY KEY,
    "PostLogoutRedirectUri" TEXT NOT NULL,
    "ClientId" TEXT NOT NULL,
    CONSTRAINT "FK_ClientPostLogoutRedirectUris_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ClientProperties" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ClientProperties" PRIMARY KEY,
    "ClientId" TEXT NOT NULL,
    "Key" TEXT NOT NULL,
    "Value" TEXT NOT NULL,
    CONSTRAINT "FK_ClientProperties_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ClientRedirectUris" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ClientRedirectUris" PRIMARY KEY,
    "RedirectUri" TEXT NOT NULL,
    "ClientId" TEXT NOT NULL,
    CONSTRAINT "FK_ClientRedirectUris_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ClientSecrets" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ClientSecrets" PRIMARY KEY,
    "ClientId" TEXT NOT NULL,
    "Description" TEXT NULL,
    "Value" TEXT NOT NULL,
    "Expiration" TEXT NULL,
    "Type" TEXT NOT NULL,
    "Created" TEXT NOT NULL,
    CONSTRAINT "FK_ClientSecrets_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ApiResourceScope" (
    "ScopeId" TEXT NOT NULL,
    "ApiResourceId" TEXT NOT NULL,
    CONSTRAINT "PK_ApiResourceScope" PRIMARY KEY ("ScopeId", "ApiResourceId"),
    CONSTRAINT "FK_ApiResourceScope_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES "ApiResources" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ApiResourceScope_Scopes_ScopeId" FOREIGN KEY ("ScopeId") REFERENCES "Scopes" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ClientScope" (
    "ScopeId" TEXT NOT NULL,
    "ClientId" TEXT NOT NULL,
    CONSTRAINT "PK_ClientScope" PRIMARY KEY ("ScopeId", "ClientId"),
    CONSTRAINT "FK_ClientScope_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ClientScope_Scopes_ScopeId" FOREIGN KEY ("ScopeId") REFERENCES "Scopes" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ScopeClaim" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ScopeClaim" PRIMARY KEY,
    "ScopeId" TEXT NOT NULL,
    "Type" TEXT NOT NULL,
    CONSTRAINT "FK_ScopeClaim_Scopes_ScopeId" FOREIGN KEY ("ScopeId") REFERENCES "Scopes" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ScopeProperty" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_ScopeProperty" PRIMARY KEY,
    "ScopeId" TEXT NOT NULL,
    "Key" TEXT NOT NULL,
    "Value" TEXT NOT NULL,
    CONSTRAINT "FK_ScopeProperty_Scopes_ScopeId" FOREIGN KEY ("ScopeId") REFERENCES "Scopes" ("Id") ON DELETE CASCADE
);

CREATE TABLE "MRoleMUser" (
    "RolesId" TEXT NOT NULL,
    "UsersId" TEXT NOT NULL,
    CONSTRAINT "PK_MRoleMUser" PRIMARY KEY ("RolesId", "UsersId"),
    CONSTRAINT "FK_MRoleMUser_Roles_RolesId" FOREIGN KEY ("RolesId") REFERENCES "Roles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_MRoleMUser_Users_UsersId" FOREIGN KEY ("UsersId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "UserClaims" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_UserClaims" PRIMARY KEY,
    "Type" TEXT NOT NULL,
    "Value" TEXT NOT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "FK_UserClaims_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "UserLogins" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_UserLogins" PRIMARY KEY,
    "Provider" TEXT NOT NULL,
    "ProviderIdentityKey" TEXT NOT NULL,
    "UserId" TEXT NOT NULL,
    "ConcurrencyStamp" TEXT NULL,
    CONSTRAINT "FK_UserLogins_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "UserSecrets" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_UserSecrets" PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "Secret" TEXT NOT NULL,
    "UserId" TEXT NOT NULL,
    "ConcurrencyStamp" TEXT NULL,
    CONSTRAINT "FK_UserSecrets_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_ApiResourceClaims_ApiResourceId" ON "ApiResourceClaims" ("ApiResourceId");

CREATE INDEX "IX_ApiResourceProperties_ApiResourceId" ON "ApiResourceProperties" ("ApiResourceId");

CREATE UNIQUE INDEX "IX_ApiResources_Name" ON "ApiResources" ("Name");

CREATE INDEX "IX_ApiResourceScope_ApiResourceId" ON "ApiResourceScope" ("ApiResourceId");

CREATE INDEX "IX_ApiResourceSecrets_ApiResourceId" ON "ApiResourceSecrets" ("ApiResourceId");

CREATE INDEX "IX_ClientClaims_ClientId" ON "ClientClaims" ("ClientId");

CREATE INDEX "IX_ClientCorsOrigins_ClientId" ON "ClientCorsOrigins" ("ClientId");

CREATE INDEX "IX_ClientGrantTypes_ClientId" ON "ClientGrantTypes" ("ClientId");

CREATE INDEX "IX_ClientIdPRestrictions_ClientId" ON "ClientIdPRestrictions" ("ClientId");

CREATE INDEX "IX_ClientPostLogoutRedirectUris_ClientId" ON "ClientPostLogoutRedirectUris" ("ClientId");

CREATE INDEX "IX_ClientProperties_ClientId" ON "ClientProperties" ("ClientId");

CREATE INDEX "IX_ClientRedirectUris_ClientId" ON "ClientRedirectUris" ("ClientId");

CREATE UNIQUE INDEX "IX_Clients_ClientId" ON "Clients" ("ClientId");

CREATE INDEX "IX_ClientScope_ClientId" ON "ClientScope" ("ClientId");

CREATE INDEX "IX_ClientSecrets_ClientId" ON "ClientSecrets" ("ClientId");

CREATE UNIQUE INDEX "IX_DeviceFlowCodes_DeviceCode" ON "DeviceFlowCodes" ("DeviceCode");

CREATE INDEX "IX_DeviceFlowCodes_Expiration" ON "DeviceFlowCodes" ("Expiration");

CREATE INDEX "IX_MRoleMUser_UsersId" ON "MRoleMUser" ("UsersId");

CREATE INDEX "IX_PersistedGrants_Expiration" ON "PersistedGrants" ("Expiration");

CREATE INDEX "IX_PersistedGrants_SubjectId_ClientId_Type" ON "PersistedGrants" ("SubjectId", "ClientId", "Type");

CREATE INDEX "IX_PersistedGrants_SubjectId_SessionId_Type" ON "PersistedGrants" ("SubjectId", "SessionId", "Type");

CREATE INDEX "IX_ScopeClaim_ScopeId" ON "ScopeClaim" ("ScopeId");

CREATE INDEX "IX_ScopeProperty_ScopeId" ON "ScopeProperty" ("ScopeId");

CREATE UNIQUE INDEX "IX_Scopes_Name" ON "Scopes" ("Name");

CREATE INDEX "IX_UserClaims_UserId" ON "UserClaims" ("UserId");

CREATE INDEX "IX_UserLogins_UserId" ON "UserLogins" ("UserId");

CREATE UNIQUE INDEX "IX_Users_Subject" ON "Users" ("Subject");

CREATE UNIQUE INDEX "IX_Users_UserName" ON "Users" ("UserName");

CREATE INDEX "IX_UserSecrets_UserId" ON "UserSecrets" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20201026154503_Initial', '5.0.0');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "UserClaims" ADD "Issuer" TEXT NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20201119154543_ClaimsIssuer', '5.0.0');

COMMIT;

BEGIN TRANSACTION;

CREATE TABLE "AuthenticationProviders" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AuthenticationProviders" PRIMARY KEY,
    "Type" INTEGER NOT NULL,
    "Name" TEXT NULL,
    "DisplayName" TEXT NULL,
    "Description" TEXT NULL,
    "Parameters" TEXT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20201119175725_AuthenticationProviders', '5.0.0');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "AuthenticationProviders" ADD "Enabled" INTEGER NOT NULL DEFAULT 0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20201119190645_AuthenticationProvidersEnable', '5.0.0');

COMMIT;

