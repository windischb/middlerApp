using Microsoft.EntityFrameworkCore.Migrations;

namespace middlerApp.IDP.DataAccess.SqlServer.Migrations
{
    public partial class ClaimsIssuer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Issuer",
                table: "UserClaims",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Issuer",
                table: "UserClaims");
        }
    }
}
