CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "EndpointRules" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_EndpointRules" PRIMARY KEY,
    "Order" TEXT NOT NULL,
    "Name" TEXT NULL,
    "Enabled" INTEGER NOT NULL,
    "Scheme" TEXT NULL,
    "Hostname" TEXT NULL,
    "Path" TEXT NULL,
    "HttpMethods" TEXT NULL
);

CREATE TABLE "Variables" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Variables" PRIMARY KEY,
    "Parent" TEXT NULL,
    "Name" TEXT NULL,
    "IsFolder" INTEGER NOT NULL,
    "Extension" TEXT NULL,
    "Content" TEXT NULL,
    "CreatedAt" TEXT NOT NULL,
    "UpdatedAt" TEXT NULL
);

CREATE TABLE "EndpointActions" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_EndpointActions" PRIMARY KEY,
    "Order" TEXT NOT NULL,
    "EndpointRuleEntityId" TEXT NOT NULL,
    "Terminating" INTEGER NOT NULL,
    "WriteStreamDirect" INTEGER NOT NULL,
    "Enabled" INTEGER NOT NULL,
    "ActionType" TEXT NULL,
    "Parameters" TEXT NULL,
    CONSTRAINT "FK_EndpointActions_EndpointRules_EndpointRuleEntityId" FOREIGN KEY ("EndpointRuleEntityId") REFERENCES "EndpointRules" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_EndpointActions_EndpointRuleEntityId" ON "EndpointActions" ("EndpointRuleEntityId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20201026154447_Initial', '5.0.0');

COMMIT;

BEGIN TRANSACTION;

CREATE TABLE "EndpointRulePermission" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_EndpointRulePermission" PRIMARY KEY,
    "PrincipalName" TEXT NULL,
    "Type" TEXT NULL,
    "AccessMode" TEXT NULL,
    "Client" TEXT NULL,
    "SourceAddress" TEXT NULL,
    "EndpointRuleEntityId" TEXT NULL,
    CONSTRAINT "FK_EndpointRulePermission_EndpointRules_EndpointRuleEntityId" FOREIGN KEY ("EndpointRuleEntityId") REFERENCES "EndpointRules" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_EndpointRulePermission_EndpointRuleEntityId" ON "EndpointRulePermission" ("EndpointRuleEntityId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20201108190540_AddPermissions', '5.0.0');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "EndpointRulePermission" ADD "Order" TEXT NOT NULL DEFAULT '0.0';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20201112184531_PermissionRuleOrder', '5.0.0');

COMMIT;

