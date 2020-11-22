CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ApiResources" (
        "Id" uuid NOT NULL,
        "Enabled" boolean NOT NULL,
        "Name" character varying(200) NOT NULL,
        "DisplayName" character varying(200) NULL,
        "Description" character varying(1000) NULL,
        "AllowedAccessTokenSigningAlgorithms" character varying(100) NULL,
        "ShowInDiscoveryDocument" boolean NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        "Updated" timestamp without time zone NULL,
        "LastAccessed" timestamp without time zone NULL,
        "NonEditable" boolean NOT NULL,
        CONSTRAINT "PK_ApiResources" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "Clients" (
        "Id" uuid NOT NULL,
        "Enabled" boolean NOT NULL,
        "ClientId" character varying(200) NOT NULL,
        "ProtocolType" character varying(200) NOT NULL,
        "RequireClientSecret" boolean NOT NULL,
        "ClientName" character varying(200) NULL,
        "Description" character varying(1000) NULL,
        "ClientUri" character varying(2000) NULL,
        "LogoUri" character varying(2000) NULL,
        "RequireConsent" boolean NOT NULL,
        "AllowRememberConsent" boolean NOT NULL,
        "AlwaysIncludeUserClaimsInIdToken" boolean NOT NULL,
        "RequirePkce" boolean NOT NULL,
        "AllowPlainTextPkce" boolean NOT NULL,
        "RequireRequestObject" boolean NOT NULL,
        "AllowAccessTokensViaBrowser" boolean NOT NULL,
        "FrontChannelLogoutUri" character varying(2000) NULL,
        "FrontChannelLogoutSessionRequired" boolean NOT NULL,
        "BackChannelLogoutUri" character varying(2000) NULL,
        "BackChannelLogoutSessionRequired" boolean NOT NULL,
        "AllowOfflineAccess" boolean NOT NULL,
        "IdentityTokenLifetime" integer NOT NULL,
        "AllowedIdentityTokenSigningAlgorithms" character varying(100) NULL,
        "AccessTokenLifetime" integer NOT NULL,
        "AuthorizationCodeLifetime" integer NOT NULL,
        "ConsentLifetime" integer NULL,
        "AbsoluteRefreshTokenLifetime" integer NOT NULL,
        "SlidingRefreshTokenLifetime" integer NOT NULL,
        "RefreshTokenUsage" integer NOT NULL,
        "UpdateAccessTokenClaimsOnRefresh" boolean NOT NULL,
        "RefreshTokenExpiration" integer NOT NULL,
        "AccessTokenType" integer NOT NULL,
        "EnableLocalLogin" boolean NOT NULL,
        "IncludeJwtId" boolean NOT NULL,
        "AlwaysSendClientClaims" boolean NOT NULL,
        "ClientClaimsPrefix" character varying(200) NULL,
        "PairWiseSubjectSalt" character varying(200) NULL,
        "Created" timestamp without time zone NOT NULL,
        "Updated" timestamp without time zone NULL,
        "LastAccessed" timestamp without time zone NULL,
        "UserSsoLifetime" integer NULL,
        "UserCodeType" character varying(100) NULL,
        "DeviceCodeLifetime" integer NOT NULL,
        "NonEditable" boolean NOT NULL,
        CONSTRAINT "PK_Clients" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "DeviceFlowCodes" (
        "UserCode" character varying(200) NOT NULL,
        "DeviceCode" character varying(200) NOT NULL,
        "SubjectId" character varying(200) NULL,
        "SessionId" character varying(100) NULL,
        "ClientId" character varying(200) NOT NULL,
        "Description" character varying(200) NULL,
        "CreationTime" timestamp without time zone NOT NULL,
        "Expiration" timestamp without time zone NOT NULL,
        "Data" character varying(50000) NOT NULL,
        CONSTRAINT "PK_DeviceFlowCodes" PRIMARY KEY ("UserCode")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "PersistedGrants" (
        "Key" character varying(200) NOT NULL,
        "Type" character varying(50) NOT NULL,
        "SubjectId" character varying(200) NULL,
        "SessionId" character varying(100) NULL,
        "ClientId" character varying(200) NOT NULL,
        "Description" character varying(200) NULL,
        "CreationTime" timestamp without time zone NOT NULL,
        "Expiration" timestamp without time zone NULL,
        "ConsumedTime" timestamp without time zone NULL,
        "Data" character varying(50000) NOT NULL,
        CONSTRAINT "PK_PersistedGrants" PRIMARY KEY ("Key")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "Roles" (
        "Id" uuid NOT NULL,
        "Name" text NULL,
        "DisplayName" text NULL,
        "Description" text NULL,
        "BuiltIn" boolean NOT NULL,
        CONSTRAINT "PK_Roles" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "Scopes" (
        "Id" uuid NOT NULL,
        "Enabled" boolean NOT NULL,
        "Name" character varying(200) NOT NULL,
        "DisplayName" character varying(200) NULL,
        "Description" character varying(1000) NULL,
        "Required" boolean NOT NULL,
        "Emphasize" boolean NOT NULL,
        "ShowInDiscoveryDocument" boolean NOT NULL,
        "Type" text NULL,
        "Created" timestamp without time zone NOT NULL,
        "Updated" timestamp without time zone NULL,
        "NonEditable" boolean NOT NULL,
        CONSTRAINT "PK_Scopes" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "UserConsents" (
        "Id" uuid NOT NULL,
        "SubjectId" text NULL,
        "ClientId" text NULL,
        "Scopes" text NULL,
        "CreationTime" timestamp without time zone NOT NULL,
        "Expiration" timestamp without time zone NULL,
        CONSTRAINT "PK_UserConsents" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "Users" (
        "Id" uuid NOT NULL,
        "Subject" character varying(200) NOT NULL,
        "UserName" character varying(200) NULL,
        "Email" character varying(200) NULL,
        "FirstName" text NULL,
        "LastName" text NULL,
        "PhoneNumber" text NULL,
        "EmailConfirmed" boolean NOT NULL,
        "PhoneNumberConfirmed" boolean NOT NULL,
        "TwoFactorEnabled" boolean NOT NULL,
        "LockoutEnabled" boolean NOT NULL,
        "ExpiresOn" timestamp without time zone NULL,
        "Password" character varying(200) NULL,
        "Active" boolean NOT NULL,
        "SecurityCode" character varying(200) NULL,
        "SecurityCodeExpirationDate" timestamp without time zone NOT NULL,
        "ConcurrencyStamp" text NULL,
        CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ApiResourceClaims" (
        "Id" uuid NOT NULL,
        "ApiResourceId" uuid NOT NULL,
        "Type" character varying(200) NOT NULL,
        CONSTRAINT "PK_ApiResourceClaims" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ApiResourceClaims_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES "ApiResources" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ApiResourceProperties" (
        "Id" uuid NOT NULL,
        "ApiResourceId" uuid NOT NULL,
        "Key" character varying(250) NOT NULL,
        "Value" character varying(2000) NOT NULL,
        CONSTRAINT "PK_ApiResourceProperties" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ApiResourceProperties_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES "ApiResources" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ApiResourceSecrets" (
        "Id" uuid NOT NULL,
        "ApiResourceId" uuid NOT NULL,
        "Description" character varying(1000) NULL,
        "Value" character varying(4000) NOT NULL,
        "Expiration" timestamp without time zone NULL,
        "Type" character varying(250) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_ApiResourceSecrets" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ApiResourceSecrets_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES "ApiResources" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ClientClaims" (
        "Id" uuid NOT NULL,
        "Type" character varying(250) NOT NULL,
        "Value" character varying(250) NOT NULL,
        "ClientId" uuid NOT NULL,
        CONSTRAINT "PK_ClientClaims" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ClientClaims_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ClientCorsOrigins" (
        "Id" uuid NOT NULL,
        "Origin" character varying(150) NOT NULL,
        "ClientId" uuid NOT NULL,
        CONSTRAINT "PK_ClientCorsOrigins" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ClientCorsOrigins_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ClientGrantTypes" (
        "Id" uuid NOT NULL,
        "GrantType" character varying(250) NOT NULL,
        "ClientId" uuid NOT NULL,
        CONSTRAINT "PK_ClientGrantTypes" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ClientGrantTypes_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ClientIdPRestrictions" (
        "Id" uuid NOT NULL,
        "Provider" character varying(200) NOT NULL,
        "ClientId" uuid NOT NULL,
        CONSTRAINT "PK_ClientIdPRestrictions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ClientIdPRestrictions_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ClientPostLogoutRedirectUris" (
        "Id" uuid NOT NULL,
        "PostLogoutRedirectUri" character varying(2000) NOT NULL,
        "ClientId" uuid NOT NULL,
        CONSTRAINT "PK_ClientPostLogoutRedirectUris" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ClientPostLogoutRedirectUris_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ClientProperties" (
        "Id" uuid NOT NULL,
        "ClientId" uuid NOT NULL,
        "Key" character varying(250) NOT NULL,
        "Value" character varying(2000) NOT NULL,
        CONSTRAINT "PK_ClientProperties" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ClientProperties_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ClientRedirectUris" (
        "Id" uuid NOT NULL,
        "RedirectUri" character varying(2000) NOT NULL,
        "ClientId" uuid NOT NULL,
        CONSTRAINT "PK_ClientRedirectUris" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ClientRedirectUris_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ClientSecrets" (
        "Id" uuid NOT NULL,
        "ClientId" uuid NOT NULL,
        "Description" character varying(2000) NULL,
        "Value" character varying(4000) NOT NULL,
        "Expiration" timestamp without time zone NULL,
        "Type" character varying(250) NOT NULL,
        "Created" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_ClientSecrets" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ClientSecrets_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ApiResourceScope" (
        "ScopeId" uuid NOT NULL,
        "ApiResourceId" uuid NOT NULL,
        CONSTRAINT "PK_ApiResourceScope" PRIMARY KEY ("ScopeId", "ApiResourceId"),
        CONSTRAINT "FK_ApiResourceScope_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES "ApiResources" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_ApiResourceScope_Scopes_ScopeId" FOREIGN KEY ("ScopeId") REFERENCES "Scopes" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ClientScope" (
        "ScopeId" uuid NOT NULL,
        "ClientId" uuid NOT NULL,
        CONSTRAINT "PK_ClientScope" PRIMARY KEY ("ScopeId", "ClientId"),
        CONSTRAINT "FK_ClientScope_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_ClientScope_Scopes_ScopeId" FOREIGN KEY ("ScopeId") REFERENCES "Scopes" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ScopeClaim" (
        "Id" uuid NOT NULL,
        "ScopeId" uuid NOT NULL,
        "Type" character varying(200) NOT NULL,
        CONSTRAINT "PK_ScopeClaim" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ScopeClaim_Scopes_ScopeId" FOREIGN KEY ("ScopeId") REFERENCES "Scopes" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "ScopeProperty" (
        "Id" uuid NOT NULL,
        "ScopeId" uuid NOT NULL,
        "Key" character varying(250) NOT NULL,
        "Value" character varying(2000) NOT NULL,
        CONSTRAINT "PK_ScopeProperty" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ScopeProperty_Scopes_ScopeId" FOREIGN KEY ("ScopeId") REFERENCES "Scopes" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "MRoleMUser" (
        "RolesId" uuid NOT NULL,
        "UsersId" uuid NOT NULL,
        CONSTRAINT "PK_MRoleMUser" PRIMARY KEY ("RolesId", "UsersId"),
        CONSTRAINT "FK_MRoleMUser_Roles_RolesId" FOREIGN KEY ("RolesId") REFERENCES "Roles" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_MRoleMUser_Users_UsersId" FOREIGN KEY ("UsersId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "UserClaims" (
        "Id" uuid NOT NULL,
        "Type" character varying(250) NOT NULL,
        "Value" character varying(250) NOT NULL,
        "ConcurrencyStamp" text NULL,
        "UserId" uuid NOT NULL,
        CONSTRAINT "PK_UserClaims" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_UserClaims_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "UserLogins" (
        "Id" uuid NOT NULL,
        "Provider" character varying(200) NOT NULL,
        "ProviderIdentityKey" character varying(200) NOT NULL,
        "UserId" uuid NOT NULL,
        "ConcurrencyStamp" text NULL,
        CONSTRAINT "PK_UserLogins" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_UserLogins_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE TABLE "UserSecrets" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "Secret" text NOT NULL,
        "UserId" uuid NOT NULL,
        "ConcurrencyStamp" text NULL,
        CONSTRAINT "PK_UserSecrets" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_UserSecrets_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ApiResourceClaims_ApiResourceId" ON "ApiResourceClaims" ("ApiResourceId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ApiResourceProperties_ApiResourceId" ON "ApiResourceProperties" ("ApiResourceId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE UNIQUE INDEX "IX_ApiResources_Name" ON "ApiResources" ("Name");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ApiResourceScope_ApiResourceId" ON "ApiResourceScope" ("ApiResourceId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ApiResourceSecrets_ApiResourceId" ON "ApiResourceSecrets" ("ApiResourceId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ClientClaims_ClientId" ON "ClientClaims" ("ClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ClientCorsOrigins_ClientId" ON "ClientCorsOrigins" ("ClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ClientGrantTypes_ClientId" ON "ClientGrantTypes" ("ClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ClientIdPRestrictions_ClientId" ON "ClientIdPRestrictions" ("ClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ClientPostLogoutRedirectUris_ClientId" ON "ClientPostLogoutRedirectUris" ("ClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ClientProperties_ClientId" ON "ClientProperties" ("ClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ClientRedirectUris_ClientId" ON "ClientRedirectUris" ("ClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE UNIQUE INDEX "IX_Clients_ClientId" ON "Clients" ("ClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ClientScope_ClientId" ON "ClientScope" ("ClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ClientSecrets_ClientId" ON "ClientSecrets" ("ClientId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE UNIQUE INDEX "IX_DeviceFlowCodes_DeviceCode" ON "DeviceFlowCodes" ("DeviceCode");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_DeviceFlowCodes_Expiration" ON "DeviceFlowCodes" ("Expiration");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_MRoleMUser_UsersId" ON "MRoleMUser" ("UsersId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_PersistedGrants_Expiration" ON "PersistedGrants" ("Expiration");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_PersistedGrants_SubjectId_ClientId_Type" ON "PersistedGrants" ("SubjectId", "ClientId", "Type");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_PersistedGrants_SubjectId_SessionId_Type" ON "PersistedGrants" ("SubjectId", "SessionId", "Type");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ScopeClaim_ScopeId" ON "ScopeClaim" ("ScopeId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_ScopeProperty_ScopeId" ON "ScopeProperty" ("ScopeId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE UNIQUE INDEX "IX_Scopes_Name" ON "Scopes" ("Name");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_UserClaims_UserId" ON "UserClaims" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_UserLogins_UserId" ON "UserLogins" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE UNIQUE INDEX "IX_Users_Subject" ON "Users" ("Subject");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE UNIQUE INDEX "IX_Users_UserName" ON "Users" ("UserName");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    CREATE INDEX "IX_UserSecrets_UserId" ON "UserSecrets" ("UserId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154509_Initial') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201026154509_Initial', '5.0.0');
    END IF;
END $$;
COMMIT;

START TRANSACTION;


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201119154549_ClaimsIssuer') THEN
    ALTER TABLE "UserClaims" ADD "Issuer" text NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201119154549_ClaimsIssuer') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201119154549_ClaimsIssuer', '5.0.0');
    END IF;
END $$;
COMMIT;

START TRANSACTION;


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201119175731_AuthenticationProviders') THEN
    CREATE TABLE "AuthenticationProviders" (
        "Id" uuid NOT NULL,
        "Type" integer NOT NULL,
        "Name" text NULL,
        "DisplayName" text NULL,
        "Description" text NULL,
        "Parameters" text NULL,
        CONSTRAINT "PK_AuthenticationProviders" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201119175731_AuthenticationProviders') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201119175731_AuthenticationProviders', '5.0.0');
    END IF;
END $$;
COMMIT;

START TRANSACTION;


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201119190650_AuthenticationProvidersEnable') THEN
    ALTER TABLE "AuthenticationProviders" ADD "Enabled" boolean NOT NULL DEFAULT FALSE;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201119190650_AuthenticationProvidersEnable') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201119190650_AuthenticationProvidersEnable', '5.0.0');
    END IF;
END $$;
COMMIT;

START TRANSACTION;


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201119202652_AuthenticationProvidersType') THEN
    ALTER TABLE "AuthenticationProviders" ALTER COLUMN "Type" TYPE text;
    ALTER TABLE "AuthenticationProviders" ALTER COLUMN "Type" DROP NOT NULL;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201119202652_AuthenticationProvidersType') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201119202652_AuthenticationProvidersType', '5.0.0');
    END IF;
END $$;
COMMIT;

