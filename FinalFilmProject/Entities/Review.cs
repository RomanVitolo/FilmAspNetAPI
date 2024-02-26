using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FinalFilmProject.Entities
{
    public class Review : IId
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        [Range(1, 5)]
        public int Score { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}