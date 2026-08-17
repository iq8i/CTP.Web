using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSystemSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityInputs_OrganizationEntities_OrganizationEntityId",
                table: "EntityInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityInputs_Users_PreparerId",
                table: "EntityInputs");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                columns: new[] { "RoleCode", "RoleName" },
                values: new object[] { "STAFF", "المنسوبون" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                columns: new[] { "RoleCode", "RoleName" },
                values: new object[] { "MANAGER", "القادة والمدراء المباشرون" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                columns: new[] { "RoleCode", "RoleName" },
                values: new object[] { "AMBASSADOR", "سفراء التغيير" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                columns: new[] { "RoleCode", "RoleName" },
                values: new object[] { "UNIT_LEADER", "أصحاب السعادة قادة الوحدات والمدراء" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedDate", "Description", "IsActive", "RoleCode", "RoleName" },
                values: new object[,]
                {
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "COMMITTEE_MEMBER", "أعضاء لجنة شركاء التغيير" },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "COMMITTEE_CHAIR", "رئيس لجنة شركاء التغيير" },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "CHIEF_OF_STAFF", "سعادة رئيس فريق عمل القائد" },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "DEPUTY_LEADER", "سعادة نائب القائد" },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "LEADER", "معالي القائد" },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, "CORP_COMMS", "الاتصال المؤسسي" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "AssignedDate", "ExpiresDate", "IsActive", "UserRoleId" },
                values: new object[] { 1, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "FullName", "Username" },
                values: new object[] { "مستخدم منسوب", "staff" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "FullName", "Username" },
                values: new object[] { "مدير مباشر", "manager" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedDate", "Email", "FailedLoginAttempts", "FullName", "IsActive", "JobTitle", "LastLogin", "LockedUntil", "MustChangePassword", "OrganizationEntityId", "PasswordExpiresDate", "PasswordHash", "RefreshToken", "RefreshTokenExpiresDate", "Username" },
                values: new object[,]
                {
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "سفير التغيير", true, null, null, null, false, 1, null, "123456", null, null, "ambassador" },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "قائد الوحدة", true, null, null, null, false, 1, null, "123456", null, null, "unitleader" },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "عضو اللجنة", true, null, null, null, false, 1, null, "123456", null, null, "member" },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "رئيس اللجنة", true, null, null, null, false, 1, null, "123456", null, null, "chair" },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "رئيس فريق عمل القائد", true, null, null, null, false, 1, null, "123456", null, null, "chief" },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "نائب القائد", true, null, null, null, false, 1, null, "123456", null, null, "deputy" },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "معالي القائد", true, null, null, null, false, 1, null, "123456", null, null, "leader" },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "موظف الاتصال", true, null, null, null, false, 1, null, "123456", null, null, "comms" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "AssignedDate", "ExpiresDate", "IsActive", "UserRoleId" },
                values: new object[,]
                {
                    { 3, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 3 },
                    { 4, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 4 },
                    { 5, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 5 },
                    { 6, 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 6 },
                    { 7, 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 7 },
                    { 8, 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 8 },
                    { 9, 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 9 },
                    { 10, 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 10 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EntityInputs_OrganizationEntities_OrganizationEntityId",
                table: "EntityInputs",
                column: "OrganizationEntityId",
                principalTable: "OrganizationEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityInputs_Users_PreparerId",
                table: "EntityInputs",
                column: "PreparerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityInputs_OrganizationEntities_OrganizationEntityId",
                table: "EntityInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityInputs_Users_PreparerId",
                table: "EntityInputs");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 8, 8 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 9, 9 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 10, 10 });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                columns: new[] { "RoleCode", "RoleName" },
                values: new object[] { "LEADER", "معالي القائد" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                columns: new[] { "RoleCode", "RoleName" },
                values: new object[] { "AMBASSADOR", "سفير التغيير" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                columns: new[] { "RoleCode", "RoleName" },
                values: new object[] { "COMMITTEE_CHAIR", "رئيس لجنة شركاء التغيير" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                columns: new[] { "RoleCode", "RoleName" },
                values: new object[] { "STAFF", "المنسوبون" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "AssignedDate", "ExpiresDate", "IsActive", "UserRoleId" },
                values: new object[] { 3, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "FullName", "Username" },
                values: new object[] { "مدير النظام التجريبي", "admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "FullName", "Username" },
                values: new object[] { "سفير التغيير التجريبي", "ambassador" });

            migrationBuilder.AddForeignKey(
                name: "FK_EntityInputs_OrganizationEntities_OrganizationEntityId",
                table: "EntityInputs",
                column: "OrganizationEntityId",
                principalTable: "OrganizationEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityInputs_Users_PreparerId",
                table: "EntityInputs",
                column: "PreparerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
