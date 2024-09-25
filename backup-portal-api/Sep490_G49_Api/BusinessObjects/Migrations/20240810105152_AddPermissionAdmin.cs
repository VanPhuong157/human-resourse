using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddPermissionAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 10, 10, 51, 51, 904, DateTimeKind.Utc).AddTicks(3076));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("f2fbfa3c-a3f9-4909-830c-7fe16999dc5c"), "Admin" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 10, 10, 51, 51, 904, DateTimeKind.Utc).AddTicks(668), new DateTime(2024, 8, 10, 10, 51, 51, 904, DateTimeKind.Utc).AddTicks(669) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 10, 17, 51, 51, 904, DateTimeKind.Local).AddTicks(2978), new byte[] { 122, 88, 158, 180, 224, 192, 195, 41, 26, 252, 89, 190, 151, 159, 136, 174, 91, 28, 67, 22, 57, 62, 29, 231, 229, 100, 140, 132, 221, 109, 49, 113, 136, 5, 132, 96, 158, 62, 231, 100, 121, 52, 64, 187, 90, 247, 10, 231, 51, 129, 21, 80, 111, 255, 167, 148, 76, 97, 162, 9, 66, 6, 219, 2 }, new byte[] { 75, 138, 28, 6, 24, 110, 184, 157, 221, 229, 121, 64, 168, 72, 28, 204, 182, 87, 162, 105, 47, 56, 4, 198, 251, 63, 228, 130, 87, 17, 32, 64, 52, 176, 102, 167, 142, 31, 77, 244, 9, 236, 188, 23, 126, 156, 241, 1, 120, 246, 67, 69, 71, 40, 68, 215, 165, 99, 196, 60, 48, 210, 71, 115, 142, 76, 173, 200, 224, 123, 194, 233, 158, 251, 157, 138, 185, 242, 101, 228, 146, 197, 134, 211, 23, 198, 95, 158, 143, 138, 91, 141, 105, 145, 194, 239, 132, 93, 72, 184, 66, 182, 42, 6, 109, 35, 3, 32, 255, 249, 183, 152, 117, 147, 255, 14, 136, 60, 143, 165, 56, 142, 111, 84, 113, 242, 220, 228 } });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "Id", "IsEnabled" },
                values: new object[] { new Guid("f2fbfa3c-a3f9-4909-830c-7fe16999dc5c"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f2fbfa3c-a3f9-4909-830c-7fe16999dc5c"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f2fbfa3c-a3f9-4909-830c-7fe16999dc5c"));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 6, 3, 42, 46, 511, DateTimeKind.Utc).AddTicks(1312));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 6, 3, 42, 46, 510, DateTimeKind.Utc).AddTicks(9753), new DateTime(2024, 8, 6, 3, 42, 46, 510, DateTimeKind.Utc).AddTicks(9754) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 6, 10, 42, 46, 511, DateTimeKind.Local).AddTicks(1244), new byte[] { 150, 173, 47, 148, 171, 122, 20, 41, 246, 10, 4, 129, 5, 116, 122, 63, 73, 200, 202, 98, 118, 157, 240, 75, 201, 191, 240, 188, 128, 160, 17, 83, 26, 31, 54, 215, 174, 165, 134, 192, 60, 239, 138, 210, 88, 127, 108, 74, 68, 116, 182, 227, 132, 168, 216, 160, 36, 62, 96, 205, 152, 128, 99, 14 }, new byte[] { 14, 14, 205, 33, 50, 251, 45, 34, 57, 183, 76, 67, 5, 67, 24, 99, 8, 195, 74, 135, 155, 189, 5, 65, 21, 193, 172, 144, 14, 106, 67, 134, 167, 146, 8, 14, 107, 98, 232, 202, 208, 166, 138, 105, 208, 48, 107, 190, 29, 6, 218, 35, 48, 15, 116, 23, 80, 173, 250, 30, 244, 205, 74, 48, 196, 102, 55, 36, 32, 232, 200, 101, 115, 193, 22, 132, 253, 26, 94, 186, 245, 37, 136, 36, 60, 47, 109, 13, 93, 157, 97, 229, 163, 86, 92, 123, 116, 122, 43, 69, 157, 169, 77, 64, 194, 78, 83, 58, 118, 136, 180, 135, 53, 227, 10, 249, 70, 200, 53, 126, 77, 13, 100, 169, 167, 9, 91, 65 } });
        }
    }
}
