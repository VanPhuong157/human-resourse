using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddRoleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_JobPosts_JobPostId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_OKRs_Departments_DepartmentId",
                table: "OKRs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "OKRs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "JobPostId",
                table: "Candidates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 15, 10, 38, 24, 44, DateTimeKind.Utc).AddTicks(9543));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "Type", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), new DateTime(2024, 8, 15, 10, 38, 24, 44, DateTimeKind.Utc).AddTicks(7853), new Guid("00000000-0000-0000-0000-000000000000"), "Human Resources.", "HR", "Basic", new DateTime(2024, 8, 15, 10, 38, 24, 44, DateTimeKind.Utc).AddTicks(7853), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"), new DateTime(2024, 8, 15, 10, 38, 24, 44, DateTimeKind.Utc).AddTicks(7855), new Guid("00000000-0000-0000-0000-000000000000"), "Employees.", "Employee", "Basic", new DateTime(2024, 8, 15, 10, 38, 24, 44, DateTimeKind.Utc).AddTicks(7855), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), new DateTime(2024, 8, 15, 10, 38, 24, 44, DateTimeKind.Utc).AddTicks(7857), new Guid("00000000-0000-0000-0000-000000000000"), "Board of Directors", "BOD", "Basic", new DateTime(2024, 8, 15, 10, 38, 24, 44, DateTimeKind.Utc).AddTicks(7858), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new DateTime(2024, 8, 15, 10, 38, 24, 44, DateTimeKind.Utc).AddTicks(7850), new Guid("00000000-0000-0000-0000-000000000000"), "Administrator role with all permissions.", "Admin", "Basic", new DateTime(2024, 8, 15, 10, 38, 24, 44, DateTimeKind.Utc).AddTicks(7851), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), new DateTime(2024, 8, 15, 10, 38, 24, 44, DateTimeKind.Utc).AddTicks(7859), new Guid("00000000-0000-0000-0000-000000000000"), "Manager Of Department", "Manager", "Basic", new DateTime(2024, 8, 15, 10, 38, 24, 44, DateTimeKind.Utc).AddTicks(7860), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "UserInformations",
                columns: new[] { "Id", "AcademicLevel", "Address", "AddressOfBirth", "AddressOfProvide", "Avatar", "BankingNo", "Code", "CreatedAt", "CreatedBy", "DateOfBirth", "DateOfProvide", "DriverLicenseIssueDate", "DriverLicenseIssuePlace", "DriverLicenseNo", "Email", "Ethnic", "FullName", "Gender", "HealthyStatus", "HiCode", "HomeTown", "IdCardNo", "IsPartyMember", "IsUnionMember", "MatitalStatus", "Note", "PassportIssuedAddress", "PassportIssuedDate", "PassportNo", "PhoneNumber", "PitCode", "Religious", "SiCode", "Status", "TypeOfWork", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[] { new Guid("a852b906-1eac-44ba-bcbe-4f43ad62af11"), null, null, null, null, null, null, "ADMIN", null, null, null, null, null, null, null, null, null, "AdminSHR", null, null, null, null, null, false, false, null, null, null, null, null, null, null, null, null, "Active", null, null, null, new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 15, 17, 38, 24, 44, DateTimeKind.Local).AddTicks(9475), new byte[] { 40, 170, 30, 121, 116, 121, 73, 243, 112, 4, 166, 233, 182, 211, 228, 48, 142, 57, 145, 120, 152, 253, 2, 16, 171, 221, 19, 118, 173, 185, 243, 143, 136, 149, 186, 115, 6, 122, 229, 196, 32, 25, 212, 85, 166, 148, 76, 131, 54, 180, 10, 2, 149, 22, 242, 250, 182, 217, 160, 198, 235, 27, 99, 186 }, new byte[] { 11, 122, 101, 234, 235, 249, 235, 242, 53, 245, 82, 66, 229, 31, 138, 230, 29, 214, 71, 143, 53, 5, 122, 104, 98, 171, 59, 158, 140, 159, 24, 189, 48, 42, 144, 235, 85, 74, 196, 49, 251, 190, 198, 133, 152, 24, 231, 172, 25, 186, 195, 82, 223, 145, 98, 161, 73, 140, 165, 116, 148, 32, 198, 128, 127, 225, 181, 40, 216, 102, 226, 203, 136, 204, 201, 200, 187, 253, 146, 39, 74, 100, 79, 199, 145, 140, 187, 90, 46, 66, 228, 33, 195, 177, 68, 95, 135, 54, 79, 38, 5, 81, 179, 239, 168, 10, 107, 20, 196, 141, 25, 214, 78, 84, 223, 178, 40, 111, 241, 160, 229, 224, 119, 45, 37, 33, 189, 229 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_JobPosts_JobPostId",
                table: "Candidates",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OKRs_Departments_DepartmentId",
                table: "OKRs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_JobPosts_JobPostId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_OKRs_Departments_DepartmentId",
                table: "OKRs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5659d538-3a19-4c5a-aec8-2c024cba0f05"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d4e0498-36dd-478c-ab11-b28c25e9da0f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"));

            migrationBuilder.DeleteData(
                table: "UserInformations",
                keyColumn: "Id",
                keyValue: new Guid("a852b906-1eac-44ba-bcbe-4f43ad62af11"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "OKRs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "JobPostId",
                table: "Candidates",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 10, 10, 51, 51, 904, DateTimeKind.Utc).AddTicks(3076));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "Type", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new DateTime(2024, 8, 10, 10, 51, 51, 904, DateTimeKind.Utc).AddTicks(668), new Guid("00000000-0000-0000-0000-000000000000"), "Administrator role with all permissions.", "Admin", "Basic", new DateTime(2024, 8, 10, 10, 51, 51, 904, DateTimeKind.Utc).AddTicks(669), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                columns: new[] { "CreatedAt", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2024, 8, 10, 17, 51, 51, 904, DateTimeKind.Local).AddTicks(2978), new byte[] { 122, 88, 158, 180, 224, 192, 195, 41, 26, 252, 89, 190, 151, 159, 136, 174, 91, 28, 67, 22, 57, 62, 29, 231, 229, 100, 140, 132, 221, 109, 49, 113, 136, 5, 132, 96, 158, 62, 231, 100, 121, 52, 64, 187, 90, 247, 10, 231, 51, 129, 21, 80, 111, 255, 167, 148, 76, 97, 162, 9, 66, 6, 219, 2 }, new byte[] { 75, 138, 28, 6, 24, 110, 184, 157, 221, 229, 121, 64, 168, 72, 28, 204, 182, 87, 162, 105, 47, 56, 4, 198, 251, 63, 228, 130, 87, 17, 32, 64, 52, 176, 102, 167, 142, 31, 77, 244, 9, 236, 188, 23, 126, 156, 241, 1, 120, 246, 67, 69, 71, 40, 68, 215, 165, 99, 196, 60, 48, 210, 71, 115, 142, 76, 173, 200, 224, 123, 194, 233, 158, 251, 157, 138, 185, 242, 101, 228, 146, 197, 134, 211, 23, 198, 95, 158, 143, 138, 91, 141, 105, 145, 194, 239, 132, 93, 72, 184, 66, 182, 42, 6, 109, 35, 3, 32, 255, 249, 183, 152, 117, 147, 255, 14, 136, 60, 143, 165, 56, 142, 111, 84, 113, 242, 220, 228 } });

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_JobPosts_JobPostId",
                table: "Candidates",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OKRs_Departments_DepartmentId",
                table: "OKRs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
