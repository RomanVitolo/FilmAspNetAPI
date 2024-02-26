using System.ComponentModel.DataAnnotations;

namespace FinalFilmProject.Validations
{
     public class FileWeightValidation : ValidationAttribute
     {
          private readonly int _maxWeightInMegaBytes;

          public FileWeightValidation(int MaxWeightInMegaBytes)
          {
               _maxWeightInMegaBytes = MaxWeightInMegaBytes;
          }

          protected override ValidationResult IsValid(object value, ValidationContext validationContext)
          {
               if (value is null) 
                    return ValidationResult.Success; 
          
               IFormFile formFile = value as IFormFile;

               if (formFile == null)
                    return ValidationResult.Success;

               if (formFile.Length > _maxWeightInMegaBytes * 1024 * 1024)
               {
                    return new ValidationResult($"The Weight File is more than {_maxWeightInMegaBytes}mb");
               }
          
               return ValidationResult.Success;       
          }
     }
}