using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAmbassadorUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Committees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Committees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "OrganizationEntities",
                columns: new[] { "Id", "CreatedDate", "EntityCode", "EntityName", "IsActive" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "UNIT-01", "وحدة التحول الرقمي", true });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "OrganizationEntityId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedDate", "Email", "FailedLoginAttempts", "FullName", "IsActive", "JobTitle", "LastLogin", "LockedUntil", "MustChangePassword", "OrganizationEntityId", "PasswordExpiresDate", "PasswordHash", "RefreshToken", "RefreshTokenExpiresDate", "Username" },
                values: new object[] { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "سفير التغيير التجريبي", true, null, null, null, false, 1, null, "123456", null, null, "ambassador" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "AssignedDate", "ExpiresDate", "IsActive", "UserRoleId" },
                values: new object[] { 2, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrganizationEntities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Committees",
                columns: new[] { "Id", "CreatedDate", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 10, 11, 43, 16, 753, DateTimeKind.Local).AddTicks(1054), "مسؤولة عن متابعة أتمتة الإجراءات", true, "لجنة التحول الرقمي" },
                    { 2, new DateTime(2026, 8, 10, 11, 43, 16, 753, DateTimeKind.Local).AddTicks(1122), "إدارة عمليات التغيير للموظفين", true, "لجنة الموارد البشرية" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 10, 11, 43, 16, 751, DateTimeKind.Local).AddTicks(6416));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 10, 11, 43, 16, 752, DateTimeKind.Local).AddTicks(6438));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 10, 11, 43, 16, 752, DateTimeKind.Local).AddTicks(6448));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 10, 11, 43, 16, 752, DateTimeKind.Local).AddTicks(6449));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "OrganizationEntityId",
                value: null);
        }
    }
}
