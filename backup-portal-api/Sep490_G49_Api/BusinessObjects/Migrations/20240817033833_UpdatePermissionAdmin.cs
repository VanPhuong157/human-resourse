using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdatePermissionAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 3, 38, 32, 93, DateTimeKind.Utc).AddTicks(362));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8357), new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8357) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8362), new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8362) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8365), new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8366) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8343), new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8350) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8371), new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8372) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 17, 10, 38, 32, 93, DateTimeKind.Local).AddTicks(232), new byte[] { 224, 249, 237, 235, 21, 84, 239, 246, 212, 53, 111, 218, 32, 42, 197, 245, 127, 246, 129, 223, 172, 132, 231, 40, 35, 169, 251, 233, 210, 183, 237, 250, 123, 64, 216, 39, 152, 213, 155, 15, 88, 237, 73, 6, 184, 54, 7, 42, 1, 245, 11, 138, 59, 60, 149, 173, 19, 186, 59, 16, 118, 207, 80, 181 }, new byte[] { 202, 188, 60, 79, 67, 27, 186, 130, 106, 145, 136, 197, 226, 0, 62, 205, 121, 96, 139, 205, 198, 20, 73, 128, 27, 204, 50, 57, 41, 144, 26, 33, 143, 29, 189, 206, 227, 154, 222, 27, 132, 142, 62, 83, 104, 179, 142, 227, 146, 77, 73, 17, 78, 9, 157, 164, 156, 77, 123, 52, 185, 175, 94, 161, 247, 204, 164, 203, 238, 228, 13, 79, 51, 117, 153, 200, 237, 185, 186, 96, 176, 45, 121, 207, 55, 116, 26, 53, 113, 56, 152, 130, 31, 5, 5, 46, 63, 53, 33, 3, 37, 85, 197, 51, 199, 96, 66, 90, 33, 78, 47, 93, 190, 152, 62, 228, 139, 36, 114, 101, 201, 221, 212, 232, 231, 155, 205, 35 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
