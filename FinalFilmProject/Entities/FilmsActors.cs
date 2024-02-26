namespace FinalFilmProject.Entities
{
    public class FilmsActors
    {
        public int ActorId { get; set; }
        public int FilmId { get; set; }
        public string MainCharacter { get; set; }
        public int Order { get; set; }
        public Actor Actor { get; set; }
        public Film Film { get; set; }
    }
}