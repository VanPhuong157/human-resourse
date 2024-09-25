using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateCandidateJobPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionPlanDetail",
                table: "OKRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "JobPostId",
                table: "Candidates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_JobPostId",
                table: "Candidates",
                column: "JobPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_JobPosts_JobPostId",
                table: "Candidates",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_JobPosts_JobPostId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_JobPostId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "ActionPlanDetail",
                table: "OKRs");

            migrationBuilder.DropColumn(
                name: "JobPostId",
                table: "Candidates");
        }
    }
}
