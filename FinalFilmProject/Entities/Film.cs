using System.ComponentModel.DataAnnotations;

namespace FinalFilmProject.Entities
{
    public class Film : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public List<FilmsActors> FilmsActors { get; set; }
        public List<FilmsGenres> FilmsGenres { get; set; }
    }
}