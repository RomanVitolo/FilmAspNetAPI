namespace FinalFilmProject.DTOS
{
    public class FilmsIndexDTO
    {
        public List<FilmDTO> UpcomingReleases { get; set; }
        public List<FilmDTO> InCinemas { get; set; }           
    }
}