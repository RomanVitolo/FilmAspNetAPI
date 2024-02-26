using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace FinalFilmProject.Migrations
{
    public partial class CinemaRoomLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmActors_CinemaRooms_CinemaRoomId",
                table: "FilmActors");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmGenres_CinemaRooms_CinemaRoomId",
                table: "FilmGenres");

            migrationBuilder.DropIndex(
                name: "IX_FilmGenres_CinemaRoomId",
                table: "FilmGenres");

            migrationBuilder.DropIndex(
                name: "IX_FilmActors_CinemaRoomId",
                table: "FilmActors");

            migrationBuilder.DropColumn(
                name: "CinemaRoomId",
                table: "FilmGenres");

            migrationBuilder.DropColumn(
                name: "CinemaRoomId",
                table: "FilmActors");

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "CinemaRooms",
                type: "geography",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "CinemaRooms");

            migrationBuilder.AddColumn<int>(
                name: "CinemaRoomId",
                table: "FilmGenres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CinemaRoomId",
                table: "FilmActors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenres_CinemaRoomId",
                table: "FilmGenres",
                column: "CinemaRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmActors_CinemaRoomId",
                table: "FilmActors",
                column: "CinemaRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmActors_CinemaRooms_CinemaRoomId",
                table: "FilmActors",
                column: "CinemaRoomId",
                principalTable: "CinemaRooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmGenres_CinemaRooms_CinemaRoomId",
                table: "FilmGenres",
                column: "CinemaRoomId",
                principalTable: "CinemaRooms",
                principalColumn: "Id");
        }
    }
}
