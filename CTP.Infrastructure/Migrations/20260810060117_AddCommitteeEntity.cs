using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommitteeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Committees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Committees",
                columns: new[] { "Id", "CreatedDate", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 8, 10, 9, 1, 17, 33, DateTimeKind.Local).AddTicks(4274), "مسؤولة عن متابعة أتمتة الإجراءات", true, "لجنة التحول الرقمي" },
                    { 2, new DateTime(2026, 8, 10, 9, 1, 17, 33, DateTimeKind.Local).AddTicks(4343), "إدارة عمليات التغيير للموظفين", true, "لجنة الموارد البشرية" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 10, 9, 1, 17, 31, DateTimeKind.Local).AddTicks(6105));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 10, 9, 1, 17, 32, DateTimeKind.Local).AddTicks(8309));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 10, 9, 1, 17, 32, DateTimeKind.Local).AddTicks(8315));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 10, 9, 1, 17, 32, DateTimeKind.Local).AddTicks(8316));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Committees");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 3, 10, 42, 27, 225, DateTimeKind.Local).AddTicks(2432));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 3, 10, 42, 27, 226, DateTimeKind.Local).AddTicks(2080));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 3, 10, 42, 27, 226, DateTimeKind.Local).AddTicks(2088));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 3, 10, 42, 27, 226, DateTimeKind.Local).AddTicks(2089));
        }
    }
}
