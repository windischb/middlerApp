

[CmdletBinding()]
param (
    [Parameter()]
    [string]$Name,

    [Parameter()]
    [string]$Context
)



if ($Context -eq "App") {
    # AppDbContext
    dotnet ef migrations add $Name `
        --context APPDbContext `
        --project source\middlerCore\middlerApp.Core.DataAccess.Sqlite `
        --startup-project source\middlerCore\middlerApp.Core.MigrationBuilder `
        --configuration sqlite

    dotnet ef migrations add $Name `
        --context APPDbContext `
        --project source\middlerCore\middlerApp.Core.DataAccess.Postgres `
        --startup-project source\middlerCore\middlerApp.Core.MigrationBuilder `
        --configuration postgres

    dotnet ef migrations add $Name `
        --context APPDbContext `
        --project source\middlerCore\middlerApp.Core.DataAccess.SqlServer `
        --startup-project source\middlerCore\middlerApp.Core.MigrationBuilder `
        --configuration sqlserver
}


if ($Context -eq "Identity") {

    # IdentityDbContext
    dotnet ef migrations add $Name `
        --context IDPDbContext `
        --project source\middlerIdp\middlerApp.IDP.DataAccess.Sqlite `
        --startup-project source\middlerIdp\middlerApp.IDP.MigrationBuilder `
        --configuration sqlite 

    dotnet ef migrations add $Name `
        --context IDPDbContext `
        --project source\middlerIdp\middlerApp.IDP.DataAccess.Postgres `
        --startup-project source\middlerIdp\middlerApp.IDP.MigrationBuilder `
        --configuration postgres 

    dotnet ef migrations add $Name `
        --context IDPDbContext `
        --project source\middlerIdp\middlerApp.IDP.DataAccess.SqlServer `
        --startup-project source\middlerIdp\middlerApp.IDP.MigrationBuilder `
        --configuration sqlserver 
}
