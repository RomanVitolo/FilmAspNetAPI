namespace FinalFilmProject.DTOS
{
     public class PageDTO
     {
          public int Page { get; set; } = 1;
          private int _registersPerPage = 10;
          private readonly int _maxRegistersPerPage = 50;

          public int RegistersPerPage
          {
               get => _registersPerPage;
               set => _registersPerPage = (value > _maxRegistersPerPage) ? _maxRegistersPerPage : value;
          }
     }
}