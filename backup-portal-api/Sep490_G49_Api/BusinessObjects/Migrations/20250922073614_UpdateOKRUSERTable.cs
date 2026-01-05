using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateOKRUSERTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "OkrUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "OkrUsers");

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
        }
    }
}
