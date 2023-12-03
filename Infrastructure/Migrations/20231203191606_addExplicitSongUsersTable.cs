using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addExplicitSongUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongUser_Songs_SongId",
                table: "SongUser");

            migrationBuilder.DropForeignKey(
                name: "FK_SongUser_Users_UserId",
                table: "SongUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongUser",
                table: "SongUser");

            migrationBuilder.RenameTable(
                name: "SongUser",
                newName: "SongUsers");

            migrationBuilder.RenameIndex(
                name: "IX_SongUser_SongId",
                table: "SongUsers",
                newName: "IX_SongUsers_SongId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongUsers",
                table: "SongUsers",
                columns: new[] { "UserId", "SongId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SongUsers_Songs_SongId",
                table: "SongUsers",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongUsers_Users_UserId",
                table: "SongUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongUsers_Songs_SongId",
                table: "SongUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SongUsers_Users_UserId",
                table: "SongUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongUsers",
                table: "SongUsers");

            migrationBuilder.RenameTable(
                name: "SongUsers",
                newName: "SongUser");

            migrationBuilder.RenameIndex(
                name: "IX_SongUsers_SongId",
                table: "SongUser",
                newName: "IX_SongUser_SongId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongUser",
                table: "SongUser",
                columns: new[] { "UserId", "SongId" });

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
    }
}
