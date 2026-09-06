using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRecommendationPlacementFlags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IncludeInExecutiveSummary",
                table: "Recommendations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeInImpactDashboard",
                table: "Recommendations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeInInstitutionalReport",
                table: "Recommendations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ChairReviewNotes",
                table: "Recommendations",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedDate",
                table: "Recommendations",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncludeInExecutiveSummary",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "IncludeInImpactDashboard",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "IncludeInInstitutionalReport",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "ChairReviewNotes",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "ReviewedDate",
                table: "Recommendations");
        }
    }
}
