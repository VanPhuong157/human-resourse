using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddProjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Stt = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TargetDays = table.Column<int>(type: "int", nullable: true),
                    ActualDays = table.Column<int>(type: "int", nullable: true),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IssuedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    IsGroup = table.Column<bool>(type: "bit", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoredPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    IsImage = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: new DateTime(2025, 9, 29, 10, 30, 38, 536, DateTimeKind.Utc).AddTicks(1808));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 29, 10, 30, 38, 536, DateTimeKind.Utc).AddTicks(1027), new DateTime(2025, 9, 29, 10, 30, 38, 536, DateTimeKind.Utc).AddTicks(1028) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 29, 10, 30, 38, 536, DateTimeKind.Utc).AddTicks(1030), new DateTime(2025, 9, 29, 10, 30, 38, 536, DateTimeKind.Utc).AddTicks(1030) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 29, 10, 30, 38, 536, DateTimeKind.Utc).AddTicks(1031), new DateTime(2025, 9, 29, 10, 30, 38, 536, DateTimeKind.Utc).AddTicks(1032) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 29, 10, 30, 38, 536, DateTimeKind.Utc).AddTicks(1023), new DateTime(2025, 9, 29, 10, 30, 38, 536, DateTimeKind.Utc).AddTicks(1025) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 29, 10, 30, 38, 536, DateTimeKind.Utc).AddTicks(1033), new DateTime(2025, 9, 29, 10, 30, 38, 536, DateTimeKind.Utc).AddTicks(1033) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 9, 29, 17, 30, 38, 536, DateTimeKind.Local).AddTicks(1752), new byte[] { 29, 191, 76, 249, 215, 174, 235, 219, 75, 74, 203, 103, 197, 67, 235, 193, 13, 57, 119, 183, 74, 63, 194, 88, 206, 113, 198, 204, 215, 5, 130, 7, 89, 45, 60, 216, 205, 122, 25, 5, 190, 116, 161, 99, 159, 218, 72, 249, 168, 11, 118, 192, 54, 236, 98, 216, 67, 141, 6, 61, 9, 55, 188, 158 }, new byte[] { 151, 207, 107, 46, 97, 128, 39, 27, 157, 2, 199, 84, 53, 126, 75, 152, 35, 180, 112, 132, 186, 179, 254, 91, 81, 23, 27, 176, 178, 93, 189, 68, 59, 106, 132, 79, 68, 200, 65, 48, 236, 236, 190, 135, 6, 236, 141, 212, 9, 87, 66, 47, 1, 24, 238, 173, 148, 240, 239, 132, 50, 255, 36, 33, 182, 138, 216, 173, 148, 65, 163, 202, 49, 229, 7, 234, 26, 218, 5, 112, 0, 56, 66, 220, 245, 30, 65, 94, 3, 90, 150, 36, 134, 242, 172, 30, 175, 175, 81, 228, 35, 192, 235, 182, 122, 216, 194, 133, 40, 167, 13, 62, 125, 129, 73, 105, 218, 67, 150, 86, 227, 144, 68, 91, 202, 117, 181, 37 } });
        }
    }
}
