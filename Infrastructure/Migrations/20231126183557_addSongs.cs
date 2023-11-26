using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSongs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReleaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    AudioLink = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Songs_Releases_ReleaseId",
                        column: x => x.ReleaseId,
                        principalTable: "Releases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistSong",
                columns: table => new
                {
                    ArtistsOnFeatId = table.Column<Guid>(type: "uuid", nullable: false),
                    SongsOnFeatId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistSong", x => new { x.ArtistsOnFeatId, x.SongsOnFeatId });
                    table.ForeignKey(
                        name: "FK_ArtistSong_Artists_ArtistsOnFeatId",
                        column: x => x.ArtistsOnFeatId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistSong_Songs_SongsOnFeatId",
                        column: x => x.SongsOnFeatId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongUser",
                columns: table => new
                {
                    FavouriteSongsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersWhoAddedId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongUser", x => new { x.FavouriteSongsId, x.UsersWhoAddedId });
                    table.ForeignKey(
                        name: "FK_SongUser_Songs_FavouriteSongsId",
                        column: x => x.FavouriteSongsId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongUser_Users_UsersWhoAddedId",
                        column: x => x.UsersWhoAddedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSong_SongsOnFeatId",
                table: "ArtistSong",
                column: "SongsOnFeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_GenreId",
                table: "Songs",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_Name",
                table: "Songs",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ReleaseId",
                table: "Songs",
                column: "ReleaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SongUser_UsersWhoAddedId",
                table: "SongUser",
                column: "UsersWhoAddedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistSong");

            migrationBuilder.DropTable(
                name: "SongUser");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
