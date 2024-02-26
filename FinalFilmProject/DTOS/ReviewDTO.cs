namespace FinalFilmProject.DTOS
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }    
        public int Score { get; set; }
        public int FilmId { get; set; }   
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}