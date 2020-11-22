using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace middlerApp.IDP.DataAccess.SqlServer.Migrations
{
    public partial class UserExternalClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Issuer",
                table: "UserClaims");

            migrationBuilder.CreateTable(
                name: "ExternalClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Issuer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalClaims_UserId",
                table: "ExternalClaims",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalClaims");

            migrationBuilder.AddColumn<string>(
                name: "Issuer",
                table: "UserClaims",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
