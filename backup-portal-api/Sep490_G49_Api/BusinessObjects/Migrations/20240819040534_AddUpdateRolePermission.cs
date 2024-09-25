using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddUpdateRolePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 19, 4, 5, 33, 190, DateTimeKind.Utc).AddTicks(995));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("380c2e30-9242-4401-879f-3756ad0156ef"), "Employee:UpdateRole" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 19, 4, 5, 33, 189, DateTimeKind.Utc).AddTicks(9541), new DateTime(2024, 8, 19, 4, 5, 33, 189, DateTimeKind.Utc).AddTicks(9541) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 19, 4, 5, 33, 189, DateTimeKind.Utc).AddTicks(9544), new DateTime(2024, 8, 19, 4, 5, 33, 189, DateTimeKind.Utc).AddTicks(9546) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 19, 4, 5, 33, 189, DateTimeKind.Utc).AddTicks(9548), new DateTime(2024, 8, 19, 4, 5, 33, 189, DateTimeKind.Utc).AddTicks(9548) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 19, 4, 5, 33, 189, DateTimeKind.Utc).AddTicks(9536), new DateTime(2024, 8, 19, 4, 5, 33, 189, DateTimeKind.Utc).AddTicks(9537) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 19, 4, 5, 33, 189, DateTimeKind.Utc).AddTicks(9550), new DateTime(2024, 8, 19, 4, 5, 33, 189, DateTimeKind.Utc).AddTicks(9551) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 5, 33, 190, DateTimeKind.Local).AddTicks(904), new byte[] { 144, 140, 40, 124, 134, 254, 164, 103, 39, 18, 160, 115, 150, 131, 80, 93, 22, 76, 252, 172, 178, 173, 254, 85, 116, 97, 93, 10, 150, 206, 169, 111, 251, 13, 252, 30, 1, 165, 217, 240, 44, 52, 145, 185, 97, 237, 211, 1, 174, 112, 170, 12, 127, 215, 115, 182, 76, 126, 20, 254, 108, 156, 28, 153 }, new byte[] { 110, 235, 210, 193, 218, 108, 131, 110, 93, 81, 69, 71, 166, 108, 35, 227, 172, 191, 92, 235, 19, 30, 18, 95, 81, 92, 45, 231, 244, 175, 51, 80, 254, 40, 5, 210, 252, 236, 55, 221, 202, 150, 125, 26, 18, 111, 76, 61, 214, 31, 235, 121, 248, 225, 130, 14, 158, 104, 30, 118, 153, 96, 163, 50, 35, 216, 239, 177, 124, 84, 237, 76, 140, 236, 187, 150, 158, 192, 5, 154, 91, 186, 101, 56, 17, 116, 48, 98, 172, 25, 185, 196, 94, 86, 8, 215, 12, 174, 90, 252, 80, 174, 31, 0, 9, 145, 158, 209, 242, 227, 32, 166, 202, 157, 227, 60, 36, 63, 121, 72, 124, 134, 248, 226, 126, 5, 240, 95 } });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "Id", "IsEnabled" },
                values: new object[] { new Guid("380c2e30-9242-4401-879f-3756ad0156ef"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("380c2e30-9242-4401-879f-3756ad0156ef"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("380c2e30-9242-4401-879f-3756ad0156ef"));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 10, 10, 37, 768, DateTimeKind.Utc).AddTicks(3934));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 10, 10, 37, 768, DateTimeKind.Utc).AddTicks(2725), new DateTime(2024, 8, 17, 10, 10, 37, 768, DateTimeKind.Utc).AddTicks(2725) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 10, 10, 37, 768, DateTimeKind.Utc).AddTicks(2727), new DateTime(2024, 8, 17, 10, 10, 37, 768, DateTimeKind.Utc).AddTicks(2727) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 10, 10, 37, 768, DateTimeKind.Utc).AddTicks(2730), new DateTime(2024, 8, 17, 10, 10, 37, 768, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 10, 10, 37, 768, DateTimeKind.Utc).AddTicks(2721), new DateTime(2024, 8, 17, 10, 10, 37, 768, DateTimeKind.Utc).AddTicks(2722) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 10, 10, 37, 768, DateTimeKind.Utc).AddTicks(2732), new DateTime(2024, 8, 17, 10, 10, 37, 768, DateTimeKind.Utc).AddTicks(2732) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 17, 17, 10, 37, 768, DateTimeKind.Local).AddTicks(3863), new byte[] { 22, 36, 122, 93, 119, 87, 85, 165, 68, 14, 99, 21, 80, 147, 154, 87, 253, 13, 166, 161, 130, 144, 103, 238, 184, 101, 174, 162, 58, 66, 82, 124, 19, 144, 209, 16, 195, 64, 80, 113, 30, 113, 254, 95, 190, 248, 41, 167, 125, 190, 106, 123, 174, 110, 127, 98, 115, 191, 99, 175, 51, 166, 205, 235 }, new byte[] { 179, 243, 102, 42, 60, 124, 88, 222, 165, 36, 147, 255, 4, 120, 237, 66, 2, 232, 98, 181, 251, 244, 241, 8, 91, 74, 58, 32, 175, 254, 91, 255, 205, 3, 203, 53, 69, 49, 37, 114, 142, 98, 100, 193, 151, 229, 204, 124, 216, 144, 208, 201, 60, 81, 65, 242, 123, 145, 220, 171, 66, 182, 46, 44, 96, 201, 252, 158, 149, 234, 173, 181, 57, 70, 128, 38, 187, 16, 129, 59, 204, 81, 189, 251, 182, 187, 202, 14, 144, 2, 185, 143, 15, 11, 6, 92, 234, 61, 120, 222, 250, 195, 226, 8, 130, 48, 135, 75, 25, 22, 37, 125, 253, 84, 143, 84, 106, 43, 63, 151, 164, 247, 5, 54, 172, 42, 101, 62 } });
        }
    }
}
