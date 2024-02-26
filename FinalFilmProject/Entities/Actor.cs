using System.ComponentModel.DataAnnotations;

namespace FinalFilmProject.Entities
{
    public class Actor : IId
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhotoUrl { get; set; }
        public List<FilmsActors> FilmsActors { get; set; }
    }
}