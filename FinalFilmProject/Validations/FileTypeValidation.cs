using System.ComponentModel.DataAnnotations;

namespace FinalFilmProject.Validations
{
     public class FileTypeValidation : ValidationAttribute 
     {
          private readonly string[] _validTypes;

          public FileTypeValidation(string[] validTypes)
          {
               _validTypes = validTypes;
          }

          public FileTypeValidation(GroupFileType groupFileType)
          {
               if (groupFileType == GroupFileType.Image)
               {
                    _validTypes = new string[] { "image/jpg", "image/jpeg", "image/png", "image/gif", };
               } 
          }

          protected override ValidationResult IsValid(object value, ValidationContext validationContext)
          {
               if (value == null) 
                    return ValidationResult.Success; 
          
               IFormFile formFile = value as IFormFile;

               if (formFile == null)
                    return ValidationResult.Success;

               if (!_validTypes.Contains(formFile.ContentType))
                    return new ValidationResult($"The file type must be: {string.Join(", ", _validTypes)}");

               return ValidationResult.Success;
          }
     }
}