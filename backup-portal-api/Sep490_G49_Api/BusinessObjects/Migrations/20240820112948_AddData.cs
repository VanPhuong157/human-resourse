using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("5b96eab3-85fe-4d9f-b625-db72fe1d8455"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6b6d688a-40b8-48f1-9e20-5762ad427715"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8a587770-c2c8-4eca-93a2-9e165b909d7b"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d192acc4-1ee7-4cf7-838e-0680076a78c3"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e477af98-e937-40b1-aee9-c53c8a91d954"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 20, 11, 29, 46, 687, DateTimeKind.Utc).AddTicks(5408));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("380c2e30-9242-4401-879f-3756ad0156ef"),
                column: "Name",
                value: "Employee:UpdatePosition");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c6df77f3-81a5-4c57-b116-53a7f21c4986"),
                column: "Name",
                value: "Employee:UpdateStatus");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2df7174c-c270-4021-9e86-69faa72086a1"), "Okr:EditProgress" },
                    { new Guid("3f4456ad-6589-4896-89b6-a18e4d8b7086"), "Okr:History" },
                    { new Guid("4550e1f6-57c0-4b9f-8362-4562bfa88826"), "Okr:EditOwner" },
                    { new Guid("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"), "OkrRequest:Detail" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "Id", "IsEnabled" },
                values: new object[,]
                {
                    { new Guid("01ce805d-e56f-4837-9f27-f4e31e67a22e"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("c6df77f3-81a5-4c57-b116-53a7f21c4986"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), new Guid("00000000-0000-0000-0000-000000000000"), true }
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

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "Id", "IsEnabled" },
                values: new object[,]
                {
                    { new Guid("2df7174c-c270-4021-9e86-69faa72086a1"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("4550e1f6-57c0-4b9f-8362-4562bfa88826"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("2df7174c-c270-4021-9e86-69faa72086a1"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("3f4456ad-6589-4896-89b6-a18e4d8b7086"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("2df7174c-c270-4021-9e86-69faa72086a1"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("3f4456ad-6589-4896-89b6-a18e4d8b7086"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("4550e1f6-57c0-4b9f-8362-4562bfa88826"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("2df7174c-c270-4021-9e86-69faa72086a1"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("3f4456ad-6589-4896-89b6-a18e4d8b7086"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("4550e1f6-57c0-4b9f-8362-4562bfa88826"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), new Guid("00000000-0000-0000-0000-000000000000"), true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01ce805d-e56f-4837-9f27-f4e31e67a22e"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2df7174c-c270-4021-9e86-69faa72086a1"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4550e1f6-57c0-4b9f-8362-4562bfa88826"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c6df77f3-81a5-4c57-b116-53a7f21c4986"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2df7174c-c270-4021-9e86-69faa72086a1"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("3f4456ad-6589-4896-89b6-a18e4d8b7086"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2df7174c-c270-4021-9e86-69faa72086a1"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("3f4456ad-6589-4896-89b6-a18e4d8b7086"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4550e1f6-57c0-4b9f-8362-4562bfa88826"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2df7174c-c270-4021-9e86-69faa72086a1"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("3f4456ad-6589-4896-89b6-a18e4d8b7086"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4550e1f6-57c0-4b9f-8362-4562bfa88826"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2df7174c-c270-4021-9e86-69faa72086a1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3f4456ad-6589-4896-89b6-a18e4d8b7086"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4550e1f6-57c0-4b9f-8362-4562bfa88826"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 19, 4, 5, 33, 190, DateTimeKind.Utc).AddTicks(995));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("380c2e30-9242-4401-879f-3756ad0156ef"),
                column: "Name",
                value: "Employee:UpdateRole");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c6df77f3-81a5-4c57-b116-53a7f21c4986"),
                column: "Name",
                value: "Employee:Update");

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "Id", "IsEnabled" },
                values: new object[,]
                {
                    { new Guid("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("4f9068dd-6f69-483f-97fa-77a79b59edf9"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("5b96eab3-85fe-4d9f-b625-db72fe1d8455"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("6b6d688a-40b8-48f1-9e20-5762ad427715"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("8a587770-c2c8-4eca-93a2-9e165b909d7b"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("d192acc4-1ee7-4cf7-838e-0680076a78c3"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("e477af98-e937-40b1-aee9-c53c8a91d954"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true }
                });

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
        }
    }
}
