using AutoMapper;   
using FinalFilmProject.DTOS;
using FinalFilmProject.Entities;
using FinalFilmProject.Helpers;
using FinalFilmProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

namespace FinalFilmProject.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFilesStorage _filesStorage;
        private const string _container = "actors";

        public ActorsController(ApplicationDbContext context, IMapper mapper, IFilesStorage filesStorage) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _filesStorage = filesStorage;
        }

        [HttpGet]     //Page
        public async Task<ActionResult<List<ActorDTO>>> GetActor([FromQuery] PageDTO pageDto)
        {
            return await Get<Actor, ActorDTO>(pageDto);
        }

        [HttpGet("{id:int}", Name = "getActor")]
        public async Task<ActionResult<ActorDTO>> GetActorById(int id)
        {
            return await GetById<Actor, ActorDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostActor([FromForm] ActorCreationDTO actorCreationDto)
        {
            var entity = _mapper.Map<Actor>(actorCreationDto);

            if (actorCreationDto.PhotoUrl != null)
            {
                entity.PhotoUrl = await _filesStorage.SaveFile(_container, actorCreationDto.PhotoUrl);
            }
            
            _context.Add(entity);
            await _context.SaveChangesAsync();                     

            var dto = _mapper.Map<ActorDTO>(entity);
            return new CreatedAtRouteResult("getActor", new {id = entity.Id}, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutActor(int id, [FromForm] ActorCreationDTO actorCreationDto)
        {
            var actorDB = await _context.Actors.FirstOrDefaultAsync(x => x.Id == id);  
            if (actorDB == null) return NotFound();

            actorDB = _mapper.Map(actorCreationDto, actorDB);
            
            if (actorCreationDto.PhotoUrl != null)
            {
                actorDB.PhotoUrl =
                    await _filesStorage.EditFile(actorCreationDto.PhotoUrl, _container, actorDB.PhotoUrl);
            }
            
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchActor(int id, [FromBody] JsonPatchDocument<ActorPatchDTO> patchDocument)
        {
            return await Patch<Actor, ActorPatchDTO>(id, patchDocument);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActor(int id)
        {
            return await Delete<Actor>(id);
        }    
    }
}      