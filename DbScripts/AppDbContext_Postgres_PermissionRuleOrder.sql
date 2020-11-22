﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154452_Initial') THEN
    CREATE TABLE "EndpointRules" (
        "Id" uuid NOT NULL,
        "Order" numeric NOT NULL,
        "Name" text NULL,
        "Enabled" boolean NOT NULL,
        "Scheme" text NULL,
        "Hostname" text NULL,
        "Path" text NULL,
        "HttpMethods" text NULL,
        CONSTRAINT "PK_EndpointRules" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154452_Initial') THEN
    CREATE TABLE "Variables" (
        "Id" uuid NOT NULL,
        "Parent" text NULL,
        "Name" text NULL,
        "IsFolder" boolean NOT NULL,
        "Extension" text NULL,
        "Content" text NULL,
        "CreatedAt" timestamp without time zone NOT NULL,
        "UpdatedAt" timestamp without time zone NULL,
        CONSTRAINT "PK_Variables" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154452_Initial') THEN
    CREATE TABLE "EndpointActions" (
        "Id" uuid NOT NULL,
        "Order" numeric NOT NULL,
        "EndpointRuleEntityId" uuid NOT NULL,
        "Terminating" boolean NOT NULL,
        "WriteStreamDirect" boolean NOT NULL,
        "Enabled" boolean NOT NULL,
        "ActionType" text NULL,
        "Parameters" text NULL,
        CONSTRAINT "PK_EndpointActions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_EndpointActions_EndpointRules_EndpointRuleEntityId" FOREIGN KEY ("EndpointRuleEntityId") REFERENCES "EndpointRules" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154452_Initial') THEN
    CREATE INDEX "IX_EndpointActions_EndpointRuleEntityId" ON "EndpointActions" ("EndpointRuleEntityId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201026154452_Initial') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201026154452_Initial', '5.0.0');
    END IF;
END $$;
COMMIT;

START TRANSACTION;


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201108190546_AddPermissions') THEN
    CREATE TABLE "EndpointRulePermission" (
        "Id" uuid NOT NULL,
        "PrincipalName" text NULL,
        "Type" text NULL,
        "AccessMode" text NULL,
        "Client" text NULL,
        "SourceAddress" text NULL,
        "EndpointRuleEntityId" uuid NULL,
        CONSTRAINT "PK_EndpointRulePermission" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_EndpointRulePermission_EndpointRules_EndpointRuleEntityId" FOREIGN KEY ("EndpointRuleEntityId") REFERENCES "EndpointRules" ("Id") ON DELETE RESTRICT
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201108190546_AddPermissions') THEN
    CREATE INDEX "IX_EndpointRulePermission_EndpointRuleEntityId" ON "EndpointRulePermission" ("EndpointRuleEntityId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201108190546_AddPermissions') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201108190546_AddPermissions', '5.0.0');
    END IF;
END $$;
COMMIT;

START TRANSACTION;


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201112184536_PermissionRuleOrder') THEN
    ALTER TABLE "EndpointRulePermission" ADD "Order" numeric NOT NULL DEFAULT 0.0;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201112184536_PermissionRuleOrder') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201112184536_PermissionRuleOrder', '5.0.0');
    END IF;
END $$;
COMMIT;

