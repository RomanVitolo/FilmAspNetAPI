using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace FinalFilmProject.Migrations
{
    public partial class CinemaRoomData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CinemaRooms",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[] { 4, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-69.9118804 18.4826214)"), "Sambil" });

            migrationBuilder.InsertData(
                table: "CinemaRooms",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[] { 5, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-69.856427 18.506934)"), "Megacentro" });

            migrationBuilder.InsertData(
                table: "CinemaRooms",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[] { 6, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-73.986227 40.730898)"), "Village East Cinema" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CinemaRooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CinemaRooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CinemaRooms",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
