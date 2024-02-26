using AutoMapper;
using FinalFilmProject.DTOS;
using FinalFilmProject.Entities;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace FinalFilmProject.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>();

            CreateMap<Review, ReviewDTO>()
                .ForMember(x => x.UserName,
                    x => x
                        .MapFrom(y => y.User.UserName));

            CreateMap<ReviewDTO, Review>();
            CreateMap<ReviewCreationDTO, Review>();

            CreateMap<IdentityUser, UserDTO>();

            CreateMap<CinemaRoom, CinemaRoomDTO>()
                .ForMember(x => x.Latitude, 
                    x =>
                        x.MapFrom(y => y.Location.Y))
                .ForMember(x => x.Longitude, x => 
                    x.MapFrom(y => y.Location.X));        
            
            CreateMap<CinemaRoomDTO, CinemaRoom>()
                .ForMember(x => x.Location, x => 
                    x.MapFrom(y => geometryFactory.CreatePoint(new Coordinate(y.Longitude, y.Latitude))));    
            
            CreateMap<CinemaRoomCreationDTO, CinemaRoom>()
                .ForMember(x => x.Location, x => 
                    x.MapFrom(y => geometryFactory.CreatePoint(new Coordinate(y.Longitude, y.Latitude))));;

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreationDTO, Actor>()
                .ForMember(x => x.PhotoUrl,
                    options => options.Ignore());    
            CreateMap<ActorPatchDTO, Actor>().ReverseMap();
            
            CreateMap<Film, FilmDTO>().ReverseMap();
            CreateMap<FilmCreationDTO, Film>()
                .ForMember(x => x.Poster,
                    options => options.Ignore())
                .ForMember(x => x.FilmsGenres,
                    options => options.MapFrom(FilmsGenresMap))
                .ForMember(x => x.FilmsActors,
                    options => options.MapFrom(FilmsActorsMap));
            CreateMap<FilmPatchDTO, Film>().ReverseMap();

            CreateMap<Film, FilmDetailsDTO>()
                .ForMember(x => x.Genres, options => options.MapFrom(FilmsGenresMap))
                .ForMember(x => x.Actors, options => options.MapFrom(FilmActorDetails));
        }

        private List<FilmActorDetailsDTO> FilmActorDetails(Film film, FilmDetailsDTO filmDetailsDto)
        {
            var result = new List<FilmActorDetailsDTO>();
            if (film.FilmsActors == null) return result;

            foreach (var filmActor in film.FilmsActors)
            {
                result.Add(new FilmActorDetailsDTO()
                {
                    ActorId = filmActor.ActorId, 
                    MainCharacter = filmActor.MainCharacter,
                    CharacterName = filmActor.Actor.Name
                });   
            }

            return result;
        }

        private List<GenreDTO> FilmsGenresMap(Film film, FilmDetailsDTO filmDetailsDto)
        {
            var result = new List<GenreDTO>();
            if (film.FilmsGenres == null) return result;

            foreach (var filmGenre in film.FilmsGenres)
            {
                 result.Add(new GenreDTO() {Id = filmGenre.GenreId, Name = filmGenre.Genre.Name});
            }

            return result;
        }

        private List<FilmsGenres> FilmsGenresMap(FilmCreationDTO filmCreationDto, Film film)
        {
            var result = new List<FilmsGenres>();
            if (filmCreationDto.GenresIds == null) return result;

            foreach (var id in filmCreationDto.GenresIds)
            {
               result.Add(new FilmsGenres() { GenreId = id}); 
            }

            return result;
        }

        private List<FilmsActors> FilmsActorsMap(FilmCreationDTO filmCreationDto, Film film)
        {
            var result = new List<FilmsActors>();
            if (filmCreationDto.Actors == null) return result;

            foreach (var actor in filmCreationDto.Actors)
            {
                result.Add(new FilmsActors() { ActorId = actor.ActorId, MainCharacter = actor.MainCharacter}); 
            }

            return result;
        }
    }
}