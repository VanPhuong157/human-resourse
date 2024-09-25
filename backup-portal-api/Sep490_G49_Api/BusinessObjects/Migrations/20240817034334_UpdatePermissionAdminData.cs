using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdatePermissionAdminData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.InsertData(
    table: "RolePermissions",
    columns: new[] { "Id", "RoleId", "PermissionId", "IsEnabled" },
    values: new object[,]
    {
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("8a587770-c2c8-4eca-93a2-9e165b909d7b"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("0e593329-b61a-4ec2-a43d-878fa01a5867"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("e477af98-e937-40b1-aee9-c53c8a91d954"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("6b6d688a-40b8-48f1-9e20-5762ad427715"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("d192acc4-1ee7-4cf7-838e-0680076a78c3"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("01ce805d-e56f-4837-9f27-f4e31e67a22e"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("c6df77f3-81a5-4c57-b116-53a7f21c4986"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("84f5598f-98b9-4c82-b212-b63efca554f2"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("cbbcc181-d1fb-4970-9949-777a1a3ce387"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("d7811916-2ffc-426a-8561-3013225cf9e2"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("ded1da72-2523-41f4-a651-330ca8804e3b"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("4d77c298-d02f-420f-ad98-936e37f26ec1"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("18b6356d-2bb2-4525-baab-214afaf13faf"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("5b96eab3-85fe-4d9f-b625-db72fe1d8455"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("f2fbfa3c-a3f9-4909-830c-7fe16999dc5c"), true }
    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 17, 3, 38, 32, 93, DateTimeKind.Utc).AddTicks(362));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8357), new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8357) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8362), new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8362) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8365), new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8366) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8343), new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8350) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8371), new DateTime(2024, 8, 17, 3, 38, 32, 92, DateTimeKind.Utc).AddTicks(8372) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 17, 10, 38, 32, 93, DateTimeKind.Local).AddTicks(232), new byte[] { 224, 249, 237, 235, 21, 84, 239, 246, 212, 53, 111, 218, 32, 42, 197, 245, 127, 246, 129, 223, 172, 132, 231, 40, 35, 169, 251, 233, 210, 183, 237, 250, 123, 64, 216, 39, 152, 213, 155, 15, 88, 237, 73, 6, 184, 54, 7, 42, 1, 245, 11, 138, 59, 60, 149, 173, 19, 186, 59, 16, 118, 207, 80, 181 }, new byte[] { 202, 188, 60, 79, 67, 27, 186, 130, 106, 145, 136, 197, 226, 0, 62, 205, 121, 96, 139, 205, 198, 20, 73, 128, 27, 204, 50, 57, 41, 144, 26, 33, 143, 29, 189, 206, 227, 154, 222, 27, 132, 142, 62, 83, 104, 179, 142, 227, 146, 77, 73, 17, 78, 9, 157, 164, 156, 77, 123, 52, 185, 175, 94, 161, 247, 204, 164, 203, 238, 228, 13, 79, 51, 117, 153, 200, 237, 185, 186, 96, 176, 45, 121, 207, 55, 116, 26, 53, 113, 56, 152, 130, 31, 5, 5, 46, 63, 53, 33, 3, 37, 85, 197, 51, 199, 96, 66, 90, 33, 78, 47, 93, 190, 152, 62, 228, 139, 36, 114, 101, 201, 221, 212, 232, 231, 155, 205, 35 } });
            migrationBuilder.DeleteData(
            table: "RolePermissions",
            keyColumns: new[] { "RoleId", "PermissionId" },
            keyValues: new object[,]
            {
                { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("8a587770-c2c8-4eca-93a2-9e165b909d7b"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("0e593329-b61a-4ec2-a43d-878fa01a5867"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("e477af98-e937-40b1-aee9-c53c8a91d954"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("6b6d688a-40b8-48f1-9e20-5762ad427715"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("d192acc4-1ee7-4cf7-838e-0680076a78c3"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("01ce805d-e56f-4837-9f27-f4e31e67a22e"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("c6df77f3-81a5-4c57-b116-53a7f21c4986"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("84f5598f-98b9-4c82-b212-b63efca554f2"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("cbbcc181-d1fb-4970-9949-777a1a3ce387"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("d7811916-2ffc-426a-8561-3013225cf9e2"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("ded1da72-2523-41f4-a651-330ca8804e3b"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("4d77c298-d02f-420f-ad98-936e37f26ec1"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("18b6356d-2bb2-4525-baab-214afaf13faf"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("5b96eab3-85fe-4d9f-b625-db72fe1d8455"), true },
        { new Guid("00000000-0000-0000-0000-000000000000"), Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), Guid.Parse("f2fbfa3c-a3f9-4909-830c-7fe16999dc5c"), true }
    });
        }
    }
}
