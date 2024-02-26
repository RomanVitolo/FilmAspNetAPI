using FinalFilmProject.Validations;

namespace FinalFilmProject.DTOS
{
    public class ActorCreationDTO : ActorPatchDTO
    {     
        [FileWeightValidation(MaxWeightInMegaBytes: 4)]
        [FileTypeValidation(groupFileType: GroupFileType.Image)]
        public IFormFile PhotoUrl { get; set; }
    }
}