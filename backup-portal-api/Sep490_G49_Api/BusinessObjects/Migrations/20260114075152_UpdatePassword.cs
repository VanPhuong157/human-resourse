using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdatePassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 7, 51, 51, 188, DateTimeKind.Utc).AddTicks(1288));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 7, 51, 51, 187, DateTimeKind.Utc).AddTicks(7909), new DateTime(2026, 1, 14, 7, 51, 51, 187, DateTimeKind.Utc).AddTicks(7909) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 7, 51, 51, 187, DateTimeKind.Utc).AddTicks(7911), new DateTime(2026, 1, 14, 7, 51, 51, 187, DateTimeKind.Utc).AddTicks(7911) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 7, 51, 51, 187, DateTimeKind.Utc).AddTicks(7913), new DateTime(2026, 1, 14, 7, 51, 51, 187, DateTimeKind.Utc).AddTicks(7913) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 7, 51, 51, 187, DateTimeKind.Utc).AddTicks(7905), new DateTime(2026, 1, 14, 7, 51, 51, 187, DateTimeKind.Utc).AddTicks(7906) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 7, 51, 51, 187, DateTimeKind.Utc).AddTicks(7914), new DateTime(2026, 1, 14, 7, 51, 51, 187, DateTimeKind.Utc).AddTicks(7915) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessToken", "AccessTokenCreated", "CreatedAt", "CreatedBy", "DepartmentId", "IsDeleted", "PasswordHash", "PasswordSalt", "RefreshToken", "RefreshTokenCreated", "RefreshTokenExpires", "RoleId", "Status", "TemporaryPasswordExpires", "TemporaryPasswordHash", "TemporaryPasswordSalt", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new Guid("12240fc0-2a2a-4b45-9ba5-6a57667413c2"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 14, 51, 51, 188, DateTimeKind.Local).AddTicks(1128), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"), false, new byte[] { 255, 98, 157, 159, 144, 66, 226, 143, 92, 5, 166, 122, 210, 152, 112, 23, 19, 165, 15, 66, 117, 197, 247, 198, 253, 176, 71, 46, 86, 74, 246, 136, 197, 81, 103, 33, 214, 153, 175, 229, 143, 41, 118, 209, 158, 127, 17, 1, 110, 39, 46, 141, 0, 126, 67, 37, 97, 67, 182, 91, 10, 11, 215, 15 }, new byte[] { 70, 103, 56, 237, 100, 116, 24, 140, 88, 67, 111, 23, 86, 204, 130, 229, 199, 156, 198, 100, 205, 60, 243, 86, 46, 222, 50, 132, 30, 57, 42, 19, 138, 194, 196, 89, 131, 186, 114, 152, 211, 132, 99, 221, 150, 63, 201, 56, 170, 215, 50, 12, 26, 254, 71, 81, 204, 86, 244, 3, 139, 98, 134, 5, 0, 84, 151, 179, 166, 174, 129, 199, 142, 241, 138, 208, 251, 250, 145, 212, 128, 142, 154, 132, 139, 64, 119, 33, 20, 51, 129, 220, 52, 118, 146, 39, 137, 164, 179, 4, 206, 131, 17, 55, 210, 93, 34, 105, 202, 228, 118, 84, 223, 11, 193, 50, 198, 49, 33, 223, 97, 180, 31, 182, 168, 233, 42, 22 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), true, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "AdminSHR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("12240fc0-2a2a-4b45-9ba5-6a57667413c2"));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 6, 10, 41, 132, DateTimeKind.Utc).AddTicks(7928));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 6, 10, 41, 132, DateTimeKind.Utc).AddTicks(7031), new DateTime(2026, 1, 14, 6, 10, 41, 132, DateTimeKind.Utc).AddTicks(7031) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 6, 10, 41, 132, DateTimeKind.Utc).AddTicks(7034), new DateTime(2026, 1, 14, 6, 10, 41, 132, DateTimeKind.Utc).AddTicks(7034) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 6, 10, 41, 132, DateTimeKind.Utc).AddTicks(7035), new DateTime(2026, 1, 14, 6, 10, 41, 132, DateTimeKind.Utc).AddTicks(7036) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 6, 10, 41, 132, DateTimeKind.Utc).AddTicks(7027), new DateTime(2026, 1, 14, 6, 10, 41, 132, DateTimeKind.Utc).AddTicks(7029) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 6, 10, 41, 132, DateTimeKind.Utc).AddTicks(7037), new DateTime(2026, 1, 14, 6, 10, 41, 132, DateTimeKind.Utc).AddTicks(7037) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessToken", "AccessTokenCreated", "CreatedAt", "CreatedBy", "DepartmentId", "IsDeleted", "PasswordHash", "PasswordSalt", "RefreshToken", "RefreshTokenCreated", "RefreshTokenExpires", "RoleId", "Status", "TemporaryPasswordExpires", "TemporaryPasswordHash", "TemporaryPasswordSalt", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 13, 10, 41, 132, DateTimeKind.Local).AddTicks(7872), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"), false, new byte[] { 68, 191, 163, 95, 184, 194, 39, 156, 232, 69, 151, 170, 29, 42, 249, 91, 232, 169, 35, 128, 123, 33, 128, 218, 225, 61, 88, 171, 213, 65, 131, 205, 253, 216, 169, 208, 164, 208, 187, 90, 159, 51, 43, 202, 182, 229, 44, 128, 147, 73, 140, 139, 178, 89, 150, 89, 9, 251, 77, 120, 72, 241, 171, 23 }, new byte[] { 251, 173, 253, 130, 187, 102, 127, 21, 124, 177, 203, 193, 188, 235, 133, 157, 11, 85, 124, 104, 223, 59, 180, 230, 83, 134, 227, 206, 112, 60, 113, 53, 126, 56, 73, 180, 149, 149, 170, 73, 141, 221, 25, 122, 131, 135, 66, 204, 56, 131, 17, 34, 92, 25, 72, 84, 146, 137, 128, 193, 57, 97, 74, 122, 114, 95, 161, 169, 91, 142, 58, 149, 93, 25, 249, 184, 76, 38, 95, 178, 198, 150, 82, 223, 34, 254, 55, 184, 113, 139, 229, 216, 181, 237, 177, 202, 140, 94, 14, 201, 140, 212, 72, 117, 0, 118, 101, 203, 250, 8, 77, 217, 90, 78, 71, 115, 166, 22, 80, 1, 20, 242, 66, 167, 91, 9, 3, 18 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), true, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "AdminSHR" });
        }
    }
}
