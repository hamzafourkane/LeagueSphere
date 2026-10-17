using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueSphere.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreationInitiale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false),
                    MatchsJoues = table.Column<int>(type: "int", nullable: false),
                    Victoires = table.Column<int>(type: "int", nullable: false),
                    Defaites = table.Column<int>(type: "int", nullable: false),
                    Nuls = table.Column<int>(type: "int", nullable: false),
                    ButsPour = table.Column<int>(type: "int", nullable: false),
                    ButsContre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matchs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateMatch = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EquipeDomicileId = table.Column<int>(type: "int", nullable: false),
                    EquipeExterieurId = table.Column<int>(type: "int", nullable: false),
                    ScoreDomicile = table.Column<int>(type: "int", nullable: false),
                    ScoreExterieur = table.Column<int>(type: "int", nullable: false),
                    EstTermine = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matchs_Equipes_EquipeDomicileId",
                        column: x => x.EquipeDomicileId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matchs_Equipes_EquipeExterieurId",
                        column: x => x.EquipeExterieurId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_EquipeDomicileId",
                table: "Matchs",
                column: "EquipeDomicileId");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_EquipeExterieurId",
                table: "Matchs",
                column: "EquipeExterieurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matchs");

            migrationBuilder.DropTable(
                name: "Equipes");
        }
    }
}

