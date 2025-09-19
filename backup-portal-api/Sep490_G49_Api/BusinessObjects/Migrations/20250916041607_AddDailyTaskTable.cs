using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddDailyTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OkrId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: true),
                    TargetNumber = table.Column<int>(type: "int", nullable: true),
                    Achieved = table.Column<int>(type: "int", nullable: true)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyTasks");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9926));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9176), new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9176) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9178), new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9178) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9179), new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9180) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9172), new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9174) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9201), new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9201) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 9, 11, 9, 25, 11, 764, DateTimeKind.Local).AddTicks(9877), new byte[] { 154, 150, 20, 36, 42, 43, 152, 92, 211, 239, 140, 160, 149, 216, 199, 160, 175, 124, 222, 34, 46, 211, 136, 130, 119, 186, 181, 40, 156, 247, 76, 111, 0, 73, 81, 215, 86, 143, 45, 89, 197, 234, 89, 172, 143, 144, 151, 30, 240, 123, 88, 74, 171, 113, 88, 67, 170, 61, 205, 122, 5, 188, 169, 233 }, new byte[] { 87, 150, 154, 83, 20, 152, 26, 2, 70, 98, 39, 240, 173, 151, 240, 202, 28, 170, 228, 6, 190, 174, 231, 118, 6, 175, 112, 235, 9, 164, 202, 25, 56, 57, 224, 224, 121, 91, 89, 195, 168, 221, 248, 173, 198, 202, 167, 150, 205, 36, 205, 49, 183, 39, 111, 217, 168, 251, 214, 8, 149, 1, 196, 9, 26, 135, 111, 137, 114, 216, 19, 247, 80, 196, 240, 3, 70, 158, 153, 97, 162, 203, 212, 160, 243, 249, 15, 219, 195, 233, 74, 187, 157, 50, 21, 99, 212, 232, 80, 217, 97, 176, 179, 164, 61, 46, 57, 150, 3, 24, 158, 223, 245, 35, 129, 77, 9, 93, 210, 118, 249, 174, 74, 236, 254, 233, 231, 99 } });
        }
    }
}
