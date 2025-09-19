using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateNewDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "HomePageReasons");

            migrationBuilder.DropTable(
                name: "HomePages");

            migrationBuilder.DropTable(
                name: "JobPosts");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9926));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9176), new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9176) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9178), new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9178) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9179), new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9180) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9172), new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9174) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9201), new DateTime(2025, 9, 11, 2, 25, 11, 764, DateTimeKind.Utc).AddTicks(9201) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 9, 11, 9, 25, 11, 764, DateTimeKind.Local).AddTicks(9877), new byte[] { 154, 150, 20, 36, 42, 43, 152, 92, 211, 239, 140, 160, 149, 216, 199, 160, 175, 124, 222, 34, 46, 211, 136, 130, 119, 186, 181, 40, 156, 247, 76, 111, 0, 73, 81, 215, 86, 143, 45, 89, 197, 234, 89, 172, 143, 144, 151, 30, 240, 123, 88, 74, 171, 113, 88, 67, 170, 61, 205, 122, 5, 188, 169, 233 }, new byte[] { 87, 150, 154, 83, 20, 152, 26, 2, 70, 98, 39, 240, 173, 151, 240, 202, 28, 170, 228, 6, 190, 174, 231, 118, 6, 175, 112, 235, 9, 164, 202, 25, 56, 57, 224, 224, 121, 91, 89, 195, 168, 221, 248, 173, 198, 202, 167, 150, 205, 36, 205, 49, 183, 39, 111, 217, 168, 251, 214, 8, 149, 1, 196, 9, 26, 135, 111, 137, 114, 216, 19, 247, 80, 196, 240, 3, 70, 158, 153, 97, 162, 203, 212, 160, 243, 249, 15, 219, 195, 233, 74, 187, 157, 50, 21, 99, 212, 232, 80, 217, 97, 176, 179, 164, 61, 46, 57, 150, 3, 24, 158, 223, 245, 35, 129, 77, 9, 93, 210, 118, 249, 174, 74, 236, 254, 233, 231, 99 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomePageReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageBackgroundDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageBackgroundPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusJobPost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleBody = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Benefits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperienceYear = table.Column<double>(type: "float", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfRecruits = table.Column<int>(type: "int", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobPosts_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CvDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateApply = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_JobPosts_JobPostId",
                        column: x => x.JobPostId,
                        principalTable: "JobPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(9041));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7504), new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7505) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7510), new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7519), new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7519) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7497), new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7499) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7524), new DateTime(2024, 8, 20, 11, 58, 50, 772, DateTimeKind.Utc).AddTicks(7524) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 20, 18, 58, 50, 772, DateTimeKind.Local).AddTicks(8923), new byte[] { 50, 196, 87, 83, 139, 235, 8, 170, 69, 28, 89, 199, 235, 44, 2, 98, 234, 12, 147, 65, 20, 233, 91, 44, 84, 184, 46, 129, 245, 241, 35, 208, 24, 66, 79, 213, 150, 141, 253, 15, 137, 239, 4, 100, 71, 151, 123, 13, 33, 171, 150, 138, 106, 192, 49, 212, 71, 223, 157, 47, 71, 100, 180, 198 }, new byte[] { 246, 123, 250, 246, 120, 110, 131, 221, 110, 8, 19, 218, 40, 48, 197, 227, 195, 158, 57, 70, 24, 110, 254, 187, 19, 116, 248, 100, 255, 89, 201, 92, 241, 3, 85, 204, 180, 48, 67, 208, 119, 16, 162, 109, 243, 81, 126, 171, 1, 58, 181, 127, 62, 38, 221, 196, 9, 182, 218, 17, 114, 253, 68, 134, 229, 103, 35, 93, 181, 117, 175, 12, 245, 253, 44, 1, 40, 64, 51, 115, 34, 98, 153, 243, 59, 29, 123, 254, 166, 16, 17, 172, 246, 210, 32, 63, 4, 102, 232, 212, 96, 249, 25, 207, 221, 228, 220, 205, 144, 74, 52, 25, 77, 84, 173, 234, 181, 200, 137, 118, 12, 77, 249, 107, 98, 221, 79, 46 } });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_JobPostId",
                table: "Candidates",
                column: "JobPostId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPosts_DepartmentId",
                table: "JobPosts",
                column: "DepartmentId");
        }
    }
}
