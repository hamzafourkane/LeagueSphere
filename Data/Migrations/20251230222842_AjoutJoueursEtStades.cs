using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueSphere.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjoutJoueursEtStades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Stade",
                table: "Equipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ville",
                table: "Equipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Joueurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomComplet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Poste = table.Column<int>(type: "int", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Joueurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Joueurs_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Joueurs_EquipeId",
                table: "Joueurs",
                column: "EquipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Joueurs");

            migrationBuilder.DropColumn(
                name: "Stade",
                table: "Equipes");

            migrationBuilder.DropColumn(
                name: "Ville",
                table: "Equipes");
        }
    }
}

