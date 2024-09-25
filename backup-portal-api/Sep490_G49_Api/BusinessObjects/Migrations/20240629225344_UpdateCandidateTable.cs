using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateCandidateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvandceSkill",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Attitude",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "SoftSkill",
                table: "Candidates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdvandceSkill",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Attitude",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoftSkill",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
