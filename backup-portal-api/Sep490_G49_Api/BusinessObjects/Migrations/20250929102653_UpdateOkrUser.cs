using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateOkrUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "OkrUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 9, 29, 10, 26, 52, 873, DateTimeKind.Utc).AddTicks(5899));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 29, 10, 26, 52, 873, DateTimeKind.Utc).AddTicks(5113), new DateTime(2025, 9, 29, 10, 26, 52, 873, DateTimeKind.Utc).AddTicks(5113) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 29, 10, 26, 52, 873, DateTimeKind.Utc).AddTicks(5115), new DateTime(2025, 9, 29, 10, 26, 52, 873, DateTimeKind.Utc).AddTicks(5115) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 29, 10, 26, 52, 873, DateTimeKind.Utc).AddTicks(5116), new DateTime(2025, 9, 29, 10, 26, 52, 873, DateTimeKind.Utc).AddTicks(5117) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 29, 10, 26, 52, 873, DateTimeKind.Utc).AddTicks(5108), new DateTime(2025, 9, 29, 10, 26, 52, 873, DateTimeKind.Utc).AddTicks(5109) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 29, 10, 26, 52, 873, DateTimeKind.Utc).AddTicks(5118), new DateTime(2025, 9, 29, 10, 26, 52, 873, DateTimeKind.Utc).AddTicks(5118) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 9, 29, 17, 26, 52, 873, DateTimeKind.Local).AddTicks(5846), new byte[] { 100, 233, 143, 123, 187, 45, 110, 158, 71, 206, 221, 170, 188, 68, 20, 162, 139, 14, 192, 238, 66, 219, 54, 212, 41, 210, 180, 220, 123, 246, 34, 224, 240, 46, 4, 19, 234, 57, 221, 187, 176, 255, 12, 223, 145, 222, 212, 32, 207, 245, 176, 88, 145, 19, 189, 18, 87, 28, 195, 111, 207, 221, 179, 210 }, new byte[] { 229, 191, 153, 89, 185, 147, 3, 0, 26, 20, 127, 239, 101, 215, 81, 147, 16, 26, 238, 185, 200, 84, 204, 138, 134, 89, 124, 194, 163, 51, 156, 90, 228, 227, 39, 214, 39, 120, 9, 193, 162, 90, 32, 36, 14, 116, 119, 11, 27, 210, 155, 27, 220, 30, 184, 198, 11, 45, 57, 214, 252, 5, 165, 227, 126, 194, 209, 38, 119, 53, 185, 69, 161, 105, 14, 106, 86, 19, 207, 182, 0, 135, 147, 199, 72, 95, 238, 53, 235, 123, 63, 132, 36, 77, 81, 147, 176, 97, 155, 114, 193, 111, 155, 228, 111, 110, 131, 203, 31, 124, 32, 156, 55, 77, 18, 214, 30, 251, 133, 214, 138, 250, 209, 232, 32, 71, 189, 179 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "OkrUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }
    }
}
