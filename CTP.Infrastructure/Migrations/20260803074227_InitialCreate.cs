using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EntityName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RoleName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    JobTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    OrganizationEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MustChangePassword = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordExpiresDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FailedLoginAttempts = table.Column<int>(type: "INTEGER", nullable: false),
                    LockedUntil = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RefreshToken = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    RefreshTokenExpiresDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_OrganizationEntities_OrganizationEntityId",
                        column: x => x.OrganizationEntityId,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserRoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpiresDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedDate", "Description", "IsActive", "RoleCode", "RoleName" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 3, 10, 42, 27, 225, DateTimeKind.Local).AddTicks(2432), null, true, "LEADER", "معالي القائد" },
                    { 2, new DateTime(2026, 8, 3, 10, 42, 27, 226, DateTimeKind.Local).AddTicks(2080), null, true, "AMBASSADOR", "سفير التغيير" },
                    { 3, new DateTime(2026, 8, 3, 10, 42, 27, 226, DateTimeKind.Local).AddTicks(2088), null, true, "COMMITTEE_CHAIR", "رئيس لجنة شركاء التغيير" },
                    { 4, new DateTime(2026, 8, 3, 10, 42, 27, 226, DateTimeKind.Local).AddTicks(2089), null, true, "STAFF", "المنسوبون" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedDate", "Email", "FailedLoginAttempts", "FullName", "IsActive", "JobTitle", "LastLogin", "LockedUntil", "MustChangePassword", "OrganizationEntityId", "PasswordExpiresDate", "PasswordHash", "RefreshToken", "RefreshTokenExpiresDate", "Username" },
                values: new object[] { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "مدير النظام التجريبي", true, null, null, null, false, null, null, "123456", null, null, "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "AssignedDate", "ExpiresDate", "IsActive", "UserRoleId" },
                values: new object[] { 3, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationEntityId",
                table: "Users",
                column: "OrganizationEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "OrganizationEntities");
        }
    }
}
