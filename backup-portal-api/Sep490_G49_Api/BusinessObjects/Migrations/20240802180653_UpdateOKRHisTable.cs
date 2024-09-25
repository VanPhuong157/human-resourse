using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateOKRHisTable : Migration
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

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "okrHistories");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "okrHistories",
                newName: "Description");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "okrHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("06a5b968-8c8a-4595-9c4e-a7f8174b6e30"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0962280e-fd76-4b17-8aba-b9e71a7c4dee"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1542af57-c235-4cd5-98d4-a8f8c3d1fde2"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2080c599-a117-45dd-ac4a-cfb25e84daf9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("257a542c-6a1c-43de-9414-05c95def0f8e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2d47b688-8535-46fe-8e57-f5523b5262c5"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3ae3a34f-8f60-4a7e-a537-313ceff0078a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5a05e663-7181-472f-8bc9-d8b0e6c02fdf"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5cf31092-1676-4ea7-9a78-568c1635c93b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("60f92f18-0737-4183-a615-9b624e2a7570"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("63c6ed42-c5ff-4ea9-9c75-e669e711e757"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8ddad71f-d06a-451a-8a4d-39d9df353934"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8ea8d15c-5aa3-416d-91f9-df7253985f88"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("98dbc027-0068-4df9-8ab6-5c6bcae4e535"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a1020158-11aa-4840-9394-69c50b8bbab8"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a72ed5b9-4305-46be-ac0b-7878f83bbc7e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ac79c7d5-2aae-4ca1-93f5-0ab524c72b51"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbabf90d-6444-41cd-ad16-e83187e89e00"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbe22eb0-ae9a-455d-9b60-54bc1175c28e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c7fb10d5-eefe-4271-b628-3b17003be11e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d1995105-610c-4423-935f-89ea6f42e56a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e14183f2-d7bc-4f8f-b352-a59680ccb81f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("fe6e36fd-6eee-4f5e-ba4d-ce8d7b280632"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "okrHistories");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "okrHistories",
                newName: "Comment");

            migrationBuilder.AddColumn<Guid>(
                name: "Owner",
                table: "okrHistories",
                type: "uniqueidentifier",
                nullable: true);

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
