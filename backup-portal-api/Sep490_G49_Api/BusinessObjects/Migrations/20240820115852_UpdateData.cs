using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(9041));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("9e89898e-ad66-4b6b-b607-1353d22dd71b"), "EmployeeInformation:Edit" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7504), new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7505) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7510), new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7519), new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7519) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7497), new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7499) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7524), new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7524) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 20, 18, 58, 50, 772, DateTimeKind.Local).AddTicks(8923), new byte[] { 50, 196, 87, 83, 139, 235, 8, 170, 69, 28, 89, 199, 235, 44, 2, 98, 234, 12, 147, 65, 20, 233, 91, 44, 84, 184, 46, 129, 245, 241, 35, 208, 24, 66, 79, 213, 150, 141, 253, 15, 137, 239, 4, 100, 71, 151, 123, 13, 33, 171, 150, 138, 106, 192, 49, 212, 71, 223, 157, 47, 71, 100, 180, 198 }, new byte[] { 246, 123, 250, 246, 120, 110, 131, 221, 110, 8, 19, 218, 40, 48, 197, 227, 195, 158, 57, 70, 24, 110, 254, 187, 19, 116, 248, 100, 255, 89, 201, 92, 241, 3, 85, 204, 180, 48, 67, 208, 119, 16, 162, 109, 243, 81, 126, 171, 1, 58, 181, 127, 62, 38, 221, 196, 9, 182, 218, 17, 114, 253, 68, 134, 229, 103, 35, 93, 181, 117, 175, 12, 245, 253, 44, 1, 40, 64, 51, 115, 34, 98, 153, 243, 59, 29, 123, 254, 166, 16, 17, 172, 246, 210, 32, 63, 4, 102, 232, 212, 96, 249, 25, 207, 221, 228, 220, 205, 144, 74, 52, 25, 77, 84, 173, 234, 181, 200, 137, 118, 12, 77, 249, 107, 98, 221, 79, 46 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9e89898e-ad66-4b6b-b607-1353d22dd71b"));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 20, 11, 29, 46, 687, DateTimeKind.Utc).AddTicks(5408));

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "Id", "IsEnabled" },
                values: new object[,]
                {
                    { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), new Guid("00000000-0000-0000-0000-000000000000"), true }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 29, 46, 687, DateTimeKind.Utc).AddTicks(4027), new DateTime(2024, 8, 20, 11, 29, 46, 687, DateTimeKind.Utc).AddTicks(4027) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 29, 46, 687, DateTimeKind.Utc).AddTicks(4031), new DateTime(2024, 8, 20, 11, 29, 46, 687, DateTimeKind.Utc).AddTicks(4031) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 29, 46, 687, DateTimeKind.Utc).AddTicks(4035), new DateTime(2024, 8, 20, 11, 29, 46, 687, DateTimeKind.Utc).AddTicks(4035) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 29, 46, 687, DateTimeKind.Utc).AddTicks(4020), new DateTime(2024, 8, 20, 11, 29, 46, 687, DateTimeKind.Utc).AddTicks(4022) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 29, 46, 687, DateTimeKind.Utc).AddTicks(4038), new DateTime(2024, 8, 20, 11, 29, 46, 687, DateTimeKind.Utc).AddTicks(4039) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 20, 18, 29, 46, 687, DateTimeKind.Local).AddTicks(5282), new byte[] { 29, 176, 230, 56, 130, 120, 230, 97, 78, 38, 36, 195, 13, 45, 68, 110, 41, 136, 185, 102, 122, 45, 93, 33, 66, 92, 127, 121, 40, 221, 9, 122, 65, 181, 201, 62, 220, 255, 25, 5, 193, 29, 173, 57, 27, 117, 184, 131, 250, 94, 14, 220, 253, 126, 95, 62, 192, 30, 129, 172, 237, 135, 219, 39 }, new byte[] { 65, 23, 196, 66, 213, 182, 85, 173, 87, 143, 201, 5, 188, 7, 192, 132, 132, 87, 212, 131, 134, 213, 246, 109, 211, 165, 131, 102, 253, 207, 122, 191, 239, 91, 229, 135, 110, 75, 208, 249, 246, 181, 42, 144, 130, 252, 117, 208, 119, 156, 178, 2, 58, 242, 215, 129, 147, 14, 250, 136, 71, 75, 58, 53, 211, 221, 43, 133, 107, 32, 171, 39, 139, 247, 234, 242, 226, 197, 169, 235, 98, 59, 78, 90, 22, 111, 205, 12, 35, 235, 81, 65, 182, 4, 57, 194, 197, 103, 32, 252, 228, 206, 106, 235, 242, 120, 191, 230, 248, 34, 122, 233, 255, 237, 153, 67, 203, 239, 106, 99, 182, 149, 72, 3, 143, 131, 41, 125 } });
        }
    }
}
