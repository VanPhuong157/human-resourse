using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateDailyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OKRs_Departments_DepartmentId",
                table: "OKRs");

            migrationBuilder.DropTable(
                name: "DailyTasks");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "OKRs");

            migrationBuilder.RenameColumn(
                name: "TargerNumber",
                table: "OKRs",
                newName: "TargetNumber");

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "OKRs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "OKRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "OKRs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "OKRs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "OKRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "OKRs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OkrDepartments",
                columns: table => new
                {
                    OkrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrDepartments", x => new { x.OkrId, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_OkrDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OkrDepartments_OKRs_OkrId",
                        column: x => x.OkrId,
                        principalTable: "OKRs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OkrUsers",
                columns: table => new
                {
                    OkrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkrUsers", x => new { x.OkrId, x.UserId });
                    table.ForeignKey(
                        name: "FK_OkrUsers_OKRs_OkrId",
                        column: x => x.OkrId,
                        principalTable: "OKRs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OkrUsers_Users_UserId",
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
                value: new DateTime(2025, 9, 16, 9, 53, 23, 318, DateTimeKind.Utc).AddTicks(7031));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 9, 53, 23, 318, DateTimeKind.Utc).AddTicks(6242), new DateTime(2025, 9, 16, 9, 53, 23, 318, DateTimeKind.Utc).AddTicks(6242) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 9, 53, 23, 318, DateTimeKind.Utc).AddTicks(6244), new DateTime(2025, 9, 16, 9, 53, 23, 318, DateTimeKind.Utc).AddTicks(6244) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 9, 53, 23, 318, DateTimeKind.Utc).AddTicks(6245), new DateTime(2025, 9, 16, 9, 53, 23, 318, DateTimeKind.Utc).AddTicks(6246) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 9, 53, 23, 318, DateTimeKind.Utc).AddTicks(6237), new DateTime(2025, 9, 16, 9, 53, 23, 318, DateTimeKind.Utc).AddTicks(6239) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 9, 53, 23, 318, DateTimeKind.Utc).AddTicks(6247), new DateTime(2025, 9, 16, 9, 53, 23, 318, DateTimeKind.Utc).AddTicks(6247) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 9, 16, 16, 53, 23, 318, DateTimeKind.Local).AddTicks(6953), new byte[] { 34, 238, 137, 67, 3, 225, 219, 241, 18, 105, 147, 200, 42, 114, 207, 72, 23, 199, 219, 247, 183, 164, 5, 232, 233, 81, 20, 94, 127, 236, 65, 145, 247, 184, 64, 192, 214, 94, 156, 187, 112, 253, 214, 155, 134, 217, 239, 200, 105, 128, 107, 122, 222, 141, 124, 88, 175, 22, 227, 118, 35, 82, 3, 168 }, new byte[] { 59, 134, 150, 119, 118, 27, 232, 187, 95, 11, 227, 79, 210, 218, 78, 160, 100, 43, 55, 249, 66, 196, 242, 42, 195, 39, 170, 9, 199, 55, 100, 169, 217, 179, 10, 53, 201, 121, 225, 189, 34, 135, 213, 92, 201, 241, 35, 195, 237, 235, 20, 22, 215, 4, 214, 130, 174, 243, 199, 49, 129, 159, 10, 220, 46, 23, 103, 122, 233, 87, 198, 4, 251, 141, 113, 170, 208, 116, 3, 248, 136, 138, 227, 61, 155, 248, 133, 140, 90, 148, 88, 212, 14, 50, 250, 15, 155, 193, 25, 146, 5, 43, 63, 107, 203, 223, 172, 27, 127, 132, 57, 99, 100, 160, 36, 254, 63, 71, 75, 52, 255, 121, 116, 232, 79, 76, 68, 230 } });

            migrationBuilder.CreateIndex(
                name: "IX_OkrDepartments_DepartmentId",
                table: "OkrDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OkrUsers_UserId",
                table: "OkrUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OKRs_Departments_DepartmentId",
                table: "OKRs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OKRs_Departments_DepartmentId",
                table: "OKRs");

            migrationBuilder.DropTable(
                name: "OkrDepartments");

            migrationBuilder.DropTable(
                name: "OkrUsers");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "OKRs");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "OKRs");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "OKRs");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "OKRs");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "OKRs");

            migrationBuilder.RenameColumn(
                name: "TargetNumber",
                table: "OKRs",
                newName: "TargerNumber");

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "OKRs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Owner",
                table: "OKRs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DailyTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OkrId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Achieved = table.Column<int>(type: "int", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetNumber = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyTasks_OKRs_OkrId",
                        column: x => x.OkrId,
                        principalTable: "OKRs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyTasks_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyTasks_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 9, 16, 4, 16, 6, 419, DateTimeKind.Utc).AddTicks(9293));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 4, 16, 6, 419, DateTimeKind.Utc).AddTicks(8367), new DateTime(2025, 9, 16, 4, 16, 6, 419, DateTimeKind.Utc).AddTicks(8368) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 4, 16, 6, 419, DateTimeKind.Utc).AddTicks(8370), new DateTime(2025, 9, 16, 4, 16, 6, 419, DateTimeKind.Utc).AddTicks(8370) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 4, 16, 6, 419, DateTimeKind.Utc).AddTicks(8372), new DateTime(2025, 9, 16, 4, 16, 6, 419, DateTimeKind.Utc).AddTicks(8372) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 4, 16, 6, 419, DateTimeKind.Utc).AddTicks(8364), new DateTime(2025, 9, 16, 4, 16, 6, 419, DateTimeKind.Utc).AddTicks(8365) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 16, 4, 16, 6, 419, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 9, 16, 4, 16, 6, 419, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 9, 16, 11, 16, 6, 419, DateTimeKind.Local).AddTicks(9233), new byte[] { 11, 62, 3, 183, 138, 231, 6, 204, 5, 53, 49, 136, 179, 4, 29, 193, 4, 135, 97, 12, 126, 102, 51, 212, 11, 212, 47, 235, 135, 27, 148, 152, 12, 194, 32, 171, 250, 236, 220, 144, 4, 102, 101, 102, 228, 64, 11, 215, 216, 255, 142, 246, 49, 255, 106, 123, 210, 94, 3, 12, 144, 240, 248, 69 }, new byte[] { 61, 126, 130, 18, 225, 79, 240, 18, 212, 188, 18, 146, 236, 79, 249, 31, 46, 59, 133, 45, 156, 70, 245, 78, 48, 238, 49, 40, 52, 39, 159, 133, 248, 96, 123, 177, 166, 100, 93, 175, 34, 96, 225, 19, 87, 41, 72, 151, 115, 84, 255, 206, 1, 129, 130, 179, 104, 3, 54, 108, 236, 15, 64, 139, 238, 134, 94, 221, 239, 117, 30, 83, 212, 139, 47, 234, 170, 201, 83, 97, 119, 109, 110, 69, 111, 176, 39, 250, 63, 234, 123, 7, 67, 73, 67, 138, 164, 94, 12, 35, 188, 236, 227, 152, 27, 9, 62, 227, 104, 43, 188, 147, 99, 238, 126, 252, 169, 223, 254, 5, 37, 61, 112, 233, 0, 148, 46, 76 } });

            migrationBuilder.CreateIndex(
                name: "IX_DailyTasks_ManagerId",
                table: "DailyTasks",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyTasks_OkrId",
                table: "DailyTasks",
                column: "OkrId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyTasks_OwnerId",
                table: "DailyTasks",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OKRs_Departments_DepartmentId",
                table: "OKRs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
