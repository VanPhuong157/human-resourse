using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class UpdateHomePageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("01c1ee62-979c-4ffe-a3fd-3abf75848f95"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("032cfaab-746b-44ff-bfc5-4e9b50d68093"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0549e88a-199b-4fda-873d-16e7efd3bffd"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1de8328d-566f-4274-ad8c-323d956aa0f9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("20220b0e-8e98-4127-960a-1cbf9c83ceac"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2812925f-d7f7-484a-a543-9d95952e4db2"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("301fc674-4830-476a-844e-ea5a2ce360d0"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("33153839-dace-4e96-a99b-e8a342036af4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("36c3afd4-b038-483f-9153-30ccd471aa22"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("472b6ea1-142d-4f1d-b680-63754211e36d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("48780fe7-3d7a-493d-ae9b-c7fa926c2461"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("626dd488-251a-44b0-b3d2-4a0f04c43c77"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("654379d8-fa2a-4244-8eaa-9c027dab97ae"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("76dcfbb9-4d89-425f-8cb3-e224b026e354"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("7adf76c1-dfdb-4c9f-952e-a8befb73cf1c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9b2e32ce-4455-4175-bcfa-478927b44f14"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a112130c-ec54-4311-b424-b46ac0ab2144"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a5f9facd-78a6-4582-8dc0-0a7a36cbc348"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bdbfd4ef-4526-4981-9626-7c129b6d1ebe"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c24325e3-b42e-42ca-b1ff-601c1b235206"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("caba5999-47d1-4acb-8886-5b70bf88d009"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d521856a-b2de-4085-91ff-da01c8c10204"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e981c921-c675-4f1d-b86c-00f82bf28daa"));

            migrationBuilder.AlterColumn<string>(
                name: "ImageBackgroundPath",
                table: "HomePages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageBackgroundDetail",
                table: "HomePages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "ImageBackgroundPath",
                table: "HomePages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageBackgroundDetail",
                table: "HomePages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("01c1ee62-979c-4ffe-a3fd-3abf75848f95"), "OkrRequest:Create" },
                    { new Guid("032cfaab-746b-44ff-bfc5-4e9b50d68093"), "Employee:Edit" },
                    { new Guid("0549e88a-199b-4fda-873d-16e7efd3bffd"), "Okr:List" },
                    { new Guid("1de8328d-566f-4274-ad8c-323d956aa0f9"), "EmployeeFamily:List" },
                    { new Guid("20220b0e-8e98-4127-960a-1cbf9c83ceac"), "EmployeeFamily:Delete" },
                    { new Guid("2812925f-d7f7-484a-a543-9d95952e4db2"), "EmployeeFamily:Edit" },
                    { new Guid("301fc674-4830-476a-844e-ea5a2ce360d0"), "Employee:Create" },
                    { new Guid("33153839-dace-4e96-a99b-e8a342036af4"), "Okr:Detail" },
                    { new Guid("36c3afd4-b038-483f-9153-30ccd471aa22"), "Okr:Comment" },
                    { new Guid("472b6ea1-142d-4f1d-b680-63754211e36d"), "EmployeeFamily:Create" },
                    { new Guid("48780fe7-3d7a-493d-ae9b-c7fa926c2461"), "JobPost:List" },
                    { new Guid("626dd488-251a-44b0-b3d2-4a0f04c43c77"), "Candidate:Create" },
                    { new Guid("654379d8-fa2a-4244-8eaa-9c027dab97ae"), "OkrRequest:Edit" },
                    { new Guid("76dcfbb9-4d89-425f-8cb3-e224b026e354"), "JobPost:Create" },
                    { new Guid("7adf76c1-dfdb-4c9f-952e-a8befb73cf1c"), "Candidate:Update" },
                    { new Guid("9b2e32ce-4455-4175-bcfa-478927b44f14"), "OkrRequest:Update" },
                    { new Guid("a112130c-ec54-4311-b424-b46ac0ab2144"), "JobPost:Update" },
                    { new Guid("a5f9facd-78a6-4582-8dc0-0a7a36cbc348"), "OkrRequest:List" },
                    { new Guid("bdbfd4ef-4526-4981-9626-7c129b6d1ebe"), "EmployeeHistory:List" },
                    { new Guid("c24325e3-b42e-42ca-b1ff-601c1b235206"), "Candidate:List" },
                    { new Guid("caba5999-47d1-4acb-8886-5b70bf88d009"), "JobPost:Detail" },
                    { new Guid("d521856a-b2de-4085-91ff-da01c8c10204"), "Employee:List" },
                    { new Guid("e981c921-c675-4f1d-b86c-00f82bf28daa"), "Employee:Update" }
                });
        }
    }
}
