using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateAccessToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TokenExpires",
                table: "Users",
                newName: "RefreshTokenExpires");

            migrationBuilder.RenameColumn(
                name: "TokenCreated",
                table: "Users",
                newName: "RefreshTokenCreated");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AccessTokenCreated",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccessTokenCreated",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpires",
                table: "Users",
                newName: "TokenExpires");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenCreated",
                table: "Users",
                newName: "TokenCreated");
        }
    }
}
