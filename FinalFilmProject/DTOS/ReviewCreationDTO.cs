using System.ComponentModel.DataAnnotations;

namespace FinalFilmProject.DTOS
{
    public class ReviewCreationDTO
    {
        public string Comment { get; set; }
        [Range(1,5)]
        public int Score { get; set; }
    }
}