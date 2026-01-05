using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdatePolicyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PolicySteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicySteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicySteps_PolicySteps_ParentId",
                        column: x => x.ParentId,
                        principalTable: "PolicySteps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PolicyHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "PolicyStepsDepartment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyStepsDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyStepsDepartment_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyStepsDepartment_PolicySteps_PolicyStepId",
                        column: x => x.PolicyStepId,
                        principalTable: "PolicySteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyStepsUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLead = table.Column<bool>(type: "bit", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyStepsUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyStepsUser_PolicySteps_PolicyStepId",
                        column: x => x.PolicyStepId,
                        principalTable: "PolicySteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyStepsUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyHistoryComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ByRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoredPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    IsImage = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLead = table.Column<bool>(type: "bit", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_PolicySteps_ParentId",
                table: "PolicySteps",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyStepsDepartment_DepartmentId",
                table: "PolicyStepsDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyStepsDepartment_PolicyStepId",
                table: "PolicyStepsDepartment",
                column: "PolicyStepId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyStepsUser_PolicyStepId",
                table: "PolicyStepsUser",
                column: "PolicyStepId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyStepsUser_UserId",
                table: "PolicyStepsUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolicyHistoryComment");

            migrationBuilder.DropTable(
                name: "PolicyHistoryFile");

            migrationBuilder.DropTable(
                name: "PolicyHistoryUser");

            migrationBuilder.DropTable(
                name: "PolicyStepsDepartment");

            migrationBuilder.DropTable(
                name: "PolicyStepsUser");

            migrationBuilder.DropTable(
                name: "PolicyHistory");

            migrationBuilder.DropTable(
                name: "PolicySteps");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 9, 3, 9, 15, 828, DateTimeKind.Utc).AddTicks(1206));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 9, 15, 828, DateTimeKind.Utc).AddTicks(301), new DateTime(2025, 10, 9, 3, 9, 15, 828, DateTimeKind.Utc).AddTicks(301) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 9, 15, 828, DateTimeKind.Utc).AddTicks(304), new DateTime(2025, 10, 9, 3, 9, 15, 828, DateTimeKind.Utc).AddTicks(304) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 9, 15, 828, DateTimeKind.Utc).AddTicks(307), new DateTime(2025, 10, 9, 3, 9, 15, 828, DateTimeKind.Utc).AddTicks(307) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 9, 15, 828, DateTimeKind.Utc).AddTicks(295), new DateTime(2025, 10, 9, 3, 9, 15, 828, DateTimeKind.Utc).AddTicks(297) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 3, 9, 15, 828, DateTimeKind.Utc).AddTicks(310), new DateTime(2025, 10, 9, 3, 9, 15, 828, DateTimeKind.Utc).AddTicks(310) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 10, 9, 10, 9, 15, 828, DateTimeKind.Local).AddTicks(1151), new byte[] { 18, 249, 150, 79, 102, 201, 35, 134, 5, 1, 133, 103, 222, 52, 254, 121, 16, 24, 15, 19, 226, 250, 227, 159, 1, 222, 64, 133, 75, 70, 212, 189, 119, 197, 181, 235, 200, 107, 4, 34, 209, 182, 36, 132, 112, 194, 101, 159, 125, 239, 114, 120, 125, 217, 55, 159, 221, 75, 105, 241, 47, 189, 117, 10 }, new byte[] { 32, 85, 151, 138, 35, 157, 104, 46, 14, 177, 16, 129, 64, 138, 109, 88, 42, 121, 241, 108, 113, 134, 194, 198, 122, 93, 252, 20, 197, 231, 229, 165, 158, 197, 49, 99, 28, 25, 102, 191, 97, 135, 228, 8, 19, 33, 15, 191, 65, 218, 231, 31, 27, 170, 45, 25, 38, 89, 126, 231, 199, 37, 74, 56, 38, 8, 114, 115, 247, 124, 131, 217, 96, 100, 73, 4, 101, 87, 46, 193, 164, 91, 127, 126, 145, 34, 146, 98, 20, 149, 55, 159, 163, 218, 41, 47, 181, 179, 80, 156, 103, 88, 209, 20, 91, 235, 67, 66, 156, 216, 91, 3, 229, 25, 169, 128, 39, 90, 130, 118, 78, 243, 157, 120, 107, 243, 134, 54 } });
        }
    }
}
