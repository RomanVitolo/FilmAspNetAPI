using FinalFilmProject.DTOS;
using System.Linq;

namespace FinalFilmProject.Helpers
{
   public static class QueryableExtensions
   {
      public static IQueryable<T> Page<T>(this IQueryable<T> queryable, PageDTO pageDto)
      {
         return queryable
            .Skip((pageDto.Page - 1) * pageDto.RegistersPerPage)
            .Take(pageDto.RegistersPerPage);
      }
   }
}