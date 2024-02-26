using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace FinalFilmProject.Helpers;

public class FilmExistAttribute : Attribute, IAsyncResourceFilter
{
    private readonly ApplicationDbContext _dbContext;

    public FilmExistAttribute(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var filmIdObject = context.HttpContext.Request.RouteValues["filmId"];
        if (filmIdObject == null) return;

        var filmId = int.Parse(filmIdObject.ToString());
        
        var existFilm = await _dbContext.Films.AnyAsync(x => x.Id == filmId);
        if (!existFilm)
        {
            context.Result = new NotFoundResult();
        }
        else
        {
            await next();
        }
    }
}