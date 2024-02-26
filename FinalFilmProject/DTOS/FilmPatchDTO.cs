using System.ComponentModel.DataAnnotations;        

namespace FinalFilmProject.DTOS
{
    public class FilmPatchDTO
    {           
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; } 
    }
}