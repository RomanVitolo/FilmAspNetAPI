using Microsoft.EntityFrameworkCore;

namespace FinalFilmProject.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPageParameters<T>(this HttpContext httpContext, IQueryable<T> queryable,
            int registersPerPage)
        {
            double registers = await queryable.CountAsync();
            double pagesAmount = Math.Ceiling(registers / registersPerPage);
            httpContext.Response.Headers.Add("pagesAmount", pagesAmount.ToString());
        }
    }
}