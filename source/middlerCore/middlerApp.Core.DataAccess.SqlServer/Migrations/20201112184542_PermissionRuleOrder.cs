using Microsoft.EntityFrameworkCore.Migrations;

namespace middlerApp.Core.DataAccess.SqlServer.Migrations
{
    public partial class PermissionRuleOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Order",
                table: "EndpointRulePermission",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "EndpointRulePermission");
        }
    }
}
