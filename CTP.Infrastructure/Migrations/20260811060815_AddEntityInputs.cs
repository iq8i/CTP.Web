using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityInputs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityInputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrganizationEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    PreparerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityInputs_OrganizationEntities_OrganizationEntityId",
                        column: x => x.OrganizationEntityId,
                        principalTable: "OrganizationEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityInputs_Users_PreparerId",
                        column: x => x.PreparerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityInputs_OrganizationEntityId",
                table: "EntityInputs",
                column: "OrganizationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityInputs_PreparerId",
                table: "EntityInputs",
                column: "PreparerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityInputs");
        }
    }
}
