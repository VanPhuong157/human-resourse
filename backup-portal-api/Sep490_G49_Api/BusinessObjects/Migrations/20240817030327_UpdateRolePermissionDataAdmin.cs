using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateRolePermissionDataAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 3, 3, 26, 373, DateTimeKind.Utc).AddTicks(4370));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 3, 26, 373, DateTimeKind.Utc).AddTicks(3231), new DateTime(2024, 8, 17, 3, 3, 26, 373, DateTimeKind.Utc).AddTicks(3232) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 3, 26, 373, DateTimeKind.Utc).AddTicks(3233), new DateTime(2024, 8, 17, 3, 3, 26, 373, DateTimeKind.Utc).AddTicks(3234) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 3, 26, 373, DateTimeKind.Utc).AddTicks(3236), new DateTime(2024, 8, 17, 3, 3, 26, 373, DateTimeKind.Utc).AddTicks(3236) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 3, 26, 373, DateTimeKind.Utc).AddTicks(3229), new DateTime(2024, 8, 17, 3, 3, 26, 373, DateTimeKind.Utc).AddTicks(3229) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 3, 26, 373, DateTimeKind.Utc).AddTicks(3238), new DateTime(2024, 8, 17, 3, 3, 26, 373, DateTimeKind.Utc).AddTicks(3238) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 17, 10, 3, 26, 373, DateTimeKind.Local).AddTicks(4265), new byte[] { 5, 127, 93, 93, 108, 48, 10, 117, 35, 115, 75, 218, 1, 201, 103, 142, 131, 207, 60, 18, 29, 227, 159, 131, 128, 168, 184, 186, 29, 162, 100, 227, 15, 49, 132, 144, 15, 15, 225, 216, 15, 109, 158, 4, 188, 130, 150, 62, 176, 31, 130, 52, 227, 137, 102, 0, 75, 122, 203, 193, 15, 249, 22, 199 }, new byte[] { 157, 162, 87, 216, 180, 82, 206, 113, 67, 169, 153, 203, 117, 128, 50, 146, 158, 170, 51, 181, 97, 15, 127, 41, 69, 210, 7, 253, 134, 30, 3, 62, 37, 46, 51, 25, 161, 91, 104, 9, 90, 158, 244, 157, 179, 53, 171, 94, 108, 5, 33, 124, 217, 86, 235, 243, 108, 243, 139, 254, 1, 176, 11, 185, 97, 158, 150, 158, 204, 217, 136, 243, 179, 93, 54, 160, 80, 185, 5, 15, 94, 225, 120, 24, 113, 23, 186, 136, 154, 195, 249, 89, 36, 76, 37, 161, 117, 188, 12, 52, 82, 158, 78, 228, 60, 209, 206, 52, 175, 136, 143, 90, 12, 57, 104, 230, 196, 63, 143, 38, 135, 32, 66, 201, 150, 143, 121, 32 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 2, 54, 47, 978, DateTimeKind.Utc).AddTicks(1900));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 2, 54, 47, 977, DateTimeKind.Utc).AddTicks(9962), new DateTime(2024, 8, 17, 2, 54, 47, 977, DateTimeKind.Utc).AddTicks(9963) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 2, 54, 47, 977, DateTimeKind.Utc).AddTicks(9965), new DateTime(2024, 8, 17, 2, 54, 47, 977, DateTimeKind.Utc).AddTicks(9965) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 2, 54, 47, 977, DateTimeKind.Utc).AddTicks(9967), new DateTime(2024, 8, 17, 2, 54, 47, 977, DateTimeKind.Utc).AddTicks(9968) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 2, 54, 47, 977, DateTimeKind.Utc).AddTicks(9959), new DateTime(2024, 8, 17, 2, 54, 47, 977, DateTimeKind.Utc).AddTicks(9960) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 2, 54, 47, 977, DateTimeKind.Utc).AddTicks(9969), new DateTime(2024, 8, 17, 2, 54, 47, 977, DateTimeKind.Utc).AddTicks(9970) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 17, 9, 54, 47, 978, DateTimeKind.Local).AddTicks(1757), new byte[] { 88, 23, 14, 192, 61, 156, 231, 122, 246, 14, 153, 250, 183, 186, 227, 224, 221, 44, 211, 168, 145, 107, 176, 30, 177, 1, 177, 25, 224, 198, 53, 242, 114, 45, 198, 166, 226, 118, 133, 79, 207, 219, 113, 80, 207, 11, 36, 114, 31, 150, 54, 165, 122, 219, 116, 14, 241, 93, 83, 96, 102, 140, 25, 97 }, new byte[] { 36, 30, 114, 32, 12, 40, 95, 167, 94, 74, 115, 11, 198, 244, 98, 83, 215, 212, 107, 134, 112, 13, 195, 47, 99, 120, 58, 89, 50, 109, 38, 153, 5, 210, 91, 151, 11, 5, 239, 236, 233, 25, 160, 140, 16, 169, 12, 219, 151, 214, 32, 76, 215, 186, 82, 78, 20, 90, 139, 60, 128, 194, 86, 139, 105, 185, 144, 39, 210, 160, 169, 5, 8, 152, 34, 127, 164, 21, 6, 218, 39, 98, 141, 165, 171, 159, 161, 174, 29, 7, 15, 31, 116, 211, 189, 169, 182, 132, 2, 61, 26, 130, 230, 142, 44, 232, 102, 93, 65, 66, 218, 15, 252, 248, 119, 83, 210, 38, 242, 80, 227, 52, 254, 145, 238, 104, 64, 197 } });
        }
    }
}
