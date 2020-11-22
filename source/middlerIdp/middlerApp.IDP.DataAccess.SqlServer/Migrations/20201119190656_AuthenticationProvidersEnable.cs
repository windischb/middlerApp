using Microsoft.EntityFrameworkCore.Migrations;

namespace middlerApp.IDP.DataAccess.SqlServer.Migrations
{
    public partial class AuthenticationProvidersEnable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "AuthenticationProviders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "AuthenticationProviders");
        }
    }
}
