namespace FinalFilmProject.Entities
{
    public class FilmsCinemaRoom
    {
        public int FilmId { get; set; }
        public int CinemaRoomId { get; set; }
        public Film Film { get; set; }
        public CinemaRoom CinemaRoom { get; set; }
    }
}