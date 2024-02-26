using System.ComponentModel.DataAnnotations;
using FinalFilmProject.Helpers;
using FinalFilmProject.Validations;
using Microsoft.AspNetCore.Mvc;

namespace FinalFilmProject.DTOS
{
    public class FilmCreationDTO : FilmPatchDTO
    {    
        [FileWeightValidation(MaxWeightInMegaBytes: 4)]
        [FileTypeValidation(GroupFileType.Image)]           
        public IFormFile Poster { get; set; }
        
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenresIds { get; set; }
        
        [ModelBinder(BinderType = typeof(TypeBinder<List<FilmActorCreationDTO>>))]
        public List<FilmActorCreationDTO> Actors { get; set; }
    }
}