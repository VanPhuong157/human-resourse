using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddPermissionRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("00366e72-8a35-4980-88ee-89fbf87d8ad4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0f34cec5-cce9-43a5-bf8e-e7d53d07f821"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1181d8aa-6042-41dd-b7b1-ae2a04257f10"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("159a1bc3-ac08-4e04-b74f-54e449325c0b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1efa98c2-5f08-4c52-9dd5-7de7a0fcd810"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("204ba08b-0c5a-43fc-84a4-bcdbeb774e42"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2c13067b-8e54-4df8-9c0e-14b1e06a7b3b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3fa999d6-2710-49e3-872c-1b29f2f8be1c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4f508c55-579e-43c7-803d-7b19e68d1d37"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5a81c484-3893-477a-9b6b-b67952b0c6bf"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("86248324-147f-4772-bb69-511b28223914"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("883ea601-e752-4089-b1e3-17f2e1f8fefc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8f3bfe96-b2b5-4899-978c-8f201f2be4d3"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9a8eac2e-9cbf-4ea7-8985-acd2c7b2f916"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a29694c8-47bb-4230-be13-877aa3a95d0d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("b69b3bb2-28a7-483f-a382-d858db1b6cb7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c094056f-54c6-42dd-9ce1-9a133da5632f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c34ac4ce-6fd0-49d6-b1a0-15cfacec2918"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c93db57d-6a90-4cac-b45d-1bb51eaa21db"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e330dc18-f000-433f-81ee-a7f2529712b0"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f31adb72-90cf-4d42-9ef1-052a0b330f22"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("fbbb9747-b287-45bb-ad1a-96815f789723"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ffb4d399-2ed8-4129-ba55-44fa59f9f995"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e67ef8a7-129c-4df6-903a-ce6a0f5833fb"));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 6, 3, 42, 46, 511, DateTimeKind.Utc).AddTicks(1312));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("01ce805d-e56f-4837-9f27-f4e31e67a22e"), "Employee:List" },
                    { new Guid("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), "Candidate:List" },
                    { new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), "EmployeeHistory:List" },
                    { new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), "OkrRequest:Edit" },
                    { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), "Candidate:Create" },
                    { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), "OkrRequest:Create" },
                    { new Guid("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), "Employee:Edit" },
                    { new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"), "OkrRequest:List" },
                    { new Guid("5b96eab3-85fe-4d9f-b625-db72fe1d8455"), "OkrRequest:Update" },
                    { new Guid("6b6d688a-40b8-48f1-9e20-5762ad427715"), "JobPost:Create" },
                    { new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"), "EmployeeFamily:List" },
                    { new Guid("8a587770-c2c8-4eca-93a2-9e165b909d7b"), "Candidate:Update" },
                    { new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), "EmployeeFamily:Edit" },
                    { new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), "Okr:Detail" },
                    { new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), "EmployeeFamily:Delete" },
                    { new Guid("c6df77f3-81a5-4c57-b116-53a7f21c4986"), "Employee:Update" },
                    { new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"), "EmployeeFamily:Create" },
                    { new Guid("d192acc4-1ee7-4cf7-838e-0680076a78c3"), "JobPost:Update" },
                    { new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"), "Okr:List" },
                    { new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"), "Okr:Comment" },
                    { new Guid("e477af98-e937-40b1-aee9-c53c8a91d954"), "JobPost:Detail" },
                    { new Guid("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"), "Employee:Create" },
                    { new Guid("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), "JobPost:List" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 6, 3, 42, 46, 510, DateTimeKind.Utc).AddTicks(9753), new DateTime(2024, 8, 6, 3, 42, 46, 510, DateTimeKind.Utc).AddTicks(9754) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessToken", "AccessTokenCreated", "CreatedAt", "CreatedBy", "DepartmentId", "IsDeleted", "PasswordHash", "PasswordSalt", "RefreshToken", "RefreshTokenCreated", "RefreshTokenExpires", "RoleId", "Status", "TemporaryPasswordExpires", "TemporaryPasswordHash", "TemporaryPasswordSalt", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 6, 10, 42, 46, 511, DateTimeKind.Local).AddTicks(1244), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"), false, new byte[] { 150, 173, 47, 148, 171, 122, 20, 41, 246, 10, 4, 129, 5, 116, 122, 63, 73, 200, 202, 98, 118, 157, 240, 75, 201, 191, 240, 188, 128, 160, 17, 83, 26, 31, 54, 215, 174, 165, 134, 192, 60, 239, 138, 210, 88, 127, 108, 74, 68, 116, 182, 227, 132, 168, 216, 160, 36, 62, 96, 205, 152, 128, 99, 14 }, new byte[] { 14, 14, 205, 33, 50, 251, 45, 34, 57, 183, 76, 67, 5, 67, 24, 99, 8, 195, 74, 135, 155, 189, 5, 65, 21, 193, 172, 144, 14, 106, 67, 134, 167, 146, 8, 14, 107, 98, 232, 202, 208, 166, 138, 105, 208, 48, 107, 190, 29, 6, 218, 35, 48, 15, 116, 23, 80, 173, 250, 30, 244, 205, 74, 48, 196, 102, 55, 36, 32, 232, 200, 101, 115, 193, 22, 132, 253, 26, 94, 186, 245, 37, 136, 36, 60, 47, 109, 13, 93, 157, 97, 229, 163, 86, 92, 123, 116, 122, 43, 69, 157, 169, 77, 64, 194, 78, 83, 58, 118, 136, 180, 135, 53, 227, 10, 249, 70, 200, 53, 126, 77, 13, 100, 169, 167, 9, 91, 65 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), true, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "AdminSHR" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "Id", "IsEnabled" },
                values: new object[,]
                {
                    { new Guid("01ce805d-e56f-4837-9f27-f4e31e67a22e"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("5b96eab3-85fe-4d9f-b625-db72fe1d8455"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("6b6d688a-40b8-48f1-9e20-5762ad427715"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("8a587770-c2c8-4eca-93a2-9e165b909d7b"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("c6df77f3-81a5-4c57-b116-53a7f21c4986"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("d192acc4-1ee7-4cf7-838e-0680076a78c3"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("e477af98-e937-40b1-aee9-c53c8a91d954"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true },
                    { new Guid("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new Guid("00000000-0000-0000-0000-000000000000"), true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01ce805d-e56f-4837-9f27-f4e31e67a22e"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

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
                keyValues: new object[] { new Guid("c6df77f3-81a5-4c57-b116-53a7f21c4986"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

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
                keyValues: new object[] { new Guid("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("01ce805d-e56f-4837-9f27-f4e31e67a22e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("054ad0e5-acf7-4f83-815c-e5500bee5e6b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0829e08b-583e-4ac3-a349-e9d56ef71a9d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0a6608a6-b1f9-44cf-9e7d-8d033c548947"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0e593329-b61a-4ec2-a43d-878fa01a5867"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("18b6356d-2bb2-4525-baab-214afaf13faf"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4d77c298-d02f-420f-ad98-936e37f26ec1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5b96eab3-85fe-4d9f-b625-db72fe1d8455"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("6b6d688a-40b8-48f1-9e20-5762ad427715"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("84f5598f-98b9-4c82-b212-b63efca554f2"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8a587770-c2c8-4eca-93a2-9e165b909d7b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c6df77f3-81a5-4c57-b116-53a7f21c4986"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cbbcc181-d1fb-4970-9949-777a1a3ce387"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d192acc4-1ee7-4cf7-838e-0680076a78c3"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d7811916-2ffc-426a-8561-3013225cf9e2"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ded1da72-2523-41f4-a651-330ca8804e3b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e477af98-e937-40b1-aee9-c53c8a91d954"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 8, 47, 0, 263, DateTimeKind.Utc).AddTicks(1358));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("00366e72-8a35-4980-88ee-89fbf87d8ad4"), "Candidate:List" },
                    { new Guid("0f34cec5-cce9-43a5-bf8e-e7d53d07f821"), "JobPost:Create" },
                    { new Guid("1181d8aa-6042-41dd-b7b1-ae2a04257f10"), "Okr:Detail" },
                    { new Guid("159a1bc3-ac08-4e04-b74f-54e449325c0b"), "EmployeeFamily:Create" },
                    { new Guid("1efa98c2-5f08-4c52-9dd5-7de7a0fcd810"), "EmployeeFamily:Delete" },
                    { new Guid("204ba08b-0c5a-43fc-84a4-bcdbeb774e42"), "OkrRequest:Update" },
                    { new Guid("2c13067b-8e54-4df8-9c0e-14b1e06a7b3b"), "JobPost:List" },
                    { new Guid("3fa999d6-2710-49e3-872c-1b29f2f8be1c"), "Candidate:Update" },
                    { new Guid("4f508c55-579e-43c7-803d-7b19e68d1d37"), "OkrRequest:Edit" },
                    { new Guid("5a81c484-3893-477a-9b6b-b67952b0c6bf"), "EmployeeFamily:List" },
                    { new Guid("86248324-147f-4772-bb69-511b28223914"), "Okr:List" },
                    { new Guid("883ea601-e752-4089-b1e3-17f2e1f8fefc"), "OkrRequest:Create" },
                    { new Guid("8f3bfe96-b2b5-4899-978c-8f201f2be4d3"), "Employee:Update" },
                    { new Guid("9a8eac2e-9cbf-4ea7-8985-acd2c7b2f916"), "JobPost:Update" },
                    { new Guid("a29694c8-47bb-4230-be13-877aa3a95d0d"), "OkrRequest:List" },
                    { new Guid("b69b3bb2-28a7-483f-a382-d858db1b6cb7"), "Employee:List" },
                    { new Guid("c094056f-54c6-42dd-9ce1-9a133da5632f"), "Employee:Edit" },
                    { new Guid("c34ac4ce-6fd0-49d6-b1a0-15cfacec2918"), "EmployeeFamily:Edit" },
                    { new Guid("c93db57d-6a90-4cac-b45d-1bb51eaa21db"), "JobPost:Detail" },
                    { new Guid("e330dc18-f000-433f-81ee-a7f2529712b0"), "Employee:Create" },
                    { new Guid("f31adb72-90cf-4d42-9ef1-052a0b330f22"), "EmployeeHistory:List" },
                    { new Guid("fbbb9747-b287-45bb-ad1a-96815f789723"), "Okr:Comment" },
                    { new Guid("ffb4d399-2ed8-4129-ba55-44fa59f9f995"), "Candidate:Create" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 4, 8, 47, 0, 262, DateTimeKind.Utc).AddTicks(9814), new DateTime(2024, 8, 4, 8, 47, 0, 262, DateTimeKind.Utc).AddTicks(9818) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessToken", "AccessTokenCreated", "CreatedAt", "CreatedBy", "DepartmentId", "IsDeleted", "PasswordHash", "PasswordSalt", "RefreshToken", "RefreshTokenCreated", "RefreshTokenExpires", "RoleId", "Status", "TemporaryPasswordExpires", "TemporaryPasswordHash", "TemporaryPasswordSalt", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new Guid("e67ef8a7-129c-4df6-903a-ce6a0f5833fb"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 4, 15, 47, 0, 263, DateTimeKind.Local).AddTicks(1253), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"), false, new byte[] { 85, 184, 168, 95, 112, 39, 253, 41, 75, 132, 231, 234, 241, 245, 119, 174, 216, 201, 56, 30, 34, 244, 61, 70, 76, 140, 210, 64, 1, 143, 127, 60, 5, 84, 141, 154, 113, 100, 64, 149, 206, 245, 144, 99, 120, 35, 195, 152, 38, 163, 108, 152, 112, 162, 28, 125, 137, 211, 128, 207, 180, 235, 230, 213 }, new byte[] { 215, 192, 172, 10, 175, 143, 28, 234, 77, 172, 49, 41, 73, 24, 129, 145, 210, 142, 234, 189, 47, 17, 224, 166, 172, 237, 150, 196, 243, 73, 13, 226, 130, 59, 69, 232, 149, 44, 134, 160, 240, 70, 154, 134, 118, 142, 125, 7, 153, 230, 196, 106, 110, 19, 28, 97, 211, 132, 180, 103, 9, 201, 22, 177, 77, 215, 6, 0, 142, 232, 94, 200, 109, 212, 40, 75, 115, 64, 16, 32, 167, 154, 148, 111, 150, 170, 12, 221, 203, 218, 20, 181, 247, 107, 130, 66, 45, 204, 149, 1, 223, 99, 21, 204, 59, 208, 184, 231, 87, 94, 31, 66, 252, 234, 143, 82, 189, 213, 144, 172, 189, 233, 101, 87, 128, 49, 101, 235 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), true, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "AdminSHR" });
        }
    }
}
