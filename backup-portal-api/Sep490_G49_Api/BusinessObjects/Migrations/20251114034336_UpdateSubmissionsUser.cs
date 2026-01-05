using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateSubmissionsUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TargetUserId",
                table: "SubmissionEvents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 3, 43, 35, 456, DateTimeKind.Utc).AddTicks(5028));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 43, 35, 456, DateTimeKind.Utc).AddTicks(3494), new DateTime(2025, 11, 14, 3, 43, 35, 456, DateTimeKind.Utc).AddTicks(3494) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 43, 35, 456, DateTimeKind.Utc).AddTicks(3496), new DateTime(2025, 11, 14, 3, 43, 35, 456, DateTimeKind.Utc).AddTicks(3496) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 43, 35, 456, DateTimeKind.Utc).AddTicks(3498), new DateTime(2025, 11, 14, 3, 43, 35, 456, DateTimeKind.Utc).AddTicks(3498) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 43, 35, 456, DateTimeKind.Utc).AddTicks(3486), new DateTime(2025, 11, 14, 3, 43, 35, 456, DateTimeKind.Utc).AddTicks(3491) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 43, 35, 456, DateTimeKind.Utc).AddTicks(3499), new DateTime(2025, 11, 14, 3, 43, 35, 456, DateTimeKind.Utc).AddTicks(3499) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 11, 14, 10, 43, 35, 456, DateTimeKind.Local).AddTicks(4940), new byte[] { 50, 66, 75, 239, 184, 77, 61, 89, 245, 102, 37, 96, 46, 87, 253, 25, 249, 238, 207, 172, 183, 64, 176, 207, 90, 74, 7, 136, 113, 169, 181, 29, 104, 43, 239, 127, 58, 218, 29, 58, 235, 174, 228, 101, 65, 144, 190, 178, 36, 236, 215, 12, 129, 209, 4, 195, 143, 234, 127, 28, 225, 108, 135, 71 }, new byte[] { 75, 88, 226, 71, 192, 1, 231, 67, 67, 188, 73, 166, 32, 50, 182, 144, 168, 157, 131, 12, 7, 22, 73, 241, 187, 236, 219, 216, 108, 212, 249, 34, 73, 108, 255, 89, 64, 19, 14, 205, 125, 97, 234, 98, 107, 69, 175, 232, 55, 53, 51, 160, 48, 37, 244, 123, 247, 163, 153, 73, 160, 218, 18, 161, 162, 50, 27, 237, 108, 252, 221, 255, 202, 28, 215, 252, 246, 240, 126, 199, 84, 3, 120, 84, 143, 184, 211, 215, 210, 127, 113, 225, 41, 165, 112, 121, 123, 169, 248, 154, 194, 55, 245, 103, 235, 109, 48, 100, 218, 207, 114, 93, 62, 205, 113, 44, 140, 59, 60, 30, 148, 85, 117, 115, 65, 11, 230, 182 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetUserId",
                table: "SubmissionEvents");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 3, 42, 37, 2, DateTimeKind.Utc).AddTicks(4343));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 42, 37, 2, DateTimeKind.Utc).AddTicks(3527), new DateTime(2025, 11, 14, 3, 42, 37, 2, DateTimeKind.Utc).AddTicks(3528) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 42, 37, 2, DateTimeKind.Utc).AddTicks(3551), new DateTime(2025, 11, 14, 3, 42, 37, 2, DateTimeKind.Utc).AddTicks(3551) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 42, 37, 2, DateTimeKind.Utc).AddTicks(3553), new DateTime(2025, 11, 14, 3, 42, 37, 2, DateTimeKind.Utc).AddTicks(3553) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 42, 37, 2, DateTimeKind.Utc).AddTicks(3523), new DateTime(2025, 11, 14, 3, 42, 37, 2, DateTimeKind.Utc).AddTicks(3524) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 42, 37, 2, DateTimeKind.Utc).AddTicks(3555), new DateTime(2025, 11, 14, 3, 42, 37, 2, DateTimeKind.Utc).AddTicks(3555) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 11, 14, 10, 42, 37, 2, DateTimeKind.Local).AddTicks(4287), new byte[] { 100, 220, 152, 49, 56, 0, 43, 37, 162, 241, 213, 45, 213, 54, 73, 184, 162, 46, 191, 183, 246, 231, 233, 46, 188, 215, 14, 204, 106, 102, 254, 69, 95, 77, 7, 128, 157, 191, 218, 244, 209, 161, 214, 59, 100, 0, 18, 5, 186, 249, 17, 60, 64, 82, 244, 204, 236, 201, 67, 115, 91, 62, 19, 91 }, new byte[] { 194, 106, 22, 68, 239, 153, 114, 225, 81, 68, 195, 50, 9, 143, 101, 171, 21, 1, 91, 236, 104, 184, 29, 47, 133, 107, 144, 89, 201, 255, 180, 169, 92, 198, 240, 174, 4, 15, 94, 216, 161, 64, 163, 11, 224, 249, 177, 209, 48, 44, 7, 163, 85, 88, 83, 242, 10, 245, 93, 27, 150, 11, 253, 39, 66, 141, 109, 183, 45, 219, 236, 165, 115, 203, 195, 23, 126, 180, 78, 169, 236, 128, 221, 39, 187, 173, 221, 109, 193, 75, 181, 102, 123, 128, 163, 203, 35, 29, 8, 188, 66, 2, 240, 44, 123, 8, 243, 28, 249, 4, 56, 244, 50, 97, 1, 179, 134, 107, 123, 156, 36, 253, 14, 44, 98, 131, 19, 46 } });
        }
    }
}
