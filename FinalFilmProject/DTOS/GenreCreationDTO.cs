using System.ComponentModel.DataAnnotations;

namespace FinalFilmProject.DTOS
{
    public class GenreCreationDTO
    {                    
        [Required]
        [StringLength(40)]
        public string Name { get; set; }    
    }
}