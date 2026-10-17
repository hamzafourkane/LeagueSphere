using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueSphere.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ajoutfeeds1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_PostLikes_PostLikeId",
                table: "PostLikes");

            migrationBuilder.DropIndex(
                name: "IX_PostLikes_PostLikeId",
                table: "PostLikes");

            migrationBuilder.DropColumn(
                name: "PostLikeId",
                table: "PostLikes");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_PostId",
                table: "PostLikes",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_Posts_PostId",
                table: "PostLikes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_Posts_PostId",
                table: "PostLikes");

            migrationBuilder.DropIndex(
                name: "IX_PostLikes_PostId",
                table: "PostLikes");

            migrationBuilder.AddColumn<int>(
                name: "PostLikeId",
                table: "PostLikes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_PostLikeId",
                table: "PostLikes",
                column: "PostLikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_PostLikes_PostLikeId",
                table: "PostLikes",
                column: "PostLikeId",
                principalTable: "PostLikes",
                principalColumn: "Id");
        }
    }
}

