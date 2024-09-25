using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateOKRHTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_okrHistories_OKRs_OkrId",
                table: "okrHistories");

            migrationBuilder.AlterColumn<Guid>(
                name: "OkrId",
                table: "okrHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "okrHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_okrHistories_OKRs_OkrId",
                table: "okrHistories",
                column: "OkrId",
                principalTable: "OKRs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_okrHistories_OKRs_OkrId",
                table: "okrHistories");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "okrHistories");

            migrationBuilder.AlterColumn<Guid>(
                name: "OkrId",
                table: "okrHistories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_okrHistories_OKRs_OkrId",
                table: "okrHistories",
                column: "OkrId",
                principalTable: "OKRs",
                principalColumn: "Id");
        }
    }
}
