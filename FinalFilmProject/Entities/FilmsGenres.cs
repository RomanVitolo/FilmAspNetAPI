namespace FinalFilmProject.Entities;

public class FilmsGenres
{
    public int GenreId { get; set; }
    public int FilmId { get; set; }
    public Genre Genre { get; set; }
    public Film Film { get; set; }
}