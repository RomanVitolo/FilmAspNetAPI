using AutoMapper;
using FinalFilmProject.DTOS;
using FinalFilmProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalFilmProject.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : CustomBaseController
    {   

        public GenresController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
           
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> GetGenres()
        {
            return await Get<Genre, GenreDTO>();
        }

        [HttpGet("{id:int}", Name = "getGenre")]
        public async Task<ActionResult<GenreDTO>> GetGenre(int id)
        {
            return await GetById<Genre, GenreDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostGenre([FromBody] GenreCreationDTO genreCreationDto)
        {
            return await Post<GenreCreationDTO, Genre, GenreDTO>(genreCreationDto, "getGenre");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutGenre(int id, [FromBody] GenreCreationDTO genreCreationDto)
        {
            return await Put<GenreCreationDTO, Genre>(id, genreCreationDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            return await Delete<Genre>(id);
        }  
    }
}