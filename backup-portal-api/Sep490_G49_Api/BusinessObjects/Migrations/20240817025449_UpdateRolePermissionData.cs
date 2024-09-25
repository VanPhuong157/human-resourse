using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateRolePermissionData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 2, 54, 47, 978, DateTimeKind.Utc).AddTicks(1900));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6b6d688a-40b8-48f1-9e20-5762ad427715"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8a587770-c2c8-4eca-93a2-9e165b909d7b"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d192acc4-1ee7-4cf7-838e-0680076a78c3"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e477af98-e937-40b1-aee9-c53c8a91d954"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01ce805d-e56f-4837-9f27-f4e31e67a22e"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("5b96eab3-85fe-4d9f-b625-db72fe1d8455"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6b6d688a-40b8-48f1-9e20-5762ad427715"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8a587770-c2c8-4eca-93a2-9e165b909d7b"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c6df77f3-81a5-4c57-b116-53a7f21c4986"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d192acc4-1ee7-4cf7-838e-0680076a78c3"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e477af98-e937-40b1-aee9-c53c8a91d954"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01ce805d-e56f-4837-9f27-f4e31e67a22e"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("5b96eab3-85fe-4d9f-b625-db72fe1d8455"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6b6d688a-40b8-48f1-9e20-5762ad427715"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8a587770-c2c8-4eca-93a2-9e165b909d7b"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c6df77f3-81a5-4c57-b116-53a7f21c4986"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d192acc4-1ee7-4cf7-838e-0680076a78c3"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e477af98-e937-40b1-aee9-c53c8a91d954"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 2, 43, 56, 779, DateTimeKind.Utc).AddTicks(4428));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6b6d688a-40b8-48f1-9e20-5762ad427715"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8a587770-c2c8-4eca-93a2-9e165b909d7b"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d192acc4-1ee7-4cf7-838e-0680076a78c3"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e477af98-e937-40b1-aee9-c53c8a91d954"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"), new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01ce805d-e56f-4837-9f27-f4e31e67a22e"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("5b96eab3-85fe-4d9f-b625-db72fe1d8455"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6b6d688a-40b8-48f1-9e20-5762ad427715"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8a587770-c2c8-4eca-93a2-9e165b909d7b"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c6df77f3-81a5-4c57-b116-53a7f21c4986"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d192acc4-1ee7-4cf7-838e-0680076a78c3"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e477af98-e937-40b1-aee9-c53c8a91d954"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01ce805d-e56f-4837-9f27-f4e31e67a22e"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("5b96eab3-85fe-4d9f-b625-db72fe1d8455"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6b6d688a-40b8-48f1-9e20-5762ad427715"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8a587770-c2c8-4eca-93a2-9e165b909d7b"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c6df77f3-81a5-4c57-b116-53a7f21c4986"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d192acc4-1ee7-4cf7-838e-0680076a78c3"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e477af98-e937-40b1-aee9-c53c8a91d954"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d") },
                column: "IsEnabled",
                value: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 2, 43, 56, 779, DateTimeKind.Utc).AddTicks(2840), new DateTime(2024, 8, 17, 2, 43, 56, 779, DateTimeKind.Utc).AddTicks(2841) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 2, 43, 56, 779, DateTimeKind.Utc).AddTicks(2843), new DateTime(2024, 8, 17, 2, 43, 56, 779, DateTimeKind.Utc).AddTicks(2844) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 2, 43, 56, 779, DateTimeKind.Utc).AddTicks(2846), new DateTime(2024, 8, 17, 2, 43, 56, 779, DateTimeKind.Utc).AddTicks(2847) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 2, 43, 56, 779, DateTimeKind.Utc).AddTicks(2828), new DateTime(2024, 8, 17, 2, 43, 56, 779, DateTimeKind.Utc).AddTicks(2831) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 2, 43, 56, 779, DateTimeKind.Utc).AddTicks(2850), new DateTime(2024, 8, 17, 2, 43, 56, 779, DateTimeKind.Utc).AddTicks(2851) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 17, 9, 43, 56, 779, DateTimeKind.Local).AddTicks(4349), new byte[] { 194, 20, 196, 121, 156, 32, 124, 231, 73, 154, 215, 17, 213, 48, 216, 103, 237, 177, 42, 83, 126, 18, 166, 80, 9, 30, 127, 4, 228, 184, 60, 239, 118, 136, 77, 194, 138, 115, 94, 46, 31, 12, 25, 183, 180, 180, 88, 52, 217, 23, 223, 22, 193, 172, 46, 73, 42, 82, 244, 146, 71, 133, 169, 219 }, new byte[] { 254, 92, 70, 109, 169, 211, 171, 159, 84, 32, 238, 230, 163, 156, 161, 17, 226, 130, 223, 139, 142, 95, 247, 136, 227, 46, 50, 1, 250, 167, 19, 239, 213, 181, 121, 164, 248, 31, 98, 114, 100, 201, 35, 124, 241, 182, 245, 133, 13, 245, 121, 217, 95, 56, 161, 197, 117, 179, 63, 175, 175, 39, 212, 170, 42, 5, 169, 82, 214, 126, 174, 196, 13, 25, 196, 19, 213, 207, 136, 223, 4, 78, 183, 32, 239, 114, 198, 154, 126, 143, 55, 85, 232, 70, 115, 84, 156, 221, 163, 237, 254, 22, 35, 41, 191, 15, 57, 198, 174, 118, 133, 135, 15, 230, 236, 1, 32, 181, 134, 19, 96, 182, 140, 188, 188, 79, 207, 11 } });
        }
    }
}
