using System.Linq.Dynamic.Core;
using AutoMapper;
using FinalFilmProject.DTOS;
using FinalFilmProject.Entities;
using FinalFilmProject.Helpers;
using FinalFilmProject.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalFilmProject.Controllers
{
    [ApiController]
    [Route("api/films")]
    public class FilmsController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFilesStorage _filesStorage;
        private readonly ILogger<FilmsController> _logger;
        private const string _container = "films";

        public FilmsController(ApplicationDbContext context, IMapper mapper, IFilesStorage filesStorage, ILogger<FilmsController> logger) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _filesStorage = filesStorage;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<FilmsIndexDTO>> GetFilms()
        {
            var top = 5;
            var today = DateTime.Today;

            var upcomingReleases = await _context.Films
                .Where(x => x.ReleaseDate > today)
                .OrderBy(x => x.ReleaseDate)
                .Take(top)
                .ToListAsync();

            var inCinemas = await _context.Films
                .Where(x => x.InCinemas)
                .Take(top)
                .ToListAsync();

            var result = new FilmsIndexDTO();
            result.UpcomingReleases = _mapper.Map<List<FilmDTO>>(upcomingReleases);
            result.InCinemas = _mapper.Map<List<FilmDTO>>(inCinemas);
            return result;   
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<FilmDTO>>> Filter([FromQuery] FilmFilterDTO filmFilterDto)
        {
            var filmQueryable = _context.Films.AsQueryable();
            
            if (!string.IsNullOrEmpty(filmFilterDto.Title))
            {
                filmQueryable = filmQueryable.Where(x => x.Title.Contains(filmFilterDto.Title));
            }

            if (filmFilterDto.InCinemas)
            {
                filmQueryable = filmQueryable.Where(x => x.InCinemas);
            }

            if (filmFilterDto.UpcomingReleases)
            {   
                var today = DateTime.Today;
                filmQueryable = filmQueryable.Where(x => x.ReleaseDate > today);
            }

            if (filmFilterDto.GenreId != 0)
            {
                filmQueryable = filmQueryable.Where(x => x.FilmsGenres.Select(y => y.GenreId)
                    .Contains(filmFilterDto.GenreId));
            }

            if (!string.IsNullOrEmpty(filmFilterDto.OrderField))
            {
                var orderType = filmFilterDto.AscendingOrder ? "ascending" : "descending";
                try
                {
                    filmQueryable = filmQueryable.OrderBy($"{filmFilterDto.OrderField} {orderType}");
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message, e);
                }   
            }

            await HttpContext.InsertPageParameters(filmQueryable, filmFilterDto.RecordsPerPage);

            var films = await filmQueryable.Page(filmFilterDto.PageDto).ToListAsync();

            return _mapper.Map<List<FilmDTO>>(films);
        }

        [HttpGet("{id}", Name = "getFilm")]
        public async Task<ActionResult<FilmDetailsDTO>> GetFilm(int id)
        {
            var film = _context.Films
                .Include(x => x.FilmsActors)
                .ThenInclude(x => x.Actor)
                .Include(x => x.FilmsGenres)
                .ThenInclude(x => x.Genre)
                .FirstOrDefault(x => x.Id == id);  
            if (film == null) return NotFound();

            film.FilmsActors = film.FilmsActors.OrderBy(x => x.Order).ToList();

            return _mapper.Map<FilmDetailsDTO>(film);
        }

        [HttpPost]
        public async Task<ActionResult> PostNewFilm( [FromForm] FilmCreationDTO filmCreationDto)
        {
            var film = _mapper.Map<Film>(filmCreationDto);
            if (filmCreationDto.Poster != null)
            {
                film.Poster = await _filesStorage.SaveFile(_container, filmCreationDto.Poster);
            }
            
            AssignActorsOrder(film);
            _context.Add(film);
            await _context.SaveChangesAsync();                     

            var dto = _mapper.Map<FilmDTO>(film);
            return new CreatedAtRouteResult("getFilm", new {id = film.Id}, dto);
        }

        private void AssignActorsOrder(Film film)
        {
            if (film.FilmsActors != null)
            {
                for (int i = 0; i < film.FilmsActors.Count; i++)
                {
                    film.FilmsActors[i].Order = i;
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutFilm(int id, [FromForm] FilmCreationDTO filmCreationDto)
        {
            var filmDB = await _context.Films
                .Include(x => x.FilmsActors)
                .Include(x => x.FilmsGenres)
                .FirstOrDefaultAsync(x => x.Id == id);  
            if (filmDB == null) return NotFound();

            filmDB = _mapper.Map(filmCreationDto, filmDB);
            
            if (filmCreationDto.Poster != null)
            {
                filmDB.Poster =
                    await _filesStorage.EditFile(filmCreationDto.Poster, _container, filmDB.Poster);
            }
            
            AssignActorsOrder(filmDB);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchFilm(int id, [FromBody] JsonPatchDocument<FilmPatchDTO> patchDocument)
        {
            return await Patch<Film, FilmPatchDTO>(id, patchDocument);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFilm(int id)
        {
            return await Delete<Film>(id);
        }
    }
}