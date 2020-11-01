

[CmdletBinding()]
param (
    [Parameter()]
    [string]$Name,

    [Parameter()]
    [string]$Context
)


if ($Context -eq "App") {
    # AppDbContext

    dotnet ef migrations script `
        --context APPDbContext `
        --project source\middlerCore\middlerApp.Core.DataAccess.Sqlite `
        --startup-project source\middlerCore\middlerApp.Core.MigrationBuilder `
        --configuration sqlite `
        --output "DbScripts\AppDbContext_Sqlite_$($Name).sql"

    dotnet ef migrations script `
        --context APPDbContext `
        --project source\middlerCore\middlerApp.Core.DataAccess.Postgres `
        --startup-project source\middlerCore\middlerApp.Core.MigrationBuilder `
        --configuration postgres `
        --output "DbScripts\AppDbContext_Postgres_$($Name).sql" `
        --idempotent

    dotnet ef migrations script `
        --context APPDbContext `
        --project source\middlerCore\middlerApp.Core.DataAccess.SqlServer `
        --startup-project source\middlerCore\middlerApp.Core.MigrationBuilder `
        --configuration sqlserver `
        --output "DbScripts\AppDbContext_SqlServer_$($Name).sql" `
        --idempotent
}

if ($Context -eq "Identity") {
    # IdentityDbContext
    dotnet ef migrations script `
        --context IDPDbContext `
        --project source\middlerIdp\middlerApp.IDP.DataAccess.Sqlite `
        --startup-project source\middlerIdp\middlerApp.IDP.MigrationBuilder `
        --configuration sqlite `
        --output "DbScripts\IdpDbContext_Sqlite_$($Name).sql" `

    dotnet ef migrations script `
        --context IDPDbContext `
        --project source\middlerIdp\middlerApp.IDP.DataAccess.Postgres `
        --startup-project source\middlerIdp\middlerApp.IDP.MigrationBuilder `
        --configuration postgres `
        --output "DbScripts\IdpDbContext_Postgres_$($Name).sql" `
        --idempotent

    dotnet ef migrations script `
        --context IDPDbContext `
        --project source\middlerIdp\middlerApp.IDP.DataAccess.SqlServer `
        --startup-project source\middlerIdp\middlerApp.IDP.MigrationBuilder `
        --configuration sqlserver `
        --output "DbScripts\IdpDbContext_SqlServer_$($Name).sql" `
        --idempotent
}