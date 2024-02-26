using FinalFilmProject.Controllers;
using FinalFilmProject.DTOS;
using FinalFilmProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsAPI.Tests.UnitTests
{
    [TestClass]
    public class GenresControllerTests : TestsBase
    {
        [TestMethod]
        public async Task GetAllGenres()
        {
            //Preparation
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigureAutoMapper();

            context.Genres.Add(new Genre() {Name = "Genre 1"});
            context.Genres.Add(new Genre() {Name = "Genre 2"});
            await context.SaveChangesAsync();

            var context2 = BuildContext(nameDB);
            //Test                                                 
            var controller = new GenresController(context2, mapper);
            var response = await controller.GetGenres();
            
            //Verification
            var genres = response.Value;
            if (genres != null) Assert.AreEqual(2, genres.Count);
        }

        [TestMethod]
        public async Task GetGenresByIdNoExist()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigureAutoMapper();

            var controller = new GenresController(context, mapper);
            var response = await controller.GetGenre(1);

            var result = response.Result as StatusCodeResult;
            if (result != null) Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task GetGenresByIdExist()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigureAutoMapper();
            
            context.Genres.Add(new Genre() {Name = "Genre 1"});
            context.Genres.Add(new Genre() {Name = "Genre 2"});
            await context.SaveChangesAsync();

            var context2 = BuildContext(nameDB);
            var controller = new GenresController(context2, mapper);

            var id = 1;
            var response = await controller.GetGenre(1);
            var result = response.Value;
            if (result != null) Assert.AreEqual((object) id, result.Id);
        }

        [TestMethod]
        public async Task CreateGenre()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigureAutoMapper();

            var newGenre = new GenreCreationDTO() {Name = "new genre"};

            var controller = new GenresController(context, mapper: mapper);

            var response = await controller.PostGenre(newGenre);
            var result = response as CreatedAtRouteResult;
            Assert.IsNotNull(result);

            var context2 = BuildContext(nameDB: nameDB);
            var amount = await context.Genres.CountAsync();
            Assert.AreEqual(1, amount);
        }

        [TestMethod]
        public async Task UpdateGenre()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigureAutoMapper();

            context.Genres.Add(new Genre() {Name = "Genre 1"});
            await context.SaveChangesAsync();

            var context2 = BuildContext(nameDB);
            var controller = new GenresController(context2, mapper);

            var genreCreationDTO = new GenreCreationDTO() {Name = "New name"};

            var id = 1;
            var response = await controller.PutGenre(id, genreCreationDTO);

            var result = response as StatusCodeResult;
            if (result != null) Assert.AreEqual(204, result.StatusCode);

            var context3 = BuildContext(nameDB: nameDB);
            var exist = await context3.Genres.AnyAsync(x => x.Name == "New name");
            Assert.IsTrue(exist);
        }

        [TestMethod]
        public async Task DeleteGenreNotExist()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigureAutoMapper();

            var controller = new GenresController(context, mapper);

            var response = await controller.DeleteGenre(1);
            var result = response as StatusCodeResult;
            if (result != null) Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteGenre()
        {
            var nameDB = Guid.NewGuid().ToString();
            var context = BuildContext(nameDB);
            var mapper = ConfigureAutoMapper();
            
            context.Genres.Add(new Genre() {Name = "Genre 1"});
            await context.SaveChangesAsync();

            var context2 = BuildContext(nameDB);
            var controller = new GenresController(context2, mapper);

            var response = await controller.DeleteGenre(1);
            var result = response as StatusCodeResult;
            Assert.AreEqual(204, result.StatusCode);

            var context3 = BuildContext(nameDB: nameDB);
            var exist = await context3.Genres.AnyAsync();
            Assert.IsFalse(exist);
        }
    }
}