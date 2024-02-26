using System.ComponentModel.DataAnnotations;
using FinalFilmProject.Entities;

namespace FinalFilmProject.DTOS
{
    public class ActorDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhotoUrl { get; set; }     
    }
}