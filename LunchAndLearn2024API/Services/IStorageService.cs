using Azure.Storage.Blobs;

namespace LunchAndLearn2024API.Services;

public interface IStorageService
{
    Task<BlobContainerClient> GetContainerAsync(string containerName);
    Task<string> UploadBlobAsync(string containerName, string fileName, Stream file);
    Task DeleteBlobAsync(string containerName, string fileName);
    Task<List<string>> GetBlobNamesAsync(string containerName, string pathName);
}
