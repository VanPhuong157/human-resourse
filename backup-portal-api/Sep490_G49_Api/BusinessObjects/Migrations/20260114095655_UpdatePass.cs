using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdatePass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 14, 9, 56, 54, 586, DateTimeKind.Utc).AddTicks(8243));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 9, 56, 54, 586, DateTimeKind.Utc).AddTicks(7309), new DateTime(2026, 1, 14, 9, 56, 54, 586, DateTimeKind.Utc).AddTicks(7309) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 9, 56, 54, 586, DateTimeKind.Utc).AddTicks(7311), new DateTime(2026, 1, 14, 9, 56, 54, 586, DateTimeKind.Utc).AddTicks(7311) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 9, 56, 54, 586, DateTimeKind.Utc).AddTicks(7313), new DateTime(2026, 1, 14, 9, 56, 54, 586, DateTimeKind.Utc).AddTicks(7313) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 9, 56, 54, 586, DateTimeKind.Utc).AddTicks(7304), new DateTime(2026, 1, 14, 9, 56, 54, 586, DateTimeKind.Utc).AddTicks(7306) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 14, 9, 56, 54, 586, DateTimeKind.Utc).AddTicks(7314), new DateTime(2026, 1, 14, 9, 56, 54, 586, DateTimeKind.Utc).AddTicks(7315) });

            migrationBuilder.UpdateData(
                table: "UserInformations",
                keyColumn: "Id",
                keyValue: new Guid("a852b906-1eac-44ba-bcbe-4f43ad62af11"),
                column: "UserId",
                value: new Guid("12240fc0-2a2a-4b45-9ba5-6a57667413c2"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("12240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2026, 1, 14, 16, 56, 54, 586, DateTimeKind.Local).AddTicks(8185), new byte[] { 177, 216, 194, 105, 81, 96, 220, 168, 203, 41, 135, 1, 40, 61, 112, 63, 74, 9, 207, 89, 53, 37, 65, 43, 114, 54, 142, 144, 234, 42, 217, 222, 27, 175, 214, 138, 84, 114, 255, 42, 77, 247, 177, 32, 237, 123, 58, 80, 10, 61, 86, 46, 44, 189, 139, 132, 241, 64, 159, 113, 35, 54, 177, 112 }, new byte[] { 196, 152, 191, 192, 19, 215, 161, 184, 9, 91, 136, 254, 34, 70, 126, 152, 201, 122, 104, 116, 188, 226, 162, 223, 245, 9, 180, 123, 85, 93, 166, 4, 226, 23, 226, 71, 181, 230, 23, 185, 112, 67, 158, 208, 117, 70, 31, 74, 243, 51, 42, 19, 18, 178, 217, 129, 187, 128, 166, 86, 219, 45, 200, 154, 101, 219, 184, 99, 214, 230, 8, 79, 232, 139, 120, 226, 114, 155, 46, 11, 65, 104, 250, 208, 192, 149, 193, 44, 188, 165, 215, 157, 104, 244, 206, 62, 18, 66, 36, 37, 152, 137, 119, 3, 109, 18, 18, 75, 99, 68, 109, 33, 167, 142, 102, 81, 240, 130, 148, 130, 2, 67, 159, 102, 97, 63, 189, 170 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "UserInformations",
                keyColumn: "Id",
                keyValue: new Guid("a852b906-1eac-44ba-bcbe-4f43ad62af11"),
                column: "UserId",
                value: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("12240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2026, 1, 14, 14, 51, 51, 188, DateTimeKind.Local).AddTicks(1128), new byte[] { 255, 98, 157, 159, 144, 66, 226, 143, 92, 5, 166, 122, 210, 152, 112, 23, 19, 165, 15, 66, 117, 197, 247, 198, 253, 176, 71, 46, 86, 74, 246, 136, 197, 81, 103, 33, 214, 153, 175, 229, 143, 41, 118, 209, 158, 127, 17, 1, 110, 39, 46, 141, 0, 126, 67, 37, 97, 67, 182, 91, 10, 11, 215, 15 }, new byte[] { 70, 103, 56, 237, 100, 116, 24, 140, 88, 67, 111, 23, 86, 204, 130, 229, 199, 156, 198, 100, 205, 60, 243, 86, 46, 222, 50, 132, 30, 57, 42, 19, 138, 194, 196, 89, 131, 186, 114, 152, 211, 132, 99, 221, 150, 63, 201, 56, 170, 215, 50, 12, 26, 254, 71, 81, 204, 86, 244, 3, 139, 98, 134, 5, 0, 84, 151, 179, 166, 174, 129, 199, 142, 241, 138, 208, 251, 250, 145, 212, 128, 142, 154, 132, 139, 64, 119, 33, 20, 51, 129, 220, 52, 118, 146, 39, 137, 164, 179, 4, 206, 131, 17, 55, 210, 93, 34, 105, 202, 228, 118, 84, 223, 11, 193, 50, 198, 49, 33, 223, 97, 180, 31, 182, 168, 233, 42, 22 } });
        }
    }
}
