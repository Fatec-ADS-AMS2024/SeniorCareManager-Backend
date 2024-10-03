using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SeniorCareManager.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class HealthInsurancePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "healthinsuranceplan",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    type = table.Column<int>(type: "integer", maxLength: 1, nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_healthinsuranceplan", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "healthinsuranceplan",
                columns: new[] { "id", "abbreviation", "name", "type" },
                values: new object[,]
                {
                    { 1, "UNI", "Unimed", 2 },
                    { 2, "HAP", "Hapvida", 2 },
                    { 3, "SUS", "Sistema Único de Saúde", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "healthinsuranceplan");
        }
    }
}
