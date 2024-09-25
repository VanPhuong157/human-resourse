using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddRolePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_UserGroup_UserGroupId",
                table: "Roles");

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
                name: "UserGroupId",
                table: "Roles");

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup_Role",
                columns: table => new
                {
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup_Role", x => new { x.UserGroupId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserGroup_Role_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_Role_UserGroup_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1790d9f0-9ca6-4391-9589-42960e870105"), "EmployeeFamily:List" },
                    { new Guid("194aa48e-aaec-4a97-8841-bc6a11743312"), "Okr:Detail" },
                    { new Guid("230417f9-401d-4572-ae34-12563e7ea176"), "Employee:Edit" },
                    { new Guid("3cac7400-b476-416a-bd17-4a66c7ada7aa"), "OkrRequest:List" },
                    { new Guid("402963dc-28fe-4931-9223-4975ccb1fb1e"), "Employee:List" },
                    { new Guid("461c4f15-dc7a-4a55-9516-7717a54dc4b5"), "JobPost:Detail" },
                    { new Guid("79e3ced8-b90b-4e2d-9794-ee992b4c2613"), "Candidate:Update" },
                    { new Guid("8684b821-aa41-4db0-8b52-f276cb73dbca"), "OkrRequest:Create" },
                    { new Guid("87cd3bc8-cc8f-447f-b14a-573bbc3b7589"), "Candidate:Create" },
                    { new Guid("967c009d-e32e-4b0a-8f00-bf63f3cf559f"), "EmployeeFamily:Create" },
                    { new Guid("96e00273-d18e-4426-8309-e62693998bd5"), "Candidate:List" },
                    { new Guid("a904911a-7df5-434f-899b-90f607e2adaa"), "Okr:Comment" },
                    { new Guid("aa807bef-e767-45d2-a982-8a1a50be5135"), "Employee:Update" },
                    { new Guid("ae7b696a-8e01-4f11-84e7-cfd71885926f"), "JobPost:Update" },
                    { new Guid("b4fbc103-b40b-433c-992a-58b479a406a1"), "EmployeeFamily:Edit" },
                    { new Guid("bc02affb-59b1-4568-b7b6-423639cbcb07"), "OkrRequest:Update" },
                    { new Guid("bfc67371-1875-4705-b7db-f686f02b632f"), "OkrRequest:Edit" },
                    { new Guid("c72b4475-59c0-49f8-aec8-4a320b8e0ac4"), "EmployeeFamily:Delete" },
                    { new Guid("d250eed0-b2e9-4ed6-9fcc-e2355abed858"), "JobPost:List" },
                    { new Guid("deda5f22-ef54-4e62-b8c5-ba57943fd258"), "JobPost:Create" },
                    { new Guid("e28b0c0f-1937-4670-b8dc-3a31c463a1a9"), "EmployeeHistory:List" },
                    { new Guid("eb1b2382-2e1d-42ac-8dba-bf0b62775341"), "Employee:Create" },
                    { new Guid("f441af65-a775-4a02-9ff7-1f855a31794a"), "Okr:List" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_Role_RoleId",
                table: "UserGroup_Role",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "UserGroup_Role");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1790d9f0-9ca6-4391-9589-42960e870105"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("194aa48e-aaec-4a97-8841-bc6a11743312"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("230417f9-401d-4572-ae34-12563e7ea176"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3cac7400-b476-416a-bd17-4a66c7ada7aa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("402963dc-28fe-4931-9223-4975ccb1fb1e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("461c4f15-dc7a-4a55-9516-7717a54dc4b5"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("79e3ced8-b90b-4e2d-9794-ee992b4c2613"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8684b821-aa41-4db0-8b52-f276cb73dbca"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("87cd3bc8-cc8f-447f-b14a-573bbc3b7589"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("967c009d-e32e-4b0a-8f00-bf63f3cf559f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("96e00273-d18e-4426-8309-e62693998bd5"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a904911a-7df5-434f-899b-90f607e2adaa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aa807bef-e767-45d2-a982-8a1a50be5135"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ae7b696a-8e01-4f11-84e7-cfd71885926f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("b4fbc103-b40b-433c-992a-58b479a406a1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bc02affb-59b1-4568-b7b6-423639cbcb07"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bfc67371-1875-4705-b7db-f686f02b632f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c72b4475-59c0-49f8-aec8-4a320b8e0ac4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d250eed0-b2e9-4ed6-9fcc-e2355abed858"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("deda5f22-ef54-4e62-b8c5-ba57943fd258"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e28b0c0f-1937-4670-b8dc-3a31c463a1a9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("eb1b2382-2e1d-42ac-8dba-bf0b62775341"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f441af65-a775-4a02-9ff7-1f855a31794a"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserGroupId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true);

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
                name: "IX_Roles_UserGroupId",
                table: "Roles",
                column: "UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_UserGroup_UserGroupId",
                table: "Roles",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "Id");
        }
    }
}
