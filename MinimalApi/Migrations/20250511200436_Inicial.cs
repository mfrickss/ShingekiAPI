using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tabelaTitans",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: true),
                    portador = table.Column<string>(type: "TEXT", nullable: true),
                    idadePortador = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tabelaTitans", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tabelaTitans");
        }
    }
}
