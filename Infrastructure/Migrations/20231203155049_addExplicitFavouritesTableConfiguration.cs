using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addExplicitFavouritesTableConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongUser_Songs_FavouriteSongsId",
                table: "SongUser");

            migrationBuilder.DropForeignKey(
                name: "FK_SongUser_Users_UsersWhoAddedId",
                table: "SongUser");

            migrationBuilder.RenameColumn(
                name: "UsersWhoAddedId",
                table: "SongUser",
                newName: "SongId");

            migrationBuilder.RenameColumn(
                name: "FavouriteSongsId",
                table: "SongUser",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SongUser_UsersWhoAddedId",
                table: "SongUser",
                newName: "IX_SongUser_SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_SongUser_Songs_SongId",
                table: "SongUser",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongUser_Users_UserId",
                table: "SongUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongUser_Songs_SongId",
                table: "SongUser");

            migrationBuilder.DropForeignKey(
                name: "FK_SongUser_Users_UserId",
                table: "SongUser");

            migrationBuilder.RenameColumn(
                name: "SongId",
                table: "SongUser",
                newName: "UsersWhoAddedId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SongUser",
                newName: "FavouriteSongsId");

            migrationBuilder.RenameIndex(
                name: "IX_SongUser_SongId",
                table: "SongUser",
                newName: "IX_SongUser_UsersWhoAddedId");

            migrationBuilder.AddForeignKey(
                name: "FK_SongUser_Songs_FavouriteSongsId",
                table: "SongUser",
                column: "FavouriteSongsId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongUser_Users_UsersWhoAddedId",
                table: "SongUser",
                column: "UsersWhoAddedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
