using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRecommendationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecommendationNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MonthlyReportId = table.Column<int>(type: "INTEGER", nullable: false),
                    GapType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SuggestedAction = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ActionOwner = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Duration = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SuccessIndicator = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ExpectedImpact = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendations_MonthlyReports_MonthlyReportId",
                        column: x => x.MonthlyReportId,
                        principalTable: "MonthlyReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_MonthlyReportId",
                table: "Recommendations",
                column: "MonthlyReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recommendations");
        }
    }
}
