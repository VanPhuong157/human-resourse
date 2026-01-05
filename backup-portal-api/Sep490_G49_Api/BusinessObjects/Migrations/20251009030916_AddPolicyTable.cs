using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddPolicyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectDepartments");

            migrationBuilder.DropTable(
                name: "ProjectFiles");

            migrationBuilder.DropTable(
                name: "ProjectUsers");

            migrationBuilder.DropTable(
                name: "Projects");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ActualDays = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsGroup = table.Column<bool>(type: "bit", nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Stt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TargetDays = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Projects_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDepartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectDepartments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    IsImage = table.Column<bool>(type: "bit", nullable: false),
                    StoredPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectFiles_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Users_UserId",
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
                value: new DateTime(2025, 10, 3, 4, 15, 45, 226, DateTimeKind.Utc).AddTicks(9620));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 4, 15, 45, 226, DateTimeKind.Utc).AddTicks(8853), new DateTime(2025, 10, 3, 4, 15, 45, 226, DateTimeKind.Utc).AddTicks(8854) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 4, 15, 45, 226, DateTimeKind.Utc).AddTicks(8856), new DateTime(2025, 10, 3, 4, 15, 45, 226, DateTimeKind.Utc).AddTicks(8856) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 4, 15, 45, 226, DateTimeKind.Utc).AddTicks(8857), new DateTime(2025, 10, 3, 4, 15, 45, 226, DateTimeKind.Utc).AddTicks(8858) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 4, 15, 45, 226, DateTimeKind.Utc).AddTicks(8850), new DateTime(2025, 10, 3, 4, 15, 45, 226, DateTimeKind.Utc).AddTicks(8851) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 4, 15, 45, 226, DateTimeKind.Utc).AddTicks(8859), new DateTime(2025, 10, 3, 4, 15, 45, 226, DateTimeKind.Utc).AddTicks(8859) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 10, 3, 11, 15, 45, 226, DateTimeKind.Local).AddTicks(9566), new byte[] { 8, 220, 48, 218, 17, 95, 28, 117, 101, 63, 72, 171, 54, 136, 254, 55, 234, 183, 246, 54, 204, 134, 27, 84, 77, 45, 51, 106, 42, 117, 120, 223, 51, 204, 67, 174, 163, 221, 234, 212, 89, 109, 181, 30, 218, 235, 54, 148, 173, 115, 10, 2, 48, 31, 175, 5, 38, 108, 219, 108, 204, 139, 162, 33 }, new byte[] { 155, 231, 30, 119, 215, 214, 23, 103, 113, 71, 114, 192, 230, 188, 160, 179, 217, 123, 98, 146, 92, 99, 120, 125, 217, 240, 242, 59, 60, 131, 81, 33, 177, 0, 70, 30, 8, 156, 193, 205, 254, 251, 245, 14, 114, 160, 32, 42, 224, 75, 86, 182, 109, 100, 131, 47, 244, 232, 86, 156, 210, 223, 221, 238, 104, 18, 220, 189, 40, 170, 10, 47, 94, 62, 10, 226, 90, 157, 124, 217, 164, 31, 153, 141, 28, 144, 81, 43, 208, 125, 61, 210, 9, 253, 56, 232, 255, 223, 34, 32, 123, 208, 145, 89, 216, 158, 163, 63, 88, 143, 8, 148, 60, 218, 255, 0, 96, 157, 67, 183, 138, 178, 185, 84, 39, 231, 188, 31 } });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDepartments_DepartmentId",
                table: "ProjectDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDepartments_ProjectId",
                table: "ProjectDepartments",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFiles_ProjectId",
                table: "ProjectFiles",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ParentId_OrderIndex",
                table: "Projects",
                columns: new[] { "ParentId", "OrderIndex" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_ProjectId",
                table: "ProjectUsers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_UserId",
                table: "ProjectUsers",
                column: "UserId");
        }
    }
}
