using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtendRecommendationForQualityGate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cause",
                table: "Recommendations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Escalation",
                table: "Recommendations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Evidence",
                table: "Recommendations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GapEffect",
                table: "Recommendations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImpactMeasure",
                table: "Recommendations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupportDecision",
                table: "Recommendations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Recommendations",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "Cause", "Escalation", "Evidence", "GapEffect", "ImpactMeasure", "SupportDecision" },
                values: new object[] { null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cause",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "Escalation",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "Evidence",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "GapEffect",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "ImpactMeasure",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "SupportDecision",
                table: "Recommendations");
        }
    }
}
