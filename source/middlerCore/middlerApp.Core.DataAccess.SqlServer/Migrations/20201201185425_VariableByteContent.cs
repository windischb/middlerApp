using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace middlerApp.Core.DataAccess.SqlServer.Migrations
{
    public partial class VariableByteContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Bytes",
                table: "Variables",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bytes",
                table: "Variables");
        }
    }
}
