using System.Security.Claims;
using FinalFilmProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace FinalFilmProject
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmsActors> FilmActors { get; set; }
        public DbSet<FilmsGenres> FilmGenres { get; set; }
        public DbSet<CinemaRoom> CinemaRooms { get; set; }
        public DbSet<FilmsCinemaRoom> FilmsCinemaRooms { get; set; }
        public DbSet<Review> Reviews { get; set; }
    
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        
        }         

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmsActors>()
                .HasKey(x => new {x.ActorId, x.FilmId});

            modelBuilder.Entity<FilmsGenres>()
                .HasKey(x => new {x.GenreId, x.FilmId});

            modelBuilder.Entity<FilmsCinemaRoom>()
                .HasKey(x => new {x.FilmId, x.CinemaRoomId});  
            
            SeedData(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        } 
        
         private void SeedData(ModelBuilder modelBuilder)
         {
             var adminRolId = "b8ac0e97-96bd-489a-8716-ee1efe560001";
             var adminUserId = "4c7ee016-65bb-4455-92a0-d63ae18e5bf9";

             var adminRol = new IdentityRole()
             {
                 Id = adminRolId,
                 Name = "Admin",
                 NormalizedName = "Admin"
             };

             var passwordHasher = new PasswordHasher<IdentityUser>();

             var userName = "roman@7r1ck.com";

             var userAdmin = new IdentityUser()
             {
               Id = adminUserId,
               UserName = userName,
               NormalizedUserName = userName,
               Email = userName,
               NormalizedEmail = userName,
               PasswordHash = passwordHasher.HashPassword(null, "Aa123456!")
             };

             //modelBuilder.Entity<IdentityUser>()
             //    .HasData(userAdmin);

             //modelBuilder.Entity<IdentityRole>()
             //    .HasData(adminRol);

             //modelBuilder.Entity<IdentityUserClaim<string>>()
             //    .HasData(new IdentityUserClaim<string>()
             //    {
             //        Id = 1,
             //        ClaimType = ClaimTypes.Role,
             //        UserId = adminUserId,
             //        ClaimValue = "Admin"
             //    });
            
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            
            modelBuilder.Entity<CinemaRoom>()
                .HasData(new List<CinemaRoom>
                {   
                    new CinemaRoom{Id = 4, Name = "Sambil",
                        Location = geometryFactory.CreatePoint(new Coordinate(-69.9118804, 18.4826214))},
                    new CinemaRoom{Id = 5, Name = "Megacentro", 
                        Location = geometryFactory.CreatePoint(new Coordinate(-69.856427, 18.506934))},
                    new CinemaRoom{Id = 6, Name = "Village East Cinema", 
                        Location = geometryFactory.CreatePoint(new Coordinate(-73.986227, 40.730898))}
                });     
            
            var adventure = new Genre() { Id = 4, Name = "Aventura" };
            var animation = new Genre() { Id = 5, Name = "Animación" };
            var suspense = new Genre() { Id = 6, Name = "Suspenso" };
            var romance = new Genre() { Id = 7, Name = "Romance" };

            modelBuilder.Entity<Genre>()
                .HasData(new List<Genre>
                {
                    adventure, animation, suspense, romance
                });

            var jimCarrey = new Actor() { Id = 5, Name = "Jim Carrey", 
                BirthDate = new DateTime(1962, 01, 17) };
            var robertDowney = new Actor() { Id = 6, Name = "Robert Downey Jr.", 
                BirthDate = new DateTime(1965, 4, 4) };
            var chrisEvans = new Actor() { Id = 7, Name = "Chris Evans", 
                BirthDate = new DateTime(1981, 06, 13) };

            modelBuilder.Entity<Actor>()
                .HasData(new List<Actor>
                {
                    jimCarrey, robertDowney, chrisEvans
                });

            var endgame = new Film()
            {
                Id = 2,
                Title = "Avengers: Endgame",
                InCinemas = true,
                ReleaseDate = new DateTime(2019, 04, 26)
            };

            var iw = new Film()
            {
                Id = 3,
                Title = "Avengers: Infinity Wars",
                InCinemas = false,
                ReleaseDate = new DateTime(2019, 04, 26)
            };

            var sonic = new Film()
            {
                Id = 4,
                Title = "Sonic the Hedgehog",
                InCinemas = false,
                ReleaseDate = new DateTime(2020, 02, 28)
            };
            var emma = new Film()
            {
                Id = 5,
                Title = "Emma",
                InCinemas = false,
                ReleaseDate = new DateTime(2020, 02, 21)
            };
            var wonderwoman = new Film()
            {
                Id = 6,
                Title = "Wonder Woman 1984",
                InCinemas = false,
                ReleaseDate = new DateTime(2020, 08, 14)
            };

            modelBuilder.Entity<Film>()
                .HasData(new List<Film>
                {
                    endgame, iw, sonic, emma, wonderwoman
                });

            modelBuilder.Entity<FilmsGenres>().HasData(
                new List<FilmsGenres>()
                {
                    new FilmsGenres(){FilmId = endgame.Id, GenreId = suspense.Id},
                    new FilmsGenres(){FilmId = endgame.Id, GenreId = adventure.Id},
                    new FilmsGenres(){FilmId = iw.Id, GenreId = suspense.Id},
                    new FilmsGenres(){FilmId = iw.Id, GenreId = adventure.Id},
                    new FilmsGenres(){FilmId = sonic.Id, GenreId = adventure.Id},
                    new FilmsGenres(){FilmId = emma.Id, GenreId = suspense.Id},
                    new FilmsGenres(){FilmId = emma.Id, GenreId = romance.Id},
                    new FilmsGenres(){FilmId = wonderwoman.Id, GenreId = suspense.Id},
                    new FilmsGenres(){FilmId = wonderwoman.Id, GenreId = adventure.Id},
                });

            modelBuilder.Entity<FilmsActors>().HasData(
                new List<FilmsActors>()
                {
                    new FilmsActors(){FilmId = endgame.Id, ActorId = robertDowney.Id, MainCharacter = "Tony Stark", Order = 1},
                    new FilmsActors(){FilmId = endgame.Id, ActorId = chrisEvans.Id, MainCharacter = "Steve Rogers", Order = 2},
                    new FilmsActors(){FilmId = iw.Id, ActorId = robertDowney.Id, MainCharacter = "Tony Stark", Order = 1},
                    new FilmsActors(){FilmId = iw.Id, ActorId = chrisEvans.Id, MainCharacter = "Steve Rogers", Order = 2},
                    new FilmsActors(){FilmId = sonic.Id, ActorId = jimCarrey.Id, MainCharacter = "Dr. Ivo Robotnik", Order = 1}
                });
        }
    }
}