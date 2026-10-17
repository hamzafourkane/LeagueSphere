using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueSphere.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjoutVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoteDomicile",
                table: "Matchs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VoteExterieur",
                table: "Matchs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VoteNul",
                table: "Matchs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteDomicile",
                table: "Matchs");

            migrationBuilder.DropColumn(
                name: "VoteExterieur",
                table: "Matchs");

            migrationBuilder.DropColumn(
                name: "VoteNul",
                table: "Matchs");
        }
    }
}

