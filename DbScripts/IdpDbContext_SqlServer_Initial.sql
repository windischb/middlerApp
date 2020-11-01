IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ApiResources] (
        [Id] uniqueidentifier NOT NULL,
        [Enabled] bit NOT NULL,
        [Name] nvarchar(200) NOT NULL,
        [DisplayName] nvarchar(200) NULL,
        [Description] nvarchar(1000) NULL,
        [AllowedAccessTokenSigningAlgorithms] nvarchar(100) NULL,
        [ShowInDiscoveryDocument] bit NOT NULL,
        [Created] datetime2 NOT NULL,
        [Updated] datetime2 NULL,
        [LastAccessed] datetime2 NULL,
        [NonEditable] bit NOT NULL,
        CONSTRAINT [PK_ApiResources] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [Clients] (
        [Id] uniqueidentifier NOT NULL,
        [Enabled] bit NOT NULL,
        [ClientId] nvarchar(200) NOT NULL,
        [ProtocolType] nvarchar(200) NOT NULL,
        [RequireClientSecret] bit NOT NULL,
        [ClientName] nvarchar(200) NULL,
        [Description] nvarchar(1000) NULL,
        [ClientUri] nvarchar(2000) NULL,
        [LogoUri] nvarchar(2000) NULL,
        [RequireConsent] bit NOT NULL,
        [AllowRememberConsent] bit NOT NULL,
        [AlwaysIncludeUserClaimsInIdToken] bit NOT NULL,
        [RequirePkce] bit NOT NULL,
        [AllowPlainTextPkce] bit NOT NULL,
        [RequireRequestObject] bit NOT NULL,
        [AllowAccessTokensViaBrowser] bit NOT NULL,
        [FrontChannelLogoutUri] nvarchar(2000) NULL,
        [FrontChannelLogoutSessionRequired] bit NOT NULL,
        [BackChannelLogoutUri] nvarchar(2000) NULL,
        [BackChannelLogoutSessionRequired] bit NOT NULL,
        [AllowOfflineAccess] bit NOT NULL,
        [IdentityTokenLifetime] int NOT NULL,
        [AllowedIdentityTokenSigningAlgorithms] nvarchar(100) NULL,
        [AccessTokenLifetime] int NOT NULL,
        [AuthorizationCodeLifetime] int NOT NULL,
        [ConsentLifetime] int NULL,
        [AbsoluteRefreshTokenLifetime] int NOT NULL,
        [SlidingRefreshTokenLifetime] int NOT NULL,
        [RefreshTokenUsage] int NOT NULL,
        [UpdateAccessTokenClaimsOnRefresh] bit NOT NULL,
        [RefreshTokenExpiration] int NOT NULL,
        [AccessTokenType] int NOT NULL,
        [EnableLocalLogin] bit NOT NULL,
        [IncludeJwtId] bit NOT NULL,
        [AlwaysSendClientClaims] bit NOT NULL,
        [ClientClaimsPrefix] nvarchar(200) NULL,
        [PairWiseSubjectSalt] nvarchar(200) NULL,
        [Created] datetime2 NOT NULL,
        [Updated] datetime2 NULL,
        [LastAccessed] datetime2 NULL,
        [UserSsoLifetime] int NULL,
        [UserCodeType] nvarchar(100) NULL,
        [DeviceCodeLifetime] int NOT NULL,
        [NonEditable] bit NOT NULL,
        CONSTRAINT [PK_Clients] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [DeviceFlowCodes] (
        [UserCode] nvarchar(200) NOT NULL,
        [DeviceCode] nvarchar(200) NOT NULL,
        [SubjectId] nvarchar(200) NULL,
        [SessionId] nvarchar(100) NULL,
        [ClientId] nvarchar(200) NOT NULL,
        [Description] nvarchar(200) NULL,
        [CreationTime] datetime2 NOT NULL,
        [Expiration] datetime2 NOT NULL,
        [Data] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_DeviceFlowCodes] PRIMARY KEY ([UserCode])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [PersistedGrants] (
        [Key] nvarchar(200) NOT NULL,
        [Type] nvarchar(50) NOT NULL,
        [SubjectId] nvarchar(200) NULL,
        [SessionId] nvarchar(100) NULL,
        [ClientId] nvarchar(200) NOT NULL,
        [Description] nvarchar(200) NULL,
        [CreationTime] datetime2 NOT NULL,
        [Expiration] datetime2 NULL,
        [ConsumedTime] datetime2 NULL,
        [Data] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_PersistedGrants] PRIMARY KEY ([Key])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [Roles] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NULL,
        [DisplayName] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [BuiltIn] bit NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [Scopes] (
        [Id] uniqueidentifier NOT NULL,
        [Enabled] bit NOT NULL,
        [Name] nvarchar(200) NOT NULL,
        [DisplayName] nvarchar(200) NULL,
        [Description] nvarchar(1000) NULL,
        [Required] bit NOT NULL,
        [Emphasize] bit NOT NULL,
        [ShowInDiscoveryDocument] bit NOT NULL,
        [Type] nvarchar(max) NULL,
        [Created] datetime2 NOT NULL,
        [Updated] datetime2 NULL,
        [NonEditable] bit NOT NULL,
        CONSTRAINT [PK_Scopes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [UserConsents] (
        [Id] uniqueidentifier NOT NULL,
        [SubjectId] nvarchar(max) NULL,
        [ClientId] nvarchar(max) NULL,
        [Scopes] nvarchar(max) NULL,
        [CreationTime] datetime2 NOT NULL,
        [Expiration] datetime2 NULL,
        CONSTRAINT [PK_UserConsents] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [Subject] nvarchar(200) NOT NULL,
        [UserName] nvarchar(200) NULL,
        [Email] nvarchar(200) NULL,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnabled] bit NOT NULL,
        [ExpiresOn] datetime2 NULL,
        [Password] nvarchar(200) NULL,
        [Active] bit NOT NULL,
        [SecurityCode] nvarchar(200) NULL,
        [SecurityCodeExpirationDate] datetime2 NOT NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ApiResourceClaims] (
        [Id] uniqueidentifier NOT NULL,
        [ApiResourceId] uniqueidentifier NOT NULL,
        [Type] nvarchar(200) NOT NULL,
        CONSTRAINT [PK_ApiResourceClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ApiResourceClaims_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [ApiResources] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ApiResourceProperties] (
        [Id] uniqueidentifier NOT NULL,
        [ApiResourceId] uniqueidentifier NOT NULL,
        [Key] nvarchar(250) NOT NULL,
        [Value] nvarchar(2000) NOT NULL,
        CONSTRAINT [PK_ApiResourceProperties] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ApiResourceProperties_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [ApiResources] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ApiResourceSecrets] (
        [Id] uniqueidentifier NOT NULL,
        [ApiResourceId] uniqueidentifier NOT NULL,
        [Description] nvarchar(1000) NULL,
        [Value] nvarchar(4000) NOT NULL,
        [Expiration] datetime2 NULL,
        [Type] nvarchar(250) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_ApiResourceSecrets] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ApiResourceSecrets_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [ApiResources] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ClientClaims] (
        [Id] uniqueidentifier NOT NULL,
        [Type] nvarchar(250) NOT NULL,
        [Value] nvarchar(250) NOT NULL,
        [ClientId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ClientClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ClientClaims_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ClientCorsOrigins] (
        [Id] uniqueidentifier NOT NULL,
        [Origin] nvarchar(150) NOT NULL,
        [ClientId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ClientCorsOrigins] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ClientCorsOrigins_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ClientGrantTypes] (
        [Id] uniqueidentifier NOT NULL,
        [GrantType] nvarchar(250) NOT NULL,
        [ClientId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ClientGrantTypes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ClientGrantTypes_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ClientIdPRestrictions] (
        [Id] uniqueidentifier NOT NULL,
        [Provider] nvarchar(200) NOT NULL,
        [ClientId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ClientIdPRestrictions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ClientIdPRestrictions_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ClientPostLogoutRedirectUris] (
        [Id] uniqueidentifier NOT NULL,
        [PostLogoutRedirectUri] nvarchar(2000) NOT NULL,
        [ClientId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ClientPostLogoutRedirectUris] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ClientPostLogoutRedirectUris_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ClientProperties] (
        [Id] uniqueidentifier NOT NULL,
        [ClientId] uniqueidentifier NOT NULL,
        [Key] nvarchar(250) NOT NULL,
        [Value] nvarchar(2000) NOT NULL,
        CONSTRAINT [PK_ClientProperties] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ClientProperties_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ClientRedirectUris] (
        [Id] uniqueidentifier NOT NULL,
        [RedirectUri] nvarchar(2000) NOT NULL,
        [ClientId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ClientRedirectUris] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ClientRedirectUris_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ClientSecrets] (
        [Id] uniqueidentifier NOT NULL,
        [ClientId] uniqueidentifier NOT NULL,
        [Description] nvarchar(2000) NULL,
        [Value] nvarchar(4000) NOT NULL,
        [Expiration] datetime2 NULL,
        [Type] nvarchar(250) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_ClientSecrets] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ClientSecrets_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ApiResourceScope] (
        [ScopeId] uniqueidentifier NOT NULL,
        [ApiResourceId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ApiResourceScope] PRIMARY KEY ([ScopeId], [ApiResourceId]),
        CONSTRAINT [FK_ApiResourceScope_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [ApiResources] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ApiResourceScope_Scopes_ScopeId] FOREIGN KEY ([ScopeId]) REFERENCES [Scopes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ClientScope] (
        [ScopeId] uniqueidentifier NOT NULL,
        [ClientId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ClientScope] PRIMARY KEY ([ScopeId], [ClientId]),
        CONSTRAINT [FK_ClientScope_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ClientScope_Scopes_ScopeId] FOREIGN KEY ([ScopeId]) REFERENCES [Scopes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ScopeClaim] (
        [Id] uniqueidentifier NOT NULL,
        [ScopeId] uniqueidentifier NOT NULL,
        [Type] nvarchar(200) NOT NULL,
        CONSTRAINT [PK_ScopeClaim] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ScopeClaim_Scopes_ScopeId] FOREIGN KEY ([ScopeId]) REFERENCES [Scopes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [ScopeProperty] (
        [Id] uniqueidentifier NOT NULL,
        [ScopeId] uniqueidentifier NOT NULL,
        [Key] nvarchar(250) NOT NULL,
        [Value] nvarchar(2000) NOT NULL,
        CONSTRAINT [PK_ScopeProperty] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ScopeProperty_Scopes_ScopeId] FOREIGN KEY ([ScopeId]) REFERENCES [Scopes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [MRoleMUser] (
        [RolesId] uniqueidentifier NOT NULL,
        [UsersId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_MRoleMUser] PRIMARY KEY ([RolesId], [UsersId]),
        CONSTRAINT [FK_MRoleMUser_Roles_RolesId] FOREIGN KEY ([RolesId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MRoleMUser_Users_UsersId] FOREIGN KEY ([UsersId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [UserClaims] (
        [Id] uniqueidentifier NOT NULL,
        [Type] nvarchar(250) NOT NULL,
        [Value] nvarchar(250) NOT NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [UserLogins] (
        [Id] uniqueidentifier NOT NULL,
        [Provider] nvarchar(200) NOT NULL,
        [ProviderIdentityKey] nvarchar(200) NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_UserLogins] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE TABLE [UserSecrets] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Secret] nvarchar(max) NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_UserSecrets] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserSecrets_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ApiResourceClaims_ApiResourceId] ON [ApiResourceClaims] ([ApiResourceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ApiResourceProperties_ApiResourceId] ON [ApiResourceProperties] ([ApiResourceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE UNIQUE INDEX [IX_ApiResources_Name] ON [ApiResources] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ApiResourceScope_ApiResourceId] ON [ApiResourceScope] ([ApiResourceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ApiResourceSecrets_ApiResourceId] ON [ApiResourceSecrets] ([ApiResourceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ClientClaims_ClientId] ON [ClientClaims] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ClientCorsOrigins_ClientId] ON [ClientCorsOrigins] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ClientGrantTypes_ClientId] ON [ClientGrantTypes] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ClientIdPRestrictions_ClientId] ON [ClientIdPRestrictions] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ClientPostLogoutRedirectUris_ClientId] ON [ClientPostLogoutRedirectUris] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ClientProperties_ClientId] ON [ClientProperties] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ClientRedirectUris_ClientId] ON [ClientRedirectUris] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE UNIQUE INDEX [IX_Clients_ClientId] ON [Clients] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ClientScope_ClientId] ON [ClientScope] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ClientSecrets_ClientId] ON [ClientSecrets] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE UNIQUE INDEX [IX_DeviceFlowCodes_DeviceCode] ON [DeviceFlowCodes] ([DeviceCode]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_DeviceFlowCodes_Expiration] ON [DeviceFlowCodes] ([Expiration]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_MRoleMUser_UsersId] ON [MRoleMUser] ([UsersId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_PersistedGrants_Expiration] ON [PersistedGrants] ([Expiration]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_PersistedGrants_SubjectId_ClientId_Type] ON [PersistedGrants] ([SubjectId], [ClientId], [Type]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_PersistedGrants_SubjectId_SessionId_Type] ON [PersistedGrants] ([SubjectId], [SessionId], [Type]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ScopeClaim_ScopeId] ON [ScopeClaim] ([ScopeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_ScopeProperty_ScopeId] ON [ScopeProperty] ([ScopeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE UNIQUE INDEX [IX_Scopes_Name] ON [Scopes] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_UserClaims_UserId] ON [UserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_UserLogins_UserId] ON [UserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE UNIQUE INDEX [IX_Users_Subject] ON [Users] ([Subject]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Users_UserName] ON [Users] ([UserName]) WHERE [UserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    CREATE INDEX [IX_UserSecrets_UserId] ON [UserSecrets] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201026154514_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201026154514_Initial', N'5.0.0-rc.2.20475.6');
END;
GO

COMMIT;
GO

