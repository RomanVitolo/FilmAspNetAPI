namespace FinalFilmProject.Services
{
    public interface IFilesStorage
    {
        Task<string> SaveFile(string container, IFormFile file);
        Task<string> EditFile(IFormFile file, string container, string route);
        Task DeleteFile(string route, string container);
    }
}