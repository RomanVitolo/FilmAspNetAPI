namespace FinalFilmProject.DTOS;

public class FilmDetailsDTO : FilmDTO
{
    public List<GenreDTO> Genres { get; set; }
    public List<FilmActorDetailsDTO> Actors { get; set; }
}