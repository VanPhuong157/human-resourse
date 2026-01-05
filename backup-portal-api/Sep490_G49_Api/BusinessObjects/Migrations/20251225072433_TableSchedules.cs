using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class TableSchedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ApprovedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Schedules_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StoredPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleAttachments_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleParticipants",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleParticipants", x => new { x.ScheduleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ScheduleParticipants_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleParticipants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 25, 7, 24, 32, 554, DateTimeKind.Utc).AddTicks(5696));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 25, 7, 24, 32, 554, DateTimeKind.Utc).AddTicks(4389), new DateTime(2025, 12, 25, 7, 24, 32, 554, DateTimeKind.Utc).AddTicks(4390) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 25, 7, 24, 32, 554, DateTimeKind.Utc).AddTicks(4392), new DateTime(2025, 12, 25, 7, 24, 32, 554, DateTimeKind.Utc).AddTicks(4392) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 25, 7, 24, 32, 554, DateTimeKind.Utc).AddTicks(4394), new DateTime(2025, 12, 25, 7, 24, 32, 554, DateTimeKind.Utc).AddTicks(4394) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 25, 7, 24, 32, 554, DateTimeKind.Utc).AddTicks(4384), new DateTime(2025, 12, 25, 7, 24, 32, 554, DateTimeKind.Utc).AddTicks(4385) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 25, 7, 24, 32, 554, DateTimeKind.Utc).AddTicks(4396), new DateTime(2025, 12, 25, 7, 24, 32, 554, DateTimeKind.Utc).AddTicks(4396) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 12, 25, 14, 24, 32, 554, DateTimeKind.Local).AddTicks(5621), new byte[] { 16, 212, 80, 220, 89, 42, 169, 134, 124, 100, 84, 169, 140, 8, 172, 243, 201, 86, 253, 45, 33, 244, 214, 211, 57, 231, 84, 20, 22, 193, 157, 15, 190, 142, 215, 213, 201, 193, 51, 42, 215, 202, 62, 244, 208, 40, 219, 244, 214, 155, 245, 166, 49, 223, 158, 178, 109, 129, 97, 243, 91, 33, 32, 8 }, new byte[] { 148, 146, 96, 59, 39, 46, 190, 148, 72, 36, 111, 141, 12, 85, 147, 98, 52, 8, 158, 75, 102, 173, 148, 97, 10, 226, 16, 15, 203, 171, 91, 71, 202, 90, 101, 108, 5, 174, 115, 101, 239, 243, 118, 241, 225, 145, 235, 144, 54, 204, 64, 107, 144, 47, 73, 215, 76, 158, 125, 254, 122, 205, 45, 56, 103, 155, 94, 97, 204, 113, 159, 178, 140, 158, 221, 101, 80, 246, 237, 151, 246, 61, 254, 164, 101, 236, 168, 97, 207, 122, 27, 184, 76, 97, 252, 149, 171, 92, 6, 156, 136, 1, 127, 72, 82, 250, 7, 170, 240, 144, 70, 148, 91, 39, 247, 73, 72, 27, 38, 116, 169, 247, 203, 58, 150, 23, 87, 4 } });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleAttachments_ScheduleId",
                table: "ScheduleAttachments",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleParticipants_UserId",
                table: "ScheduleParticipants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ApprovedById",
                table: "Schedules",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CreatorId",
                table: "Schedules",
                column: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleAttachments");

            migrationBuilder.DropTable(
                name: "ScheduleParticipants");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(6363));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5404), new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5405) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5406), new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5406) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5407), new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5408) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5399), new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5401) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5409), new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5409) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 11, 14, 10, 49, 33, 997, DateTimeKind.Local).AddTicks(6280), new byte[] { 234, 155, 160, 59, 250, 39, 34, 245, 189, 81, 161, 198, 173, 35, 3, 232, 212, 84, 139, 143, 29, 23, 160, 141, 48, 221, 140, 12, 36, 204, 117, 255, 3, 40, 130, 251, 176, 106, 80, 29, 144, 58, 139, 121, 158, 15, 230, 59, 240, 57, 82, 10, 44, 181, 232, 204, 6, 6, 164, 203, 253, 24, 148, 80 }, new byte[] { 135, 202, 58, 226, 157, 204, 189, 79, 153, 149, 13, 245, 216, 140, 112, 92, 213, 157, 187, 152, 108, 141, 64, 92, 27, 136, 129, 135, 77, 146, 59, 212, 170, 108, 77, 225, 139, 186, 185, 168, 194, 235, 62, 51, 82, 172, 29, 114, 65, 102, 34, 32, 89, 178, 126, 139, 63, 5, 183, 245, 37, 198, 117, 80, 71, 245, 179, 113, 86, 247, 6, 103, 189, 98, 63, 150, 120, 74, 172, 209, 82, 181, 87, 188, 119, 116, 35, 167, 3, 214, 196, 79, 19, 23, 27, 110, 177, 241, 51, 198, 205, 237, 234, 135, 14, 63, 200, 84, 48, 132, 58, 186, 38, 219, 186, 1, 212, 13, 21, 41, 46, 75, 94, 170, 249, 132, 221, 195 } });
        }
    }
}
