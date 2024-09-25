using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateNotificationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartProgress",
                table: "OKRs",
                newName: "TargerNumber");

            migrationBuilder.AddColumn<int>(
                name: "Achieved",
                table: "OKRs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfTarget",
                table: "OKRs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Achieved",
                table: "OKRs");

            migrationBuilder.DropColumn(
                name: "UnitOfTarget",
                table: "OKRs");

            migrationBuilder.RenameColumn(
                name: "TargerNumber",
                table: "OKRs",
                newName: "StartProgress");
        }
    }
}
