using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0fce1785-2d3d-4bfe-8200-0b71b322a66e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("22767788-1217-4c53-94ee-d13329a0bc8d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("29db4d9b-9e5a-48a0-8d8c-722ebbdee657"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("309a8350-a6e7-4937-883e-3dea63cbe6f2"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3684489b-07d1-4e7a-9ebf-a3980147e8d1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("37a5c132-e686-4731-ad25-745153a1e2fa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4fdf5d6a-01a8-4e3f-afdb-babeba8c0484"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("57bcd6a5-9777-496e-b397-6952779ba20a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5b7a6106-9c86-4e2b-9db1-5fc8fea69b4e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5be01598-e6d7-458b-a077-9161295273cd"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("692ed12a-cf89-4e54-a4e4-a7e17790600e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("711ee627-6351-4e4d-9e7b-a7e8e8d9f8e6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("782352d8-1ff2-43e6-abb1-d7e89a0a3500"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("7cadb29e-7b14-4d4c-a6f4-47510ed188ab"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8aa0e3c3-bc5c-49c2-90a5-95a4ea433ac2"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8e145d3d-3625-471d-932c-54601acb0047"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("929ca1a2-4a69-4bf0-86c5-aceec23072bc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9cba56c5-24d4-4f66-bf0c-05a069a7ba0d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("b5766e82-2cd5-4b75-89d4-c0d40cbaa5b5"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bb7b066b-5a80-4ae2-aa61-a171355dd6da"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bffc77c2-b7c0-47ac-8535-f97ee1730bb6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c17f0530-3172-4b18-b75d-cb6120938fe8"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d1574de7-7701-4af9-8f46-bfceb878e1a6"));

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"), new DateTime(2024, 8, 3, 15, 51, 40, 378, DateTimeKind.Utc).AddTicks(2979), new Guid("00000000-0000-0000-0000-000000000000"), "Admin", "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1244816d-a379-48f6-b17c-7d67b175f464"), "Employee:Create" },
                    { new Guid("18449350-3200-4b0d-95e2-6a00c276cba3"), "JobPost:Create" },
                    { new Guid("195fcf2a-15f4-4fa7-81e9-2d7293d66624"), "Employee:Edit" },
                    { new Guid("1c25c681-7057-4082-8d48-40483bf394bb"), "JobPost:Detail" },
                    { new Guid("1d692979-3609-4002-bcc8-c6fee17a73ab"), "JobPost:Update" },
                    { new Guid("2eaa0fb4-3074-4f6b-9bff-da0ed5d8c5c3"), "Candidate:Create" },
                    { new Guid("3b3e578a-8dfe-4269-b537-eedfaabeef23"), "OkrRequest:Update" },
                    { new Guid("4f80f494-79d6-4a55-a71d-765b17c96f7b"), "JobPost:List" },
                    { new Guid("502745c5-7739-48a2-b67c-628d5a6cf106"), "EmployeeHistory:List" },
                    { new Guid("6ee9c3f8-5fd8-4fb7-866c-049845a023c8"), "Employee:Update" },
                    { new Guid("79600082-48a3-4b24-8603-019f85b3a42d"), "Candidate:List" },
                    { new Guid("7c6a7369-667a-4f13-8b0a-386336bb5153"), "Okr:Comment" },
                    { new Guid("83b0dada-6f10-4cc8-8f19-798faf1384ad"), "Okr:Detail" },
                    { new Guid("91c025e2-50fb-461a-8a26-719a686ac9c4"), "OkrRequest:List" },
                    { new Guid("930a967e-c4d2-48ef-b965-c5f3f16dd7b2"), "OkrRequest:Create" },
                    { new Guid("93d1a822-3ecf-4bd9-beca-f3f5e899fb72"), "OkrRequest:Edit" },
                    { new Guid("96e988d8-3b2c-40df-8145-af58d80806f4"), "EmployeeFamily:List" },
                    { new Guid("a2b1f874-d4f0-473e-88c9-658f82d7cb48"), "EmployeeFamily:Create" },
                    { new Guid("d378e104-a8aa-4efa-8336-5e1fd852abd6"), "Okr:List" },
                    { new Guid("e148c242-b91d-4f62-b89f-342414088a3f"), "EmployeeFamily:Edit" },
                    { new Guid("e81effb9-d8da-45e5-bfd4-ec405084452e"), "Candidate:Update" },
                    { new Guid("f1552a86-0e67-4660-9185-5366c20cd5c2"), "EmployeeFamily:Delete" },
                    { new Guid("f67613a8-ab0f-442b-8af4-6fe3a15af19c"), "Employee:List" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "Type", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), new DateTime(2024, 8, 3, 15, 51, 40, 378, DateTimeKind.Utc).AddTicks(1743), new Guid("00000000-0000-0000-0000-000000000000"), "Administrator role with all permissions.", "Admin", "Basic", new DateTime(2024, 8, 3, 15, 51, 40, 378, DateTimeKind.Utc).AddTicks(1745), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessToken", "AccessTokenCreated", "CreatedAt", "CreatedBy", "DepartmentId", "IsDeleted", "PasswordHash", "PasswordSalt", "RefreshToken", "RefreshTokenCreated", "RefreshTokenExpires", "RoleId", "Status", "TemporaryPasswordExpires", "TemporaryPasswordHash", "TemporaryPasswordSalt", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new Guid("b1feda7f-096c-463b-8908-e73a5cc46183"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 3, 22, 51, 40, 378, DateTimeKind.Local).AddTicks(2925), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"), false, new byte[] { 70, 146, 213, 59, 185, 56, 167, 253, 62, 253, 17, 241, 126, 205, 250, 179, 37, 215, 146, 190, 144, 244, 59, 201, 88, 6, 103, 47, 126, 221, 20, 188, 42, 67, 47, 196, 119, 229, 78, 107, 7, 72, 244, 212, 227, 22, 144, 3, 59, 86, 99, 167, 219, 253, 200, 33, 193, 116, 58, 91, 35, 119, 65, 138 }, new byte[] { 182, 212, 39, 10, 211, 157, 101, 100, 56, 62, 179, 133, 7, 255, 80, 128, 12, 235, 241, 80, 188, 159, 169, 125, 67, 111, 60, 47, 188, 249, 222, 211, 102, 26, 114, 239, 97, 247, 167, 215, 155, 134, 106, 196, 218, 159, 84, 140, 164, 120, 45, 58, 160, 46, 35, 101, 209, 156, 229, 11, 204, 66, 66, 103, 110, 55, 232, 229, 221, 17, 2, 6, 121, 149, 63, 190, 74, 59, 84, 37, 234, 164, 183, 117, 203, 239, 69, 217, 126, 94, 135, 169, 160, 224, 175, 191, 110, 185, 195, 195, 218, 124, 165, 9, 95, 48, 3, 69, 101, 98, 244, 40, 40, 140, 198, 54, 227, 232, 171, 23, 138, 142, 196, 233, 174, 104, 183, 179 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), true, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "AdminSHR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1244816d-a379-48f6-b17c-7d67b175f464"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("18449350-3200-4b0d-95e2-6a00c276cba3"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("195fcf2a-15f4-4fa7-81e9-2d7293d66624"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1c25c681-7057-4082-8d48-40483bf394bb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1d692979-3609-4002-bcc8-c6fee17a73ab"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2eaa0fb4-3074-4f6b-9bff-da0ed5d8c5c3"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3b3e578a-8dfe-4269-b537-eedfaabeef23"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4f80f494-79d6-4a55-a71d-765b17c96f7b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("502745c5-7739-48a2-b67c-628d5a6cf106"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("6ee9c3f8-5fd8-4fb7-866c-049845a023c8"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("79600082-48a3-4b24-8603-019f85b3a42d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("7c6a7369-667a-4f13-8b0a-386336bb5153"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("83b0dada-6f10-4cc8-8f19-798faf1384ad"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("91c025e2-50fb-461a-8a26-719a686ac9c4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("930a967e-c4d2-48ef-b965-c5f3f16dd7b2"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("93d1a822-3ecf-4bd9-beca-f3f5e899fb72"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("96e988d8-3b2c-40df-8145-af58d80806f4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a2b1f874-d4f0-473e-88c9-658f82d7cb48"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d378e104-a8aa-4efa-8336-5e1fd852abd6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e148c242-b91d-4f62-b89f-342414088a3f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e81effb9-d8da-45e5-bfd4-ec405084452e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f1552a86-0e67-4660-9185-5366c20cd5c2"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f67613a8-ab0f-442b-8af4-6fe3a15af19c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b1feda7f-096c-463b-8908-e73a5cc46183"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0fce1785-2d3d-4bfe-8200-0b71b322a66e"), "OkrRequest:Create" },
                    { new Guid("22767788-1217-4c53-94ee-d13329a0bc8d"), "Candidate:Create" },
                    { new Guid("29db4d9b-9e5a-48a0-8d8c-722ebbdee657"), "OkrRequest:List" },
                    { new Guid("309a8350-a6e7-4937-883e-3dea63cbe6f2"), "JobPost:Update" },
                    { new Guid("3684489b-07d1-4e7a-9ebf-a3980147e8d1"), "Okr:Detail" },
                    { new Guid("37a5c132-e686-4731-ad25-745153a1e2fa"), "JobPost:Create" },
                    { new Guid("4fdf5d6a-01a8-4e3f-afdb-babeba8c0484"), "JobPost:List" },
                    { new Guid("57bcd6a5-9777-496e-b397-6952779ba20a"), "Okr:Comment" },
                    { new Guid("5b7a6106-9c86-4e2b-9db1-5fc8fea69b4e"), "EmployeeFamily:Create" },
                    { new Guid("5be01598-e6d7-458b-a077-9161295273cd"), "OkrRequest:Edit" },
                    { new Guid("692ed12a-cf89-4e54-a4e4-a7e17790600e"), "Employee:List" },
                    { new Guid("711ee627-6351-4e4d-9e7b-a7e8e8d9f8e6"), "Employee:Update" },
                    { new Guid("782352d8-1ff2-43e6-abb1-d7e89a0a3500"), "Okr:List" },
                    { new Guid("7cadb29e-7b14-4d4c-a6f4-47510ed188ab"), "OkrRequest:Update" },
                    { new Guid("8aa0e3c3-bc5c-49c2-90a5-95a4ea433ac2"), "EmployeeHistory:List" },
                    { new Guid("8e145d3d-3625-471d-932c-54601acb0047"), "Employee:Create" },
                    { new Guid("929ca1a2-4a69-4bf0-86c5-aceec23072bc"), "Employee:Edit" },
                    { new Guid("9cba56c5-24d4-4f66-bf0c-05a069a7ba0d"), "JobPost:Detail" },
                    { new Guid("b5766e82-2cd5-4b75-89d4-c0d40cbaa5b5"), "Candidate:Update" },
                    { new Guid("bb7b066b-5a80-4ae2-aa61-a171355dd6da"), "EmployeeFamily:List" },
                    { new Guid("bffc77c2-b7c0-47ac-8535-f97ee1730bb6"), "EmployeeFamily:Edit" },
                    { new Guid("c17f0530-3172-4b18-b75d-cb6120938fe8"), "Candidate:List" },
                    { new Guid("d1574de7-7701-4af9-8f46-bfceb878e1a6"), "EmployeeFamily:Delete" }
                });
        }
    }
}
