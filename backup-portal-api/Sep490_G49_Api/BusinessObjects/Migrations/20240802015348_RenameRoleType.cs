using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class RenameRoleType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0285ed50-3ee0-49d7-a2c2-5c195c26536d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("028d5531-4066-4353-b68e-14f5515c33ff"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1f488cb8-71f6-4d98-bd23-745f6b420712"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("24a33bb6-c5b7-4235-a39c-392353a2dcea"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("267212e1-50a4-4e12-bc73-88cc69407a45"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2fb6de8a-fe16-46e6-93ca-b94f40c79ac4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("402c37b6-5f08-4143-9834-107775a5eb4d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("6622443f-55f7-491f-8f19-27624368aeb4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("704ab672-2ce3-4277-8b8c-f547fe21b21f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("74c350f5-ad12-4a49-a275-0f7f1a604aa7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("74fba86c-ba54-4c1b-9fb6-8ea032333c9f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("7bcef10e-04bf-45ed-8fbb-1d1769f90a16"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("94826f2a-8beb-4a67-8801-35a7d7f8777c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("97276e7e-293b-427d-8dfe-fd6538099593"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a7a2db72-0a78-49bd-ad7e-1ddc8a1b572c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cb950539-a99b-4e7d-a178-b03eeb06d860"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cc4d6302-d991-4814-97aa-588caec85858"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d0dc6bf1-7322-4665-bab6-3cf01a5d2d33"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d296a84d-feee-440c-8cfe-f943b0c335a1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("db2f503f-85d1-434b-bb7d-7c727d4e73df"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e0714e1c-f4c1-46e9-84a7-42f302ff9255"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e7998b65-7d59-47f5-9882-6f1cb6cf35b8"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f088f376-49d0-44f0-9f5a-080dfc3c2b21"));

            migrationBuilder.RenameColumn(
                name: "Group",
                table: "Roles",
                newName: "Type");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Roles",
                newName: "Group");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0285ed50-3ee0-49d7-a2c2-5c195c26536d"), "OkrRequest:Edit" },
                    { new Guid("028d5531-4066-4353-b68e-14f5515c33ff"), "OkrRequest:List" },
                    { new Guid("1f488cb8-71f6-4d98-bd23-745f6b420712"), "Employee:List" },
                    { new Guid("24a33bb6-c5b7-4235-a39c-392353a2dcea"), "EmployeeFamily:Create" },
                    { new Guid("267212e1-50a4-4e12-bc73-88cc69407a45"), "OkrRequest:Create" },
                    { new Guid("2fb6de8a-fe16-46e6-93ca-b94f40c79ac4"), "EmployeeFamily:List" },
                    { new Guid("402c37b6-5f08-4143-9834-107775a5eb4d"), "Employee:Update" },
                    { new Guid("6622443f-55f7-491f-8f19-27624368aeb4"), "EmployeeFamily:Delete" },
                    { new Guid("704ab672-2ce3-4277-8b8c-f547fe21b21f"), "JobPost:Detail" },
                    { new Guid("74c350f5-ad12-4a49-a275-0f7f1a604aa7"), "Candidate:Update" },
                    { new Guid("74fba86c-ba54-4c1b-9fb6-8ea032333c9f"), "Candidate:List" },
                    { new Guid("7bcef10e-04bf-45ed-8fbb-1d1769f90a16"), "EmployeeFamily:Edit" },
                    { new Guid("94826f2a-8beb-4a67-8801-35a7d7f8777c"), "JobPost:Create" },
                    { new Guid("97276e7e-293b-427d-8dfe-fd6538099593"), "Candidate:Create" },
                    { new Guid("a7a2db72-0a78-49bd-ad7e-1ddc8a1b572c"), "Employee:Edit" },
                    { new Guid("cb950539-a99b-4e7d-a178-b03eeb06d860"), "Okr:Detail" },
                    { new Guid("cc4d6302-d991-4814-97aa-588caec85858"), "JobPost:List" },
                    { new Guid("d0dc6bf1-7322-4665-bab6-3cf01a5d2d33"), "OkrRequest:Update" },
                    { new Guid("d296a84d-feee-440c-8cfe-f943b0c335a1"), "Okr:Comment" },
                    { new Guid("db2f503f-85d1-434b-bb7d-7c727d4e73df"), "EmployeeHistory:List" },
                    { new Guid("e0714e1c-f4c1-46e9-84a7-42f302ff9255"), "Employee:Create" },
                    { new Guid("e7998b65-7d59-47f5-9882-6f1cb6cf35b8"), "JobPost:Update" },
                    { new Guid("f088f376-49d0-44f0-9f5a-080dfc3c2b21"), "Okr:List" }
                });
        }
    }
}
