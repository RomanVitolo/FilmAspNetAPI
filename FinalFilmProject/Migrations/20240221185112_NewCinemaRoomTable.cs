using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalFilmProject.Migrations
{
    public partial class NewCinemaRoomTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "CinemaRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmsCinemaRooms",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    CinemaRoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmsCinemaRooms", x => new { x.FilmId, x.CinemaRoomId });
                    table.ForeignKey(
                        name: "FK_FilmsCinemaRooms_CinemaRooms_CinemaRoomId",
                        column: x => x.CinemaRoomId,
                        principalTable: "CinemaRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmsCinemaRooms_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenres_CinemaRoomId",
                table: "FilmGenres",
                column: "CinemaRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmActors_CinemaRoomId",
                table: "FilmActors",
                column: "CinemaRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmsCinemaRooms_CinemaRoomId",
                table: "FilmsCinemaRooms",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmActors_CinemaRooms_CinemaRoomId",
                table: "FilmActors");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmGenres_CinemaRooms_CinemaRoomId",
                table: "FilmGenres");

            migrationBuilder.DropTable(
                name: "FilmsCinemaRooms");

            migrationBuilder.DropTable(
                name: "CinemaRooms");

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
        }
    }
}
