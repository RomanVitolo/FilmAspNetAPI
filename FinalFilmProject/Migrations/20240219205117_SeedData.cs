using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalFilmProject.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "BirthDate", "Name", "PhotoUrl" },
                values: new object[,]
                {
                    { 5, new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jim Carrey", null },
                    { 6, new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert Downey Jr.", null },
                    { 7, new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chris Evans", null }
                });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "InCinemas", "Poster", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 2, true, null, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avengers: Endgame" },
                    { 3, false, null, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avengers: Infinity Wars" },
                    { 4, false, null, new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sonic the Hedgehog" },
                    { 5, false, null, new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emma" },
                    { 6, false, null, new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wonder Woman 1984" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Aventura" },
                    { 5, "Animación" },
                    { 6, "Suspenso" },
                    { 7, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "FilmActors",
                columns: new[] { "ActorId", "FilmId", "MainCharacter", "Order" },
                values: new object[,]
                {
                    { 5, 4, "Dr. Ivo Robotnik", 1 },
                    { 6, 2, "Tony Stark", 1 },
                    { 6, 3, "Tony Stark", 1 },
                    { 7, 2, "Steve Rogers", 2 },
                    { 7, 3, "Steve Rogers", 2 }
                });

            migrationBuilder.InsertData(
                table: "FilmGenres",
                columns: new[] { "FilmId", "GenreId" },
                values: new object[,]
                {
                    { 2, 4 },
                    { 3, 4 },
                    { 4, 4 },
                    { 6, 4 },
                    { 2, 6 },
                    { 3, 6 },
                    { 5, 6 },
                    { 6, 6 },
                    { 5, 7 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FilmActors",
                keyColumns: new[] { "ActorId", "FilmId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "FilmActors",
                keyColumns: new[] { "ActorId", "FilmId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "FilmActors",
                keyColumns: new[] { "ActorId", "FilmId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "FilmActors",
                keyColumns: new[] { "ActorId", "FilmId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "FilmActors",
                keyColumns: new[] { "ActorId", "FilmId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "FilmGenres",
                keyColumns: new[] { "FilmId", "GenreId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
