using AutoMapper;
using FinalFilmProject.DTOS;
using FinalFilmProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace FinalFilmProject.Controllers;

[Route("api/cinemaRoom")]
[ApiController]
public class CinemaRoomController : CustomBaseController
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly GeometryFactory _geometryFactory;

    public CinemaRoomController(ApplicationDbContext context, IMapper mapper, GeometryFactory geometryFactory) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
        _geometryFactory = geometryFactory;
    }

    [HttpGet]
    public async Task<ActionResult<List<CinemaRoomDTO>>> Get()
    {
        return await Get<CinemaRoom, CinemaRoomDTO>();
    }

    [HttpGet("{id:int}", Name = "getCinemaRoom")]
    public async Task<ActionResult<CinemaRoomDTO>> Get(int id)
    {
        return await GetById<CinemaRoom, CinemaRoomDTO>(id);
    }

    [HttpGet("nearby")]
    public async Task<ActionResult<List<CinemaRoomDistanceDTO>>> Get
        ([FromQuery] CinemaRoomDistanceFilterDTO filterDto)
    {
        var userLocation = _geometryFactory.CreatePoint(new Coordinate(filterDto.Longitude, filterDto.Latitude));

        var cinemaRoom = await _context.CinemaRooms
            .OrderBy(x => x.Location.Distance(userLocation))
            .Where(x => x.Location.IsWithinDistance(userLocation, filterDto.DistanceInKms * 1000))
            .Select(x => new CinemaRoomDistanceDTO
            {
                Id = x.Id,
                Name = x.Name,
                Latitude = x.Location.Y,
                Longitude = x.Location.X,
                DistanceInMeters = Math.Round(x.Location.Distance(userLocation))
            })
            .ToListAsync();

        return cinemaRoom;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CinemaRoomCreationDTO cinemaRoomCreationDto)
    {
        return await Post<CinemaRoomCreationDTO, CinemaRoom, CinemaRoomDTO>
            (cinemaRoomCreationDto, "getCinemaRoom");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] CinemaRoomCreationDTO cinemaRoomCreationDto)
    {
        return await Put<CinemaRoomCreationDTO, CinemaRoom>(id, cinemaRoomCreationDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        return await Delete<CinemaRoom>(id);
    }
}