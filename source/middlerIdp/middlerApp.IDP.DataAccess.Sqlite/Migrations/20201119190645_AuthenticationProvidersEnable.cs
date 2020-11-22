using Microsoft.EntityFrameworkCore.Migrations;

namespace middlerApp.IDP.DataAccess.Sqlite.Migrations
{
    public partial class AuthenticationProvidersEnable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "AuthenticationProviders",
                type: "INTEGER",
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
