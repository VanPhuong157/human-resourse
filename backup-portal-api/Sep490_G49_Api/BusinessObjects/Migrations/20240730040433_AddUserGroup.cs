using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddUserGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

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

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserGroupId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup_User",
                columns: table => new
                {
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup_User", x => new { x.UserGroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserGroup_User_UserGroup_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_User_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("036c6659-655b-46ff-83eb-8be8c2aa9402"), "JobPost:Detail" },
                    { new Guid("038aac70-db64-46ec-bace-8d979b476576"), "OkrRequest:Create" },
                    { new Guid("0a49adf1-2ebd-4803-81d8-9b43911ccdd9"), "EmployeeFamily:Create" },
                    { new Guid("176bb050-1fe3-4c0e-b32b-0cbaa2c223ae"), "OkrRequest:Edit" },
                    { new Guid("1ee94053-ab52-4766-9ed9-09d5688add68"), "OkrRequest:Update" },
                    { new Guid("22a76181-49e0-4afe-9f49-c90c11e12293"), "Employee:Update" },
                    { new Guid("2d71912e-9380-4e6c-b488-9598b1288fb8"), "JobPost:Update" },
                    { new Guid("3e526aa1-df7a-4582-816b-9ad6626814ec"), "EmployeeFamily:List" },
                    { new Guid("4a6d2955-8c54-42ff-aa3b-a9c12864576f"), "EmployeeHistory:List" },
                    { new Guid("66af5dfc-742a-455d-a2d1-7080a5a6239d"), "Okr:List" },
                    { new Guid("6f470f51-2c70-4236-98c3-de083ac9ac3c"), "OkrRequest:List" },
                    { new Guid("73a6ae18-7e36-42e5-ad7e-1a09aa38442d"), "EmployeeFamily:Edit" },
                    { new Guid("73f03a11-446f-48cc-9071-264bf777d15f"), "Employee:Create" },
                    { new Guid("7d2809ce-fa78-4bdd-a185-41fc292aed8b"), "JobPost:Create" },
                    { new Guid("878772c5-6e1d-40ce-a7ef-208418e2b0c7"), "JobPost:List" },
                    { new Guid("8ad203d3-1751-4949-bfc0-3a6cdde8065f"), "Okr:Comment" },
                    { new Guid("8cf4f833-0096-49a1-a7f8-219aece1410c"), "Candidate:Update" },
                    { new Guid("99dc9f61-af25-4987-afe0-a3887145d9a0"), "Employee:List" },
                    { new Guid("a2acc2fa-d8ae-46ea-8460-b892123fb499"), "Candidate:List" },
                    { new Guid("a347fdeb-fddc-44c1-9f9c-931a0de688c9"), "EmployeeFamily:Delete" },
                    { new Guid("a57becbb-6d7f-46ec-87a0-22e59e787dcc"), "Okr:Detail" },
                    { new Guid("c4a720be-d219-491d-a29a-bb442944ecf9"), "Employee:Edit" },
                    { new Guid("efd70df2-0c60-4bf0-8157-0d814fc50d39"), "Candidate:Create" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserGroupId",
                table: "Roles",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_User_UserId",
                table: "UserGroup_User",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_UserGroup_UserGroupId",
                table: "Roles",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_UserGroup_UserGroupId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserGroup_User");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserGroupId",
                table: "Roles");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("036c6659-655b-46ff-83eb-8be8c2aa9402"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("038aac70-db64-46ec-bace-8d979b476576"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0a49adf1-2ebd-4803-81d8-9b43911ccdd9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("176bb050-1fe3-4c0e-b32b-0cbaa2c223ae"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1ee94053-ab52-4766-9ed9-09d5688add68"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("22a76181-49e0-4afe-9f49-c90c11e12293"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2d71912e-9380-4e6c-b488-9598b1288fb8"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3e526aa1-df7a-4582-816b-9ad6626814ec"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4a6d2955-8c54-42ff-aa3b-a9c12864576f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("66af5dfc-742a-455d-a2d1-7080a5a6239d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("6f470f51-2c70-4236-98c3-de083ac9ac3c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("73a6ae18-7e36-42e5-ad7e-1a09aa38442d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("73f03a11-446f-48cc-9071-264bf777d15f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("7d2809ce-fa78-4bdd-a185-41fc292aed8b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("878772c5-6e1d-40ce-a7ef-208418e2b0c7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8ad203d3-1751-4949-bfc0-3a6cdde8065f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8cf4f833-0096-49a1-a7f8-219aece1410c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("99dc9f61-af25-4987-afe0-a3887145d9a0"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a2acc2fa-d8ae-46ea-8460-b892123fb499"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a347fdeb-fddc-44c1-9f9c-931a0de688c9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a57becbb-6d7f-46ec-87a0-22e59e787dcc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c4a720be-d219-491d-a29a-bb442944ecf9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("efd70df2-0c60-4bf0-8157-0d814fc50d39"));

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserGroupId",
                table: "Roles");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }
    }
}
