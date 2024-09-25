using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddReasonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Header",
                table: "HomePages",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "HomePages",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "HomePages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "HomePages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

           

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.DropColumn(
                name: "Address",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "HomePages");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "HomePages",
                newName: "Header");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "HomePages",
                newName: "Description");
        }
    }
}
