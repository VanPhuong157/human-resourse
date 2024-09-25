using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddPermissionCommon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 10, 10, 37, 768, DateTimeKind.Utc).AddTicks(3934));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), "EmployeeInformation:Detail" },
                    { new Guid("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"), "Common" }
                });

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

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "Id", "IsEnabled" },
                values: new object[,]
                {
                    { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), new Guid("00000000-0000-0000-0000-000000000000"), true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 3, 43, 33, 887, DateTimeKind.Utc).AddTicks(5054));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 43, 33, 887, DateTimeKind.Utc).AddTicks(4050), new DateTime(2024, 8, 17, 3, 43, 33, 887, DateTimeKind.Utc).AddTicks(4051) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 43, 33, 887, DateTimeKind.Utc).AddTicks(4052), new DateTime(2024, 8, 17, 3, 43, 33, 887, DateTimeKind.Utc).AddTicks(4053) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 43, 33, 887, DateTimeKind.Utc).AddTicks(4054), new DateTime(2024, 8, 17, 3, 43, 33, 887, DateTimeKind.Utc).AddTicks(4055) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 43, 33, 887, DateTimeKind.Utc).AddTicks(4047), new DateTime(2024, 8, 17, 3, 43, 33, 887, DateTimeKind.Utc).AddTicks(4048) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 43, 33, 887, DateTimeKind.Utc).AddTicks(4056), new DateTime(2024, 8, 17, 3, 43, 33, 887, DateTimeKind.Utc).AddTicks(4057) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 17, 10, 43, 33, 887, DateTimeKind.Local).AddTicks(4987), new byte[] { 109, 148, 136, 13, 44, 55, 92, 16, 84, 181, 236, 158, 243, 239, 183, 209, 76, 48, 35, 29, 148, 60, 221, 243, 95, 223, 177, 36, 235, 233, 250, 139, 23, 249, 108, 202, 155, 243, 222, 203, 223, 42, 119, 212, 63, 36, 160, 182, 244, 142, 81, 25, 23, 203, 209, 155, 162, 85, 43, 88, 191, 9, 87, 129 }, new byte[] { 117, 105, 114, 66, 241, 161, 41, 232, 76, 101, 163, 8, 209, 160, 58, 105, 0, 21, 91, 1, 165, 69, 129, 184, 75, 61, 120, 84, 3, 157, 151, 211, 183, 126, 172, 86, 188, 24, 49, 236, 12, 100, 135, 252, 253, 244, 34, 124, 252, 14, 64, 194, 229, 161, 26, 204, 48, 134, 27, 41, 157, 151, 215, 64, 117, 118, 175, 250, 91, 111, 129, 245, 193, 117, 205, 123, 152, 178, 90, 152, 223, 56, 176, 133, 41, 167, 189, 158, 108, 11, 167, 220, 176, 180, 68, 251, 16, 78, 98, 199, 87, 237, 11, 121, 218, 69, 65, 41, 59, 153, 158, 84, 25, 25, 190, 109, 77, 20, 61, 77, 185, 225, 16, 83, 46, 144, 185, 27 } });
        }
    }
}
