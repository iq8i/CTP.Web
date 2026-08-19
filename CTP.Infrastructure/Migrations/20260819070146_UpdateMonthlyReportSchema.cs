using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMonthlyReportSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityCompletionRate",
                table: "MonthlyReports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AdoptionBarriers",
                table: "MonthlyReports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AffectedGroups",
                table: "MonthlyReports",
                type: "TEXT",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExecutedActivities",
                table: "MonthlyReports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImpactScope",
                table: "MonthlyReports",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImprovementOpportunities",
                table: "MonthlyReports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MostInNeedGroup",
                table: "MonthlyReports",
                type: "TEXT",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequiredSupport",
                table: "MonthlyReports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Risks",
                table: "MonthlyReports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuccessStories",
                table: "MonthlyReports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatWillChange",
                table: "MonthlyReports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatWillNotChange",
                table: "MonthlyReports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhyImportant",
                table: "MonthlyReports",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityCompletionRate",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "AdoptionBarriers",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "AffectedGroups",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "ExecutedActivities",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "ImpactScope",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "ImprovementOpportunities",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "MostInNeedGroup",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "RequiredSupport",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "Risks",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "SuccessStories",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "WhatWillChange",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "WhatWillNotChange",
                table: "MonthlyReports");

            migrationBuilder.DropColumn(
                name: "WhyImportant",
                table: "MonthlyReports");
        }
    }
}
