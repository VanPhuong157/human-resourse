using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddCreatedByPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("01e1cc5d-0d71-4e45-8613-d17be66a788c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("03339442-84a4-4cca-86c3-094d6daa1e04"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("07f52242-2d41-408f-8711-e4c166315b53"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("213b86bc-9e55-46ad-a034-822d60c96572"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("226004d3-828e-4cfb-b467-e4b13e3773d5"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2d7f077f-7671-4ff5-b43f-bab328afd988"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("304653c0-da43-4a67-aa46-82e3ba43166b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3827fb9d-0a51-4439-b73d-d5c1edc556cf"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3a0d8fa1-38d3-4afa-bc5a-d304996fdc8d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4aab62ec-77cb-441b-937e-5a649056f83e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5a0a583c-2424-4f80-a034-22c629b84c3e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("62cd79db-ffd3-4660-bade-8af15f7dd3a0"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("6ee0f0e0-c645-4456-95a8-efd5f3d89696"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("702d763a-8642-4087-8793-2fe84a227a3c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9a027695-9483-4183-bb21-99f8f9f9e5a8"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9c14b334-f212-4a9c-99b3-c1c330b36635"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a62f6540-f11e-45ac-9787-3ffd15e994d1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a9785e0b-3a9e-4b12-ac37-53e2cdaa1dff"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c12477a5-8579-4f6d-8e77-88be56e37e6e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d2261070-9456-4950-9286-380d6f974850"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dc4a1d1e-427d-41a5-8d2b-a0747b7ae009"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e1d302c2-40be-4c71-9c3b-b74644c3d00c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("fdbcc204-3a15-45d7-963a-0046a1878c2e"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "JobPosts",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0577df2e-1bea-4f1e-aea4-49242e9fbe75"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0e0c7f6b-e216-4729-b762-05c15965732e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("32e1e2e5-f922-40f5-bd2a-212a39ea0543"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("33aa4299-a77e-4ed5-9fed-f615d1405bb3"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3982659b-5f10-4067-ba92-eb56b72a8954"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3b235750-f8ec-44df-b688-dcde7376692a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("405c1bc0-f0b4-42cd-8256-490c6a397c16"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4cb733d0-ad18-49e7-ba53-35c1bcda6a1f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("51a7fd6c-2c76-43f1-9893-70ec312016f4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5405fd5c-e756-49ac-ba53-346798008e59"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("72d95410-636e-4b7e-80f4-b0131d4c453d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("790859ce-cf94-4ae8-bf94-f996f4d11926"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("802bffcc-f1af-4598-853c-126fd5feb54a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9f96708b-a86e-4a1d-b1a6-cb283bd8a0c5"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a8083dee-f534-4390-95f8-5bb2301ce4bd"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aea910cc-3293-4b48-928f-d6ce881051f9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("b744b822-a5a8-44c0-b264-1be15ebb9a40"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ce312c49-ed57-4867-8959-e6b0e6059844"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d460e06c-b1bb-4c39-8011-7f4a5bda0840"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ee126c98-7ba5-4db0-a60b-120e007fb192"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ee184403-0a36-4ca9-9038-45b2be54d1b4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f1715a3e-c7c6-409d-ba93-2bd6765ad853"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f5ddcfe9-1218-4769-b016-91555a1e9a1a"));

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "JobPosts");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("01e1cc5d-0d71-4e45-8613-d17be66a788c"), "EmployeeFamily:Edit" },
                    { new Guid("03339442-84a4-4cca-86c3-094d6daa1e04"), "Candidate:Update" },
                    { new Guid("07f52242-2d41-408f-8711-e4c166315b53"), "Employee:Edit" },
                    { new Guid("213b86bc-9e55-46ad-a034-822d60c96572"), "Candidate:Create" },
                    { new Guid("226004d3-828e-4cfb-b467-e4b13e3773d5"), "OkrRequest:Create" },
                    { new Guid("2d7f077f-7671-4ff5-b43f-bab328afd988"), "Okr:List" },
                    { new Guid("304653c0-da43-4a67-aa46-82e3ba43166b"), "Employee:Create" },
                    { new Guid("3827fb9d-0a51-4439-b73d-d5c1edc556cf"), "OkrRequest:Edit" },
                    { new Guid("3a0d8fa1-38d3-4afa-bc5a-d304996fdc8d"), "Okr:Comment" },
                    { new Guid("4aab62ec-77cb-441b-937e-5a649056f83e"), "JobPost:Create" },
                    { new Guid("5a0a583c-2424-4f80-a034-22c629b84c3e"), "Employee:Update" },
                    { new Guid("62cd79db-ffd3-4660-bade-8af15f7dd3a0"), "JobPost:List" },
                    { new Guid("6ee0f0e0-c645-4456-95a8-efd5f3d89696"), "EmployeeFamily:Create" },
                    { new Guid("702d763a-8642-4087-8793-2fe84a227a3c"), "Candidate:List" },
                    { new Guid("9a027695-9483-4183-bb21-99f8f9f9e5a8"), "OkrRequest:Update" },
                    { new Guid("9c14b334-f212-4a9c-99b3-c1c330b36635"), "Employee:List" },
                    { new Guid("a62f6540-f11e-45ac-9787-3ffd15e994d1"), "EmployeeFamily:List" },
                    { new Guid("a9785e0b-3a9e-4b12-ac37-53e2cdaa1dff"), "EmployeeFamily:Delete" },
                    { new Guid("c12477a5-8579-4f6d-8e77-88be56e37e6e"), "JobPost:Update" },
                    { new Guid("d2261070-9456-4950-9286-380d6f974850"), "EmployeeHistory:List" },
                    { new Guid("dc4a1d1e-427d-41a5-8d2b-a0747b7ae009"), "Okr:Detail" },
                    { new Guid("e1d302c2-40be-4c71-9c3b-b74644c3d00c"), "OkrRequest:List" },
                    { new Guid("fdbcc204-3a15-45d7-963a-0046a1878c2e"), "JobPost:Detail" }
                });
        }
    }
}
