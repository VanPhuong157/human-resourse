using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateOKRHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "NewAchieved",
                table: "okrHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldAchieved",
                table: "okrHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfTarget",
                table: "okrHistories",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "NewAchieved",
                table: "okrHistories");

            migrationBuilder.DropColumn(
                name: "OldAchieved",
                table: "okrHistories");

            migrationBuilder.DropColumn(
                name: "UnitOfTarget",
                table: "okrHistories");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                column: "CreatedAt",
                value: new DateTime(2024, 8, 3, 15, 51, 40, 378, DateTimeKind.Utc).AddTicks(2979));

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

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 8, 3, 15, 51, 40, 378, DateTimeKind.Utc).AddTicks(1743), new DateTime(2024, 8, 3, 15, 51, 40, 378, DateTimeKind.Utc).AddTicks(1745) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessToken", "AccessTokenCreated", "CreatedAt", "CreatedBy", "DepartmentId", "IsDeleted", "PasswordHash", "PasswordSalt", "RefreshToken", "RefreshTokenCreated", "RefreshTokenExpires", "RoleId", "Status", "TemporaryPasswordExpires", "TemporaryPasswordHash", "TemporaryPasswordSalt", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new Guid("b1feda7f-096c-463b-8908-e73a5cc46183"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 3, 22, 51, 40, 378, DateTimeKind.Local).AddTicks(2925), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("7bf10d6a-c521-44be-804e-8f70e6ae26d1"), false, new byte[] { 70, 146, 213, 59, 185, 56, 167, 253, 62, 253, 17, 241, 126, 205, 250, 179, 37, 215, 146, 190, 144, 244, 59, 201, 88, 6, 103, 47, 126, 221, 20, 188, 42, 67, 47, 196, 119, 229, 78, 107, 7, 72, 244, 212, 227, 22, 144, 3, 59, 86, 99, 167, 219, 253, 200, 33, 193, 116, 58, 91, 35, 119, 65, 138 }, new byte[] { 182, 212, 39, 10, 211, 157, 101, 100, 56, 62, 179, 133, 7, 255, 80, 128, 12, 235, 241, 80, 188, 159, 169, 125, 67, 111, 60, 47, 188, 249, 222, 211, 102, 26, 114, 239, 97, 247, 167, 215, 155, 134, 106, 196, 218, 159, 84, 140, 164, 120, 45, 58, 160, 46, 35, 101, 209, 156, 229, 11, 204, 66, 66, 103, 110, 55, 232, 229, 221, 17, 2, 6, 121, 149, 63, 190, 74, 59, 84, 37, 234, 164, 183, 117, 203, 239, 69, 217, 126, 94, 135, 169, 160, 224, 175, 191, 110, 185, 195, 195, 218, 124, 165, 9, 95, 48, 3, 69, 101, 98, 244, 40, 40, 140, 198, 54, 227, 232, 171, 23, 138, 142, 196, 233, 174, 104, 183, 179 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), true, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "AdminSHR" });
        }
    }
}
