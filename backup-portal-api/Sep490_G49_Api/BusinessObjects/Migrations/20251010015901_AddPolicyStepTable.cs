using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddPolicyStepTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PolicyStepsDepartment_Departments_DepartmentId",
                table: "PolicyStepsDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyStepsDepartment_PolicySteps_PolicyStepId",
                table: "PolicyStepsDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyStepsUser_PolicySteps_PolicyStepId",
                table: "PolicyStepsUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyStepsUser_Users_UserId",
                table: "PolicyStepsUser");

            migrationBuilder.DropTable(
                name: "PolicyHistoryComment");

            migrationBuilder.DropTable(
                name: "PolicyHistoryFile");

            migrationBuilder.DropTable(
                name: "PolicyHistoryUser");

            migrationBuilder.DropTable(
                name: "PolicyHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PolicyStepsUser",
                table: "PolicyStepsUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PolicyStepsDepartment",
                table: "PolicyStepsDepartment");

            migrationBuilder.RenameTable(
                name: "PolicyStepsUser",
                newName: "PolicyStepUsers");

            migrationBuilder.RenameTable(
                name: "PolicyStepsDepartment",
                newName: "PolicyStepDepartments");

            migrationBuilder.RenameIndex(
                name: "IX_PolicyStepsUser_UserId",
                table: "PolicyStepUsers",
                newName: "IX_PolicyStepUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PolicyStepsUser_PolicyStepId",
                table: "PolicyStepUsers",
                newName: "IX_PolicyStepUsers_PolicyStepId");

            migrationBuilder.RenameIndex(
                name: "IX_PolicyStepsDepartment_PolicyStepId",
                table: "PolicyStepDepartments",
                newName: "IX_PolicyStepDepartments_PolicyStepId");

            migrationBuilder.RenameIndex(
                name: "IX_PolicyStepsDepartment_DepartmentId",
                table: "PolicyStepDepartments",
                newName: "IX_PolicyStepDepartments_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PolicyStepUsers",
                table: "PolicyStepUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PolicyStepDepartments",
                table: "PolicyStepDepartments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_PolicySteps_PolicyStepId",
                        column: x => x.PolicyStepId,
                        principalTable: "PolicySteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ByRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionComments_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionDepartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmissionDepartments_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ByRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionEvents_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoredPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    IsImage = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionFiles_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionParticipants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLead = table.Column<bool>(type: "bit", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionParticipants_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmissionParticipants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 10, 1, 59, 0, 907, DateTimeKind.Utc).AddTicks(682));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9930), new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9931) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9932), new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9933) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9934), new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9934) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9926), new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9928) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9936), new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9937) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 10, 10, 8, 59, 0, 907, DateTimeKind.Local).AddTicks(631), new byte[] { 173, 48, 145, 191, 226, 21, 221, 24, 84, 177, 228, 107, 54, 97, 20, 73, 198, 1, 159, 44, 227, 212, 54, 192, 113, 134, 72, 72, 181, 190, 229, 54, 215, 208, 54, 60, 82, 155, 60, 84, 239, 94, 216, 148, 154, 135, 63, 5, 206, 159, 87, 43, 58, 153, 204, 195, 95, 200, 197, 159, 72, 119, 154, 153 }, new byte[] { 67, 122, 64, 208, 95, 155, 186, 136, 227, 18, 89, 173, 244, 190, 108, 110, 124, 108, 158, 8, 135, 31, 166, 218, 187, 98, 215, 78, 138, 84, 19, 130, 55, 46, 148, 240, 9, 85, 49, 70, 175, 145, 124, 87, 111, 194, 173, 218, 193, 121, 156, 109, 18, 41, 174, 119, 93, 89, 205, 200, 6, 85, 81, 218, 59, 163, 86, 40, 0, 39, 227, 32, 120, 19, 169, 40, 22, 156, 46, 235, 182, 19, 19, 141, 110, 48, 138, 17, 172, 139, 210, 201, 40, 52, 212, 6, 121, 232, 86, 199, 235, 80, 110, 8, 80, 168, 39, 215, 65, 199, 97, 109, 173, 95, 38, 55, 123, 143, 21, 96, 215, 177, 110, 192, 173, 160, 39, 117 } });

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionComments_SubmissionId",
                table: "SubmissionComments",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionDepartments_DepartmentId",
                table: "SubmissionDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionDepartments_SubmissionId",
                table: "SubmissionDepartments",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionEvents_SubmissionId",
                table: "SubmissionEvents",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionFiles_SubmissionId",
                table: "SubmissionFiles",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionParticipants_SubmissionId",
                table: "SubmissionParticipants",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionParticipants_UserId",
                table: "SubmissionParticipants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_PolicyStepId",
                table: "Submissions",
                column: "PolicyStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyStepDepartments_Departments_DepartmentId",
                table: "PolicyStepDepartments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyStepDepartments_PolicySteps_PolicyStepId",
                table: "PolicyStepDepartments",
                column: "PolicyStepId",
                principalTable: "PolicySteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyStepUsers_PolicySteps_PolicyStepId",
                table: "PolicyStepUsers",
                column: "PolicyStepId",
                principalTable: "PolicySteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyStepUsers_Users_UserId",
                table: "PolicyStepUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PolicyStepDepartments_Departments_DepartmentId",
                table: "PolicyStepDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyStepDepartments_PolicySteps_PolicyStepId",
                table: "PolicyStepDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyStepUsers_PolicySteps_PolicyStepId",
                table: "PolicyStepUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PolicyStepUsers_Users_UserId",
                table: "PolicyStepUsers");

            migrationBuilder.DropTable(
                name: "SubmissionComments");

            migrationBuilder.DropTable(
                name: "SubmissionDepartments");

            migrationBuilder.DropTable(
                name: "SubmissionEvents");

            migrationBuilder.DropTable(
                name: "SubmissionFiles");

            migrationBuilder.DropTable(
                name: "SubmissionParticipants");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PolicyStepUsers",
                table: "PolicyStepUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PolicyStepDepartments",
                table: "PolicyStepDepartments");

            migrationBuilder.RenameTable(
                name: "PolicyStepUsers",
                newName: "PolicyStepsUser");

            migrationBuilder.RenameTable(
                name: "PolicyStepDepartments",
                newName: "PolicyStepsDepartment");

            migrationBuilder.RenameIndex(
                name: "IX_PolicyStepUsers_UserId",
                table: "PolicyStepsUser",
                newName: "IX_PolicyStepsUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PolicyStepUsers_PolicyStepId",
                table: "PolicyStepsUser",
                newName: "IX_PolicyStepsUser_PolicyStepId");

            migrationBuilder.RenameIndex(
                name: "IX_PolicyStepDepartments_PolicyStepId",
                table: "PolicyStepsDepartment",
                newName: "IX_PolicyStepsDepartment_PolicyStepId");

            migrationBuilder.RenameIndex(
                name: "IX_PolicyStepDepartments_DepartmentId",
                table: "PolicyStepsDepartment",
                newName: "IX_PolicyStepsDepartment_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PolicyStepsUser",
                table: "PolicyStepsUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PolicyStepsDepartment",
                table: "PolicyStepsDepartment",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PolicyHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyHistory_PolicySteps_PolicyStepId",
                        column: x => x.PolicyStepId,
                        principalTable: "PolicySteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyHistoryComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ByRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyHistoryComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyHistoryComment_PolicyHistory_PolicyHistoryId",
                        column: x => x.PolicyHistoryId,
                        principalTable: "PolicyHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyHistoryFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    IsImage = table.Column<bool>(type: "bit", nullable: false),
                    StoredPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyHistoryFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyHistoryFile_PolicyHistory_PolicyHistoryId",
                        column: x => x.PolicyHistoryId,
                        principalTable: "PolicyHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyHistoryUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsLead = table.Column<bool>(type: "bit", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyHistoryUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyHistoryUser_PolicyHistory_PolicyHistoryId",
                        column: x => x.PolicyHistoryId,
                        principalTable: "PolicyHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyHistoryUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 9, 3, 14, 8, 331, DateTimeKind.Utc).AddTicks(3794));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 14, 8, 331, DateTimeKind.Utc).AddTicks(3098), new DateTime(2025, 10, 9, 3, 14, 8, 331, DateTimeKind.Utc).AddTicks(3099) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 14, 8, 331, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 10, 9, 3, 14, 8, 331, DateTimeKind.Utc).AddTicks(3101) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 14, 8, 331, DateTimeKind.Utc).AddTicks(3102), new DateTime(2025, 10, 9, 3, 14, 8, 331, DateTimeKind.Utc).AddTicks(3102) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 14, 8, 331, DateTimeKind.Utc).AddTicks(3095), new DateTime(2025, 10, 9, 3, 14, 8, 331, DateTimeKind.Utc).AddTicks(3096) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 14, 8, 331, DateTimeKind.Utc).AddTicks(3103), new DateTime(2025, 10, 9, 3, 14, 8, 331, DateTimeKind.Utc).AddTicks(3103) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 10, 9, 10, 14, 8, 331, DateTimeKind.Local).AddTicks(3725), new byte[] { 132, 192, 79, 212, 215, 185, 130, 142, 3, 195, 205, 17, 234, 210, 73, 59, 6, 4, 45, 3, 26, 194, 198, 188, 53, 230, 33, 233, 85, 98, 171, 120, 229, 162, 97, 167, 97, 166, 101, 207, 110, 174, 95, 221, 196, 25, 183, 37, 226, 204, 108, 66, 214, 203, 80, 192, 185, 45, 233, 134, 102, 135, 109, 58 }, new byte[] { 80, 111, 77, 93, 244, 157, 144, 132, 87, 233, 222, 85, 63, 1, 175, 135, 54, 30, 74, 32, 5, 111, 8, 205, 43, 1, 87, 128, 96, 219, 179, 152, 143, 28, 231, 76, 35, 189, 69, 166, 176, 244, 26, 175, 9, 191, 115, 83, 182, 228, 186, 30, 86, 211, 98, 76, 8, 98, 150, 152, 245, 9, 44, 49, 155, 14, 254, 71, 34, 41, 135, 205, 228, 226, 25, 106, 126, 21, 221, 28, 177, 105, 244, 168, 137, 230, 122, 90, 122, 33, 64, 134, 203, 240, 73, 149, 178, 218, 158, 117, 180, 142, 250, 123, 86, 186, 141, 102, 14, 4, 36, 199, 83, 152, 22, 122, 213, 123, 210, 224, 154, 76, 234, 46, 178, 136, 235, 44 } });

            migrationBuilder.CreateIndex(
                name: "IX_PolicyHistory_PolicyStepId",
                table: "PolicyHistory",
                column: "PolicyStepId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyHistoryComment_PolicyHistoryId",
                table: "PolicyHistoryComment",
                column: "PolicyHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyHistoryFile_PolicyHistoryId",
                table: "PolicyHistoryFile",
                column: "PolicyHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyHistoryUser_PolicyHistoryId",
                table: "PolicyHistoryUser",
                column: "PolicyHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyHistoryUser_UserId",
                table: "PolicyHistoryUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyStepsDepartment_Departments_DepartmentId",
                table: "PolicyStepsDepartment",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyStepsDepartment_PolicySteps_PolicyStepId",
                table: "PolicyStepsDepartment",
                column: "PolicyStepId",
                principalTable: "PolicySteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyStepsUser_PolicySteps_PolicyStepId",
                table: "PolicyStepsUser",
                column: "PolicyStepId",
                principalTable: "PolicySteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyStepsUser_Users_UserId",
                table: "PolicyStepsUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
