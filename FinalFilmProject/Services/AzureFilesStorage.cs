using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace FinalFilmProject.Services
{
    public class AzureFilesStorage : IFilesStorage
    {
        private readonly string connectionString;
        
        public AzureFilesStorage(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("AzureStorage");
        }
    
        public async Task<string> SaveFile(string container, IFormFile file)
        {
            var client = new BlobContainerClient(connectionString, container);
            await client.CreateIfNotExistsAsync();
            await client.SetAccessPolicyAsync(PublicAccessType.Blob);

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var blob = client.GetBlobClient(fileName);

            //var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHeader = new BlobHttpHeaders();
            blobHttpHeader.ContentType = file.ContentType;     

            await blob.UploadAsync(file.OpenReadStream(), blobHttpHeader);
            return blob.Uri.ToString();  
        }

        public async Task<string> EditFile(IFormFile file, string container, string route)
        {
            await DeleteFile(route, container);
            return await SaveFile(container, file);
        }

        public async Task DeleteFile(string route, string container)
        {
            if (string.IsNullOrEmpty(route))   
                return;
            
            var client = new BlobContainerClient(connectionString, container);
            await client.CreateIfNotExistsAsync();

            var file = Path.GetFileName(route);
            var blob = client.GetBlobClient(file);
            await blob.DeleteIfExistsAsync();
        }
    }
}