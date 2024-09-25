using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    public partial class AddUserGroupRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permissions_PermissionId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Roles_RoleId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_Role_Roles_RoleId",
                table: "UserGroup_Role");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_Role_UserGroup_UserGroupId",
                table: "UserGroup_Role");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_User_UserGroup_UserGroupId",
                table: "UserGroup_User");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_User_Users_UserId",
                table: "UserGroup_User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroup_User",
                table: "UserGroup_User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroup_Role",
                table: "UserGroup_Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroup",
                table: "UserGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission");

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

            migrationBuilder.RenameTable(
                name: "UserGroup_User",
                newName: "UserGroup_Users");

            migrationBuilder.RenameTable(
                name: "UserGroup_Role",
                newName: "UserGroup_Roles");

            migrationBuilder.RenameTable(
                name: "UserGroup",
                newName: "UserGroups");

            migrationBuilder.RenameTable(
                name: "RolePermission",
                newName: "RolePermissions");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroup_User_UserId",
                table: "UserGroup_Users",
                newName: "IX_UserGroup_Users_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroup_Role_RoleId",
                table: "UserGroup_Roles",
                newName: "IX_UserGroup_Roles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermissions",
                newName: "IX_RolePermissions_PermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroup_Users",
                table: "UserGroup_Users",
                columns: new[] { "UserGroupId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroup_Roles",
                table: "UserGroup_Roles",
                columns: new[] { "UserGroupId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroups",
                table: "UserGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_Roles_Roles_RoleId",
                table: "UserGroup_Roles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_Roles_UserGroups_UserGroupId",
                table: "UserGroup_Roles",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_Users_UserGroups_UserGroupId",
                table: "UserGroup_Users",
                column: "UserGroupId",
                principalTable: "UserGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_Users_Users_UserId",
                table: "UserGroup_Users",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_Roles_Roles_RoleId",
                table: "UserGroup_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_Roles_UserGroups_UserGroupId",
                table: "UserGroup_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_Users_UserGroups_UserGroupId",
                table: "UserGroup_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_Users_Users_UserId",
                table: "UserGroup_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroups",
                table: "UserGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroup_Users",
                table: "UserGroup_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroup_Roles",
                table: "UserGroup_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions");

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

            migrationBuilder.RenameTable(
                name: "UserGroups",
                newName: "UserGroup");

            migrationBuilder.RenameTable(
                name: "UserGroup_Users",
                newName: "UserGroup_User");

            migrationBuilder.RenameTable(
                name: "UserGroup_Roles",
                newName: "UserGroup_Role");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                newName: "RolePermission");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroup_Users_UserId",
                table: "UserGroup_User",
                newName: "IX_UserGroup_User_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroup_Roles_RoleId",
                table: "UserGroup_Role",
                newName: "IX_UserGroup_Role_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermission",
                newName: "IX_RolePermission_PermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroup",
                table: "UserGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroup_User",
                table: "UserGroup_User",
                columns: new[] { "UserGroupId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroup_Role",
                table: "UserGroup_Role",
                columns: new[] { "UserGroupId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission",
                columns: new[] { "RoleId", "PermissionId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permissions_PermissionId",
                table: "RolePermission",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Roles_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_Role_Roles_RoleId",
                table: "UserGroup_Role",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_Role_UserGroup_UserGroupId",
                table: "UserGroup_Role",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_User_UserGroup_UserGroupId",
                table: "UserGroup_User",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroup_User_Users_UserId",
                table: "UserGroup_User",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
