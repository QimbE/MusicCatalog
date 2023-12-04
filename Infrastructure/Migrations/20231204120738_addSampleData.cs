using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("048fdf80-2296-4cd9-9e98-5a4f07e591bc"), "Known for their experimental and abrasive sound, Death Grips is an influential American experimental hip-hop trio. Combining elements of punk, industrial, and electronic music, they push the boundaries of conventional genres with their intense and raw sonic expressions.", "Death Grips" },
                    { new Guid("a8bbf255-aafd-432d-9ad8-ca2957fd83c5"), "Sir Elton John is a legendary British singer, songwriter, and pianist, known for his flamboyant style and timeless hits. With a career spanning decades, he's a global icon and one of the best-selling music artists in the world.", "Elton John" },
                    { new Guid("da779457-fdaf-4f58-aaba-e385fbbdc603"), "Dua Lipa, a British pop sensation, has taken the music world by storm with her sultry vocals and chart-topping hits. Known for her genre-blending sound and captivating performances, she has become a prominent figure in contemporary pop music.", "Dua Lipa" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("aa8087bd-cccb-4013-b627-e6265a9611f9"), "Pop music" },
                    { new Guid("aaa5fd8d-2a2d-4e8a-9acc-2f5778daed8c"), "Punk-rap" }
                });

            migrationBuilder.InsertData(
                table: "Releases",
                columns: new[] { "Id", "AuthorId", "Description", "LinkToCover", "Name", "ReleaseDate", "TypeId" },
                values: new object[,]
                {
                    { new Guid("044efc07-7658-4c4a-96cd-119956142480"), new Guid("da779457-fdaf-4f58-aaba-e385fbbdc603"), "Some release description", "https://avatars.yandex.net/get-music-content/5457712/6fcf6795.a.18635265-1/m1000x1000?webp=false", "Cold Heart", new DateOnly(2021, 8, 13), 2 },
                    { new Guid("9ed69dde-e57d-42ef-86f3-e10b969ce5e3"), new Guid("048fdf80-2296-4cd9-9e98-5a4f07e591bc"), "Some release description", "https://sun9-78.userapi.com/impg/c857232/v857232147/176469/EkCl5P81Q8E.jpg?size=2048x2048&quality=96&sign=615509ee8cff2be10a48b45d2e0a507a&type=album", "Exmilitary", new DateOnly(2011, 4, 25), 3 }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "AudioLink", "GenreId", "Name", "ReleaseId" },
                values: new object[,]
                {
                    { new Guid("1f8f5423-b7f9-4804-ab66-5880c5476a0a"), "about:blank", new Guid("aa8087bd-cccb-4013-b627-e6265a9611f9"), "Cold Heart", new Guid("044efc07-7658-4c4a-96cd-119956142480") },
                    { new Guid("33f61113-cd1c-4218-9a1f-2e27ef0ed4f9"), "about:blank", new Guid("aaa5fd8d-2a2d-4e8a-9acc-2f5778daed8c"), "Beware", new Guid("9ed69dde-e57d-42ef-86f3-e10b969ce5e3") },
                    { new Guid("875d4ada-612b-43f8-b005-88fc57ced0be"), "about:blank", new Guid("aaa5fd8d-2a2d-4e8a-9acc-2f5778daed8c"), "Guillotine", new Guid("9ed69dde-e57d-42ef-86f3-e10b969ce5e3") },
                    { new Guid("a3b492ca-c29b-40af-86c9-4192acc369c1"), "about:blank", new Guid("aaa5fd8d-2a2d-4e8a-9acc-2f5778daed8c"), "Lord of the Game", new Guid("9ed69dde-e57d-42ef-86f3-e10b969ce5e3") },
                    { new Guid("de257ed2-5f8b-43c2-be0f-b291c263473a"), "about:blank", new Guid("aaa5fd8d-2a2d-4e8a-9acc-2f5778daed8c"), "Spread Eagle Cross The Block", new Guid("9ed69dde-e57d-42ef-86f3-e10b969ce5e3") }
                });

            migrationBuilder.InsertData(
                table: "SongArtist",
                columns: new[] { "ArtistId", "SongId" },
                values: new object[] { new Guid("a8bbf255-aafd-432d-9ad8-ca2957fd83c5"), new Guid("1f8f5423-b7f9-4804-ab66-5880c5476a0a") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SongArtist",
                keyColumns: new[] { "ArtistId", "SongId" },
                keyValues: new object[] { new Guid("a8bbf255-aafd-432d-9ad8-ca2957fd83c5"), new Guid("1f8f5423-b7f9-4804-ab66-5880c5476a0a") });

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("33f61113-cd1c-4218-9a1f-2e27ef0ed4f9"));

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("875d4ada-612b-43f8-b005-88fc57ced0be"));

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("a3b492ca-c29b-40af-86c9-4192acc369c1"));

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("de257ed2-5f8b-43c2-be0f-b291c263473a"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("a8bbf255-aafd-432d-9ad8-ca2957fd83c5"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("aaa5fd8d-2a2d-4e8a-9acc-2f5778daed8c"));

            migrationBuilder.DeleteData(
                table: "Releases",
                keyColumn: "Id",
                keyValue: new Guid("9ed69dde-e57d-42ef-86f3-e10b969ce5e3"));

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("1f8f5423-b7f9-4804-ab66-5880c5476a0a"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("048fdf80-2296-4cd9-9e98-5a4f07e591bc"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("aa8087bd-cccb-4013-b627-e6265a9611f9"));

            migrationBuilder.DeleteData(
                table: "Releases",
                keyColumn: "Id",
                keyValue: new Guid("044efc07-7658-4c4a-96cd-119956142480"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("da779457-fdaf-4f58-aaba-e385fbbdc603"));
        }
    }
}
