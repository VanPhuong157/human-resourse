using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddHomePageReasonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("06d7509f-94d7-4e24-808c-138f054b000e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("091a6ed6-fa3d-46af-b223-db8731ac246a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("097740d2-c32c-4d28-8483-9fb0db8096c1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("09c0c56b-b717-4f02-af0f-7a910f7b8bd5"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("15340912-832e-4875-a4fa-fd909fa8e35e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1b804ecb-ef5a-4c80-af7b-65deae75bf24"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2c58e8b5-f532-43ba-9dca-78045f1186d0"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3cb80c7c-32c0-48fa-a5fb-7bc0a6959a11"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3f5f10e7-e410-4edf-982e-2577c2a9477e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4107e2dc-dc0e-434b-967d-86b53ab1debb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("415eb110-72e4-46da-8fb6-72c35a5c626a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("487f2d7e-588f-462b-9772-aec73a93ca8e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4d18d295-cf92-4bf8-9a38-1311a4158e38"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("6bcb7bf0-623f-4596-a7aa-66a384bc2913"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("78d74da2-fc9e-4d11-aaf9-ea1f723d71b0"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8f956617-9b84-4dba-b6eb-dbab1dd7fc2e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("98a99ae8-c595-4b7c-a949-4d4590ccd438"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a8803c18-d49c-4242-80e6-4d366375a159"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("b3db5e38-d801-4e10-8427-bb83fe021399"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("b8502823-16c5-4db2-b02a-3b8be7012b15"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("de033741-bc20-4c70-9799-1318f508afcf"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e555c774-3f4e-4796-951f-9de3fb66989a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("fdccb630-b7b3-4270-841a-88f7abf0b7b9"));

            migrationBuilder.CreateTable(
                name: "HomePageReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageReasons", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("06d7509f-94d7-4e24-808c-138f054b000e"), "Employee:List" },
                    { new Guid("091a6ed6-fa3d-46af-b223-db8731ac246a"), "JobPost:List" },
                    { new Guid("097740d2-c32c-4d28-8483-9fb0db8096c1"), "Employee:Update" },
                    { new Guid("09c0c56b-b717-4f02-af0f-7a910f7b8bd5"), "EmployeeFamily:Delete" },
                    { new Guid("15340912-832e-4875-a4fa-fd909fa8e35e"), "EmployeeFamily:Edit" },
                    { new Guid("1b804ecb-ef5a-4c80-af7b-65deae75bf24"), "EmployeeHistory:List" },
                    { new Guid("2c58e8b5-f532-43ba-9dca-78045f1186d0"), "EmployeeFamily:List" },
                    { new Guid("3cb80c7c-32c0-48fa-a5fb-7bc0a6959a11"), "OkrRequest:Edit" },
                    { new Guid("3f5f10e7-e410-4edf-982e-2577c2a9477e"), "JobPost:Detail" },
                    { new Guid("4107e2dc-dc0e-434b-967d-86b53ab1debb"), "Employee:Create" },
                    { new Guid("415eb110-72e4-46da-8fb6-72c35a5c626a"), "Employee:Edit" },
                    { new Guid("487f2d7e-588f-462b-9772-aec73a93ca8e"), "JobPost:Update" },
                    { new Guid("4d18d295-cf92-4bf8-9a38-1311a4158e38"), "Candidate:List" },
                    { new Guid("6bcb7bf0-623f-4596-a7aa-66a384bc2913"), "OkrRequest:Update" },
                    { new Guid("78d74da2-fc9e-4d11-aaf9-ea1f723d71b0"), "Candidate:Create" },
                    { new Guid("8f956617-9b84-4dba-b6eb-dbab1dd7fc2e"), "Okr:List" },
                    { new Guid("98a99ae8-c595-4b7c-a949-4d4590ccd438"), "Candidate:Update" },
                    { new Guid("a8803c18-d49c-4242-80e6-4d366375a159"), "Okr:Comment" },
                    { new Guid("b3db5e38-d801-4e10-8427-bb83fe021399"), "OkrRequest:Create" },
                    { new Guid("b8502823-16c5-4db2-b02a-3b8be7012b15"), "OkrRequest:List" },
                    { new Guid("de033741-bc20-4c70-9799-1318f508afcf"), "Okr:Detail" },
                    { new Guid("e555c774-3f4e-4796-951f-9de3fb66989a"), "JobPost:Create" },
                    { new Guid("fdccb630-b7b3-4270-841a-88f7abf0b7b9"), "EmployeeFamily:Create" }
                });
        }
    }
}
