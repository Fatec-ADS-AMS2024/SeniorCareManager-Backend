using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SeniorCareManager.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carrier",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    corporatename = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tradename = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cpfcnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    number = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    district = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    addresscomplement = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    state = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    postalcode = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    phone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carrier", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productgroup",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productgroup", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "carrier",
                columns: new[] { "id", "addresscomplement", "city", "corporatename", "cpfcnpj", "district", "email", "number", "phone", "postalcode", "state", "street", "tradename" },
                values: new object[,]
                {
                    { 1, "Próximo ao banco", "São Paulo", "Transportes ABC LTDA", "12345678000190", "Centro", "contato@abctransportes.com", "123", "11987654321", "01001000", "SP", "Rua das Flores", "ABC Transportes" },
                    { 2, "Esquina com a Rua Augusta", "São Paulo", "Expresso XYZ S/A", "98765432000180", "Bela Vista", "expresso@xyz.com.br", "456", "11976543210", "01311000", "SP", "Avenida Paulista", "Expresso XYZ" },
                    { 3, "Próximo ao metrô", "Rio de Janeiro", "Translogística EFG ME", "22334455000122", "Centro", "contato@efgtrans.com.br", "789", "21987654321", "20040001", "RJ", "Avenida Rio Branco", "EFG Transportes" }
                });

            migrationBuilder.InsertData(
                table: "productgroup",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Medicamentos" },
                    { 2, "Equipamentos Médicos" },
                    { 3, "Suplementos" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carrier");

            migrationBuilder.DropTable(
                name: "productgroup");
        }
    }
}
