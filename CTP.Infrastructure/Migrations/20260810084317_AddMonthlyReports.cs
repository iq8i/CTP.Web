using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMonthlyReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthlyReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    OrganizationEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    PreparerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ChangeType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CurrentStage = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ChangeSummary = table.Column<string>(type: "TEXT", nullable: true),
                    ReadinessScore = table.Column<int>(type: "INTEGER", nullable: false),
                    AdoptionScore = table.Column<int>(type: "INTEGER", nullable: false),
                    AdkarAwareness = table.Column<int>(type: "INTEGER", nullable: false),
                    AdkarDesire = table.Column<int>(type: "INTEGER", nullable: false),
                    AdkarKnowledge = table.Column<int>(type: "INTEGER", nullable: false),
                    AdkarAbility = table.Column<int>(type: "INTEGER", nullable: false),
                    AdkarReinforcement = table.Column<int>(type: "INTEGER", nullable: false),
                    Obstacles = table.Column<string>(type: "TEXT", nullable: true),
                    InitialRecommendation = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ApproverNotes = table.Column<string>(type: "TEXT", nullable: true),
                    EvidenceLinks = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyReports_OrganizationEntities_OrganizationEntityId",
                        column: x => x.OrganizationEntityId,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonthlyReports_Users_PreparerId",
                        column: x => x.PreparerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 10, 11, 43, 16, 753, DateTimeKind.Local).AddTicks(1054));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 10, 11, 43, 16, 753, DateTimeKind.Local).AddTicks(1122));

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

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyReports_OrganizationEntityId",
                table: "MonthlyReports",
                column: "OrganizationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyReports_PreparerId",
                table: "MonthlyReports",
                column: "PreparerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyReports");

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 10, 9, 1, 17, 33, DateTimeKind.Local).AddTicks(4274));

            migrationBuilder.UpdateData(
                table: "Committees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 8, 10, 9, 1, 17, 33, DateTimeKind.Local).AddTicks(4343));

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
    }
}
