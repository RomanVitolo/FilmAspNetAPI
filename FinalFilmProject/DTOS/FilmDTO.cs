namespace FinalFilmProject.DTOS
{
    public class FilmDTO
    {
        public int Id { get; set; }    
        public string Title { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
    }
}