namespace FinalFilmProject.DTOS
{
    public class FilmFilterDTO
    {
        public int Page { get; set; } = 1;
        public int RecordsPerPage { get; set; } = 10;      
        public PageDTO PageDto => new() {Page = Page, RegistersPerPage = RecordsPerPage};        
        public string Title { get; set; }
        public int GenreId { get; set; }
        public bool InCinemas { get; set; }    
        public bool UpcomingReleases { get; set; }
        public string OrderField { get; set; }
        public bool AscendingOrder { get; set; } = true;
    }
}
        
