using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddPolicyStepDocumentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoredPath",
                table: "SubmissionFiles",
                newName: "StoragePath");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "SubmissionFiles",
                newName: "UploadedAt");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "SubmissionFiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "SubmissionFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelectedForPublish",
                table: "SubmissionFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PublishedDocumentId",
                table: "SubmissionFiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UploadedBy",
                table: "SubmissionFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ApproveDate",
                table: "PolicySteps",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExecDate",
                table: "PolicySteps",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LawRef",
                table: "PolicySteps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewDate",
                table: "PolicySteps",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PolicyDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    StoragePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyDocument_PolicySteps_PolicyStepId",
                        column: x => x.PolicyStepId,
                        principalTable: "PolicySteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionFiles_PublishedDocumentId",
                table: "SubmissionFiles",
                column: "PublishedDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDocument_PolicyStepId",
                table: "PolicyDocument",
                column: "PolicyStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionFiles_PolicyDocument_PublishedDocumentId",
                table: "SubmissionFiles",
                column: "PublishedDocumentId",
                principalTable: "PolicyDocument",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionFiles_PolicyDocument_PublishedDocumentId",
                table: "SubmissionFiles");

            migrationBuilder.DropTable(
                name: "PolicyDocument");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionFiles_PublishedDocumentId",
                table: "SubmissionFiles");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "SubmissionFiles");

            migrationBuilder.DropColumn(
                name: "IsSelectedForPublish",
                table: "SubmissionFiles");

            migrationBuilder.DropColumn(
                name: "PublishedDocumentId",
                table: "SubmissionFiles");

            migrationBuilder.DropColumn(
                name: "UploadedBy",
                table: "SubmissionFiles");

            migrationBuilder.DropColumn(
                name: "ApproveDate",
                table: "PolicySteps");

            migrationBuilder.DropColumn(
                name: "ExecDate",
                table: "PolicySteps");

            migrationBuilder.DropColumn(
                name: "LawRef",
                table: "PolicySteps");

            migrationBuilder.DropColumn(
                name: "ReviewDate",
                table: "PolicySteps");

            migrationBuilder.RenameColumn(
                name: "UploadedAt",
                table: "SubmissionFiles",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "StoragePath",
                table: "SubmissionFiles",
                newName: "StoredPath");

            migrationBuilder.AlterColumn<string>(
                name: "ContentType",
                table: "SubmissionFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 10, 1, 59, 0, 907, DateTimeKind.Utc).AddTicks(682));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9930), new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9931) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9932), new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9933) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9934), new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9934) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9926), new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9928) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9936), new DateTime(2025, 10, 10, 1, 59, 0, 906, DateTimeKind.Utc).AddTicks(9937) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 10, 10, 8, 59, 0, 907, DateTimeKind.Local).AddTicks(631), new byte[] { 173, 48, 145, 191, 226, 21, 221, 24, 84, 177, 228, 107, 54, 97, 20, 73, 198, 1, 159, 44, 227, 212, 54, 192, 113, 134, 72, 72, 181, 190, 229, 54, 215, 208, 54, 60, 82, 155, 60, 84, 239, 94, 216, 148, 154, 135, 63, 5, 206, 159, 87, 43, 58, 153, 204, 195, 95, 200, 197, 159, 72, 119, 154, 153 }, new byte[] { 67, 122, 64, 208, 95, 155, 186, 136, 227, 18, 89, 173, 244, 190, 108, 110, 124, 108, 158, 8, 135, 31, 166, 218, 187, 98, 215, 78, 138, 84, 19, 130, 55, 46, 148, 240, 9, 85, 49, 70, 175, 145, 124, 87, 111, 194, 173, 218, 193, 121, 156, 109, 18, 41, 174, 119, 93, 89, 205, 200, 6, 85, 81, 218, 59, 163, 86, 40, 0, 39, 227, 32, 120, 19, 169, 40, 22, 156, 46, 235, 182, 19, 19, 141, 110, 48, 138, 17, 172, 139, 210, 201, 40, 52, 212, 6, 121, 232, 86, 199, 235, 80, 110, 8, 80, 168, 39, 215, 65, 199, 97, 109, 173, 95, 38, 55, 123, 143, 21, 96, 215, 177, 110, 192, 173, 160, 39, 117 } });
        }
    }
}
