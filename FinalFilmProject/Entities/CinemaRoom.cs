using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace FinalFilmProject.Entities
{
    public class CinemaRoom : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }    
        public Point Location { get; set; }  
        public List<FilmsCinemaRoom> FilmsCinemaRooms { get; set; }
    }
}