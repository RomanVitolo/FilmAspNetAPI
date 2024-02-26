using System.Security.Claims;
using AutoMapper;
using FinalFilmProject.DTOS;
using FinalFilmProject.Entities;
using FinalFilmProject.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FinalFilmProject.Controllers
{
    [Route("api/films/{filmId:int}/reviews")]
    [ServiceFilter(typeof(FilmExistAttribute))]
    public class ReviewsController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReviewsController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewDTO>>> Get(int filmId, [FromQuery] PageDTO pageDto)
        {
            var queryable = _context.Reviews.Include(x => x.User).AsQueryable();
            queryable = queryable.Where(x => x.FilmId == filmId);
            return await Get<Review, ReviewDTO>(pageDto, queryable);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int filmId, [FromBody] ReviewCreationDTO reviewCreationDto)
        {    
            var userId = HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            var existReview = await _context.Reviews
                .AnyAsync(x => x.FilmId  == filmId&& x.UserId == userId);
            if (existReview) BadRequest("A review of the movie already exists ");

            var review = _mapper.Map<Review>(reviewCreationDto);
            review.FilmId = filmId;
            review.UserId = userId;
            _context.Add(review);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{reviewId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int filmId, int reviewId, [FromBody] ReviewCreationDTO reviewCreationDto)
        {       
            var reviewDB = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            if (reviewDB == null) NotFound();

            var userId = HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (reviewDB.UserId != userId) return BadRequest("You do not have permission to edit a review");

            reviewDB = _mapper.Map(reviewCreationDto, reviewDB);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{reviewId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int reviewId)
        {
            var reviewDB = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            if (reviewDB == null) NotFound();

            var userId = HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (reviewDB.UserId != userId) return Forbid();

            _context.Remove(reviewDB);
            await _context.SaveChangesAsync();
            return NoContent();
        }     
    }
}