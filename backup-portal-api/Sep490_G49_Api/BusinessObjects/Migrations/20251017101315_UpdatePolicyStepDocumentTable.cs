using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdatePolicyStepDocumentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "PolicyStepDepartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 10, 13, 14, 551, DateTimeKind.Utc).AddTicks(7594));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 17, 10, 13, 14, 551, DateTimeKind.Utc).AddTicks(6759), new DateTime(2025, 10, 17, 10, 13, 14, 551, DateTimeKind.Utc).AddTicks(6759) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 17, 10, 13, 14, 551, DateTimeKind.Utc).AddTicks(6761), new DateTime(2025, 10, 17, 10, 13, 14, 551, DateTimeKind.Utc).AddTicks(6761) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 17, 10, 13, 14, 551, DateTimeKind.Utc).AddTicks(6762), new DateTime(2025, 10, 17, 10, 13, 14, 551, DateTimeKind.Utc).AddTicks(6762) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 17, 10, 13, 14, 551, DateTimeKind.Utc).AddTicks(6734), new DateTime(2025, 10, 17, 10, 13, 14, 551, DateTimeKind.Utc).AddTicks(6736) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 17, 10, 13, 14, 551, DateTimeKind.Utc).AddTicks(6764), new DateTime(2025, 10, 17, 10, 13, 14, 551, DateTimeKind.Utc).AddTicks(6764) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 10, 17, 17, 13, 14, 551, DateTimeKind.Local).AddTicks(7541), new byte[] { 94, 78, 112, 123, 164, 136, 236, 156, 214, 226, 223, 147, 212, 183, 125, 149, 80, 251, 198, 106, 249, 250, 158, 128, 80, 214, 8, 104, 183, 236, 3, 205, 120, 155, 179, 31, 66, 81, 49, 112, 103, 74, 15, 224, 29, 232, 14, 156, 14, 47, 230, 220, 38, 32, 255, 196, 189, 55, 32, 145, 111, 208, 121, 49 }, new byte[] { 61, 52, 166, 143, 243, 12, 229, 250, 118, 161, 16, 209, 234, 120, 221, 187, 109, 255, 169, 70, 55, 251, 36, 188, 181, 109, 160, 84, 167, 141, 50, 100, 96, 193, 105, 144, 215, 24, 148, 203, 127, 72, 137, 0, 52, 169, 226, 157, 13, 109, 162, 6, 251, 97, 227, 81, 81, 220, 29, 205, 71, 157, 160, 87, 251, 156, 104, 106, 64, 143, 76, 163, 43, 119, 56, 48, 52, 109, 9, 167, 180, 66, 22, 112, 129, 239, 59, 82, 194, 241, 58, 184, 227, 244, 210, 175, 24, 206, 3, 231, 221, 223, 249, 68, 249, 235, 244, 78, 73, 172, 154, 150, 73, 136, 248, 106, 112, 91, 35, 198, 40, 191, 30, 44, 232, 12, 214, 231 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "PolicyStepDepartments");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 16, 6, 38, 6, 647, DateTimeKind.Utc).AddTicks(2634));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 16, 6, 38, 6, 647, DateTimeKind.Utc).AddTicks(1698), new DateTime(2025, 10, 16, 6, 38, 6, 647, DateTimeKind.Utc).AddTicks(1698) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 16, 6, 38, 6, 647, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 10, 16, 6, 38, 6, 647, DateTimeKind.Utc).AddTicks(1700) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 16, 6, 38, 6, 647, DateTimeKind.Utc).AddTicks(1702), new DateTime(2025, 10, 16, 6, 38, 6, 647, DateTimeKind.Utc).AddTicks(1702) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 16, 6, 38, 6, 647, DateTimeKind.Utc).AddTicks(1694), new DateTime(2025, 10, 16, 6, 38, 6, 647, DateTimeKind.Utc).AddTicks(1695) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 16, 6, 38, 6, 647, DateTimeKind.Utc).AddTicks(1703), new DateTime(2025, 10, 16, 6, 38, 6, 647, DateTimeKind.Utc).AddTicks(1704) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 10, 16, 13, 38, 6, 647, DateTimeKind.Local).AddTicks(2569), new byte[] { 4, 73, 225, 245, 201, 240, 51, 240, 104, 144, 126, 211, 39, 138, 88, 193, 120, 35, 53, 86, 177, 136, 131, 255, 74, 158, 192, 230, 99, 70, 68, 139, 104, 46, 176, 231, 43, 178, 4, 147, 187, 88, 148, 165, 69, 59, 204, 155, 5, 222, 187, 121, 130, 52, 64, 181, 116, 205, 235, 230, 26, 253, 240, 49 }, new byte[] { 16, 160, 141, 125, 203, 19, 75, 69, 33, 110, 72, 144, 245, 87, 24, 96, 39, 116, 53, 58, 34, 89, 240, 210, 123, 216, 141, 186, 210, 81, 125, 235, 205, 11, 20, 113, 240, 189, 35, 164, 125, 142, 171, 141, 172, 183, 190, 209, 177, 227, 202, 136, 63, 81, 77, 28, 66, 229, 68, 39, 230, 211, 22, 75, 142, 174, 99, 2, 107, 149, 183, 140, 130, 199, 104, 148, 254, 234, 238, 130, 114, 197, 126, 13, 93, 216, 47, 89, 181, 177, 21, 147, 233, 12, 194, 88, 171, 73, 253, 210, 189, 185, 155, 99, 102, 15, 46, 241, 249, 127, 74, 65, 237, 157, 53, 78, 232, 37, 243, 217, 165, 40, 45, 17, 102, 135, 198, 66 } });
        }
    }
}
