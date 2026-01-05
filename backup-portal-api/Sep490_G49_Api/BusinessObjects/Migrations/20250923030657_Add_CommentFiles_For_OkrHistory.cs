using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class Add_CommentFiles_For_OkrHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OkrHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StoredPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    IsImage = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentFiles_okrHistories_OkrHistoryId",
                        column: x => x.OkrHistoryId,
                        principalTable: "okrHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 9, 23, 3, 6, 56, 689, DateTimeKind.Utc).AddTicks(9602));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 23, 3, 6, 56, 689, DateTimeKind.Utc).AddTicks(8843), new DateTime(2025, 9, 23, 3, 6, 56, 689, DateTimeKind.Utc).AddTicks(8844) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 23, 3, 6, 56, 689, DateTimeKind.Utc).AddTicks(8902), new DateTime(2025, 9, 23, 3, 6, 56, 689, DateTimeKind.Utc).AddTicks(8902) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 23, 3, 6, 56, 689, DateTimeKind.Utc).AddTicks(8905), new DateTime(2025, 9, 23, 3, 6, 56, 689, DateTimeKind.Utc).AddTicks(8905) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 23, 3, 6, 56, 689, DateTimeKind.Utc).AddTicks(8839), new DateTime(2025, 9, 23, 3, 6, 56, 689, DateTimeKind.Utc).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 23, 3, 6, 56, 689, DateTimeKind.Utc).AddTicks(8906), new DateTime(2025, 9, 23, 3, 6, 56, 689, DateTimeKind.Utc).AddTicks(8907) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 9, 23, 10, 6, 56, 689, DateTimeKind.Local).AddTicks(9549), new byte[] { 212, 74, 135, 175, 250, 14, 111, 179, 194, 28, 205, 26, 171, 251, 217, 118, 126, 142, 102, 138, 1, 141, 124, 98, 196, 107, 162, 248, 119, 168, 6, 124, 242, 225, 166, 250, 199, 107, 15, 48, 59, 226, 30, 52, 241, 47, 212, 14, 95, 29, 154, 223, 62, 116, 35, 221, 171, 207, 46, 72, 218, 199, 125, 167 }, new byte[] { 47, 126, 200, 109, 46, 74, 133, 60, 15, 39, 48, 97, 99, 159, 58, 74, 200, 119, 255, 245, 172, 174, 27, 193, 102, 31, 47, 44, 163, 183, 97, 57, 103, 65, 114, 133, 240, 128, 32, 68, 26, 209, 176, 152, 161, 218, 234, 94, 182, 55, 47, 227, 197, 106, 9, 250, 45, 223, 98, 197, 110, 91, 20, 78, 195, 38, 205, 105, 192, 201, 146, 3, 210, 152, 172, 249, 220, 224, 92, 25, 176, 193, 199, 130, 172, 18, 100, 167, 200, 242, 183, 53, 195, 65, 61, 246, 122, 172, 16, 94, 75, 157, 106, 198, 86, 57, 56, 220, 94, 59, 35, 136, 229, 229, 183, 221, 76, 226, 30, 101, 97, 36, 239, 201, 248, 223, 9, 179 } });

            migrationBuilder.CreateIndex(
                name: "IX_CommentFiles_OkrHistoryId",
                table: "CommentFiles",
                column: "OkrHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentFiles");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 9, 22, 7, 36, 14, 276, DateTimeKind.Utc).AddTicks(6456));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 22, 7, 36, 14, 276, DateTimeKind.Utc).AddTicks(5608), new DateTime(2025, 9, 22, 7, 36, 14, 276, DateTimeKind.Utc).AddTicks(5608) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 22, 7, 36, 14, 276, DateTimeKind.Utc).AddTicks(5610), new DateTime(2025, 9, 22, 7, 36, 14, 276, DateTimeKind.Utc).AddTicks(5610) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 22, 7, 36, 14, 276, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 9, 22, 7, 36, 14, 276, DateTimeKind.Utc).AddTicks(5613) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 22, 7, 36, 14, 276, DateTimeKind.Utc).AddTicks(5604), new DateTime(2025, 9, 22, 7, 36, 14, 276, DateTimeKind.Utc).AddTicks(5605) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 22, 7, 36, 14, 276, DateTimeKind.Utc).AddTicks(5614), new DateTime(2025, 9, 22, 7, 36, 14, 276, DateTimeKind.Utc).AddTicks(5614) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 9, 22, 14, 36, 14, 276, DateTimeKind.Local).AddTicks(6401), new byte[] { 13, 88, 52, 201, 142, 117, 129, 58, 30, 237, 238, 73, 203, 124, 74, 216, 236, 135, 193, 28, 217, 244, 223, 168, 199, 104, 110, 238, 236, 235, 164, 28, 44, 104, 207, 249, 123, 148, 198, 111, 159, 198, 191, 13, 102, 42, 104, 251, 118, 196, 141, 113, 92, 190, 201, 240, 58, 152, 232, 158, 204, 166, 99, 41 }, new byte[] { 44, 124, 189, 75, 7, 220, 77, 158, 13, 158, 208, 124, 133, 0, 205, 46, 222, 65, 82, 135, 83, 184, 95, 25, 243, 131, 152, 221, 21, 158, 57, 32, 74, 190, 246, 53, 175, 157, 29, 37, 228, 245, 205, 228, 196, 84, 101, 175, 41, 203, 75, 169, 10, 249, 192, 8, 237, 207, 64, 192, 134, 249, 106, 42, 248, 202, 86, 120, 57, 79, 115, 203, 75, 20, 251, 74, 75, 8, 246, 183, 62, 14, 179, 79, 141, 88, 92, 192, 107, 212, 209, 101, 251, 254, 241, 39, 166, 13, 94, 144, 187, 253, 233, 83, 40, 141, 119, 84, 239, 86, 191, 127, 234, 85, 9, 130, 73, 235, 54, 116, 144, 138, 227, 254, 170, 136, 19, 188 } });
        }
    }
}
