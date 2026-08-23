using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommitteeRecommendation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommitteeRecommendation",
                table: "MonthlyReports",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommitteeRecommendation",
                table: "MonthlyReports");
        }
    }
}
