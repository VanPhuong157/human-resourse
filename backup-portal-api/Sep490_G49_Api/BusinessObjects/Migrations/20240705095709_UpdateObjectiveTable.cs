using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateObjectiveTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Achieved",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "TargetNumber",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "UnitOfTarget",
                table: "Objectives");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Achieved",
                table: "Objectives",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TargetNumber",
                table: "Objectives",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfTarget",
                table: "Objectives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
