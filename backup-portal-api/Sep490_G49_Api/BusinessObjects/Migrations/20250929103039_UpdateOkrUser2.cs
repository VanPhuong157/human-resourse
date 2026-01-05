using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateOkrUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OkrUsers",
                table: "OkrUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "OkrUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OkrUsers",
                table: "OkrUsers",
                columns: new[] { "OkrId", "UserId", "Role" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OkrUsers",
                table: "OkrUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "OkrUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OkrUsers",
                table: "OkrUsers",
                columns: new[] { "OkrId", "UserId" });

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
    }
}
