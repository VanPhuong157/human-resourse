using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddTemporaryPasswordToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TemporaryPasswordExpires",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "TemporaryPasswordHash",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "TemporaryPasswordSalt",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemporaryPasswordExpires",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TemporaryPasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TemporaryPasswordSalt",
                table: "Users");
        }
    }
}
