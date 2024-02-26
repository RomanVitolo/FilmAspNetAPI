using AutoMapper;
using FinalFilmProject;
using FinalFilmProject.Helpers;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;

namespace FilmsAPI.Tests
{
    public class TestsBase
    {
        protected ApplicationDbContext BuildContext(string nameDB)
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(nameDB).Options;

            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        protected IMapper ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(options =>
            {
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                options.AddProfile(new AutoMapperProfiles(geometryFactory));
            });
            return config.CreateMapper();
        }
    }
}