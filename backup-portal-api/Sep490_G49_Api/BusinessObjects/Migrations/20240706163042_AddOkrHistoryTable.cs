using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddOkrHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Owner",
                table: "OKRs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "okrHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Owner = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OldProgress = table.Column<int>(type: "int", nullable: true),
                    NewProgress = table.Column<int>(type: "int", nullable: true),
                    OldStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OkrId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_okrHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_okrHistories_OKRs_OkrId",
                        column: x => x.OkrId,
                        principalTable: "OKRs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_okrHistories_OkrId",
                table: "okrHistories",
                column: "OkrId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "okrHistories");

            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                table: "OKRs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
