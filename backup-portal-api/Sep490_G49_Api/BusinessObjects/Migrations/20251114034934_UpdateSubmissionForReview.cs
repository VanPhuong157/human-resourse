using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateSubmissionForReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentReviewerId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NextReviewerId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(6363));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5404), new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5405) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5406), new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5406) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5407), new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5408) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5399), new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5401) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5409), new DateTime(2025, 11, 14, 3, 49, 33, 997, DateTimeKind.Utc).AddTicks(5409) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 11, 14, 10, 49, 33, 997, DateTimeKind.Local).AddTicks(6280), new byte[] { 234, 155, 160, 59, 250, 39, 34, 245, 189, 81, 161, 198, 173, 35, 3, 232, 212, 84, 139, 143, 29, 23, 160, 141, 48, 221, 140, 12, 36, 204, 117, 255, 3, 40, 130, 251, 176, 106, 80, 29, 144, 58, 139, 121, 158, 15, 230, 59, 240, 57, 82, 10, 44, 181, 232, 204, 6, 6, 164, 203, 253, 24, 148, 80 }, new byte[] { 135, 202, 58, 226, 157, 204, 189, 79, 153, 149, 13, 245, 216, 140, 112, 92, 213, 157, 187, 152, 108, 141, 64, 92, 27, 136, 129, 135, 77, 146, 59, 212, 170, 108, 77, 225, 139, 186, 185, 168, 194, 235, 62, 51, 82, 172, 29, 114, 65, 102, 34, 32, 89, 178, 126, 139, 63, 5, 183, 245, 37, 198, 117, 80, 71, 245, 179, 113, 86, 247, 6, 103, 189, 98, 63, 150, 120, 74, 172, 209, 82, 181, 87, 188, 119, 116, 35, 167, 3, 214, 196, 79, 19, 23, 27, 110, 177, 241, 51, 198, 205, 237, 234, 135, 14, 63, 200, 84, 48, 132, 58, 186, 38, 219, 186, 1, 212, 13, 21, 41, 46, 75, 94, 170, 249, 132, 221, 195 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentReviewerId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "NextReviewerId",
                table: "Submissions");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 3, 48, 48, 425, DateTimeKind.Utc).AddTicks(1512));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 48, 48, 425, DateTimeKind.Utc).AddTicks(526), new DateTime(2025, 11, 14, 3, 48, 48, 425, DateTimeKind.Utc).AddTicks(526) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 48, 48, 425, DateTimeKind.Utc).AddTicks(528), new DateTime(2025, 11, 14, 3, 48, 48, 425, DateTimeKind.Utc).AddTicks(528) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 48, 48, 425, DateTimeKind.Utc).AddTicks(529), new DateTime(2025, 11, 14, 3, 48, 48, 425, DateTimeKind.Utc).AddTicks(529) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 48, 48, 425, DateTimeKind.Utc).AddTicks(522), new DateTime(2025, 11, 14, 3, 48, 48, 425, DateTimeKind.Utc).AddTicks(524) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 11, 14, 3, 48, 48, 425, DateTimeKind.Utc).AddTicks(531), new DateTime(2025, 11, 14, 3, 48, 48, 425, DateTimeKind.Utc).AddTicks(531) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 11, 14, 10, 48, 48, 425, DateTimeKind.Local).AddTicks(1458), new byte[] { 41, 190, 211, 7, 12, 109, 201, 201, 150, 82, 40, 82, 242, 139, 141, 205, 30, 48, 237, 24, 231, 152, 224, 109, 209, 178, 94, 169, 166, 113, 20, 212, 164, 160, 124, 174, 176, 168, 150, 208, 33, 177, 125, 72, 231, 26, 148, 172, 151, 196, 194, 212, 25, 135, 66, 120, 164, 140, 35, 170, 213, 110, 70, 168 }, new byte[] { 248, 18, 5, 102, 254, 48, 23, 24, 173, 145, 198, 210, 10, 111, 87, 7, 214, 213, 86, 242, 16, 0, 56, 116, 144, 43, 133, 242, 107, 0, 92, 135, 147, 142, 73, 68, 124, 207, 206, 54, 44, 133, 13, 94, 134, 19, 127, 137, 78, 142, 101, 50, 118, 131, 85, 217, 39, 133, 91, 139, 38, 187, 76, 179, 223, 17, 245, 249, 129, 158, 202, 146, 186, 50, 169, 103, 251, 129, 63, 121, 138, 73, 158, 175, 159, 11, 195, 215, 141, 194, 97, 15, 194, 57, 47, 8, 159, 188, 90, 66, 120, 25, 78, 209, 202, 233, 73, 112, 186, 220, 125, 190, 53, 195, 235, 98, 48, 173, 85, 165, 211, 244, 21, 209, 223, 192, 73, 39 } });
        }
    }
}
