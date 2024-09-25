using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;

namespace LunchAndLearn2024API.Services;

public class StorageService : IStorageService
{
    private readonly BlobServiceClient _client;

    public StorageService(IConfiguration configuration)
    {
        _client = new BlobServiceClient(configuration["STORAGE_CONNECTION_STRING"]);
    }

    public async Task<BlobContainerClient> GetContainerAsync(string containerName)
    {
        var container = _client.GetBlobContainerClient(containerName);

        await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

        return container;
    }

    public async Task<string> UploadBlobAsync(string containerName, string fileName, Stream file)
    {
        var container = await GetContainerAsync(containerName);
        var blobClient = container.GetBlobClient(fileName);
        await blobClient.UploadAsync(file, overwrite: true);
        return blobClient.Uri.AbsoluteUri;
    }

    public async Task DeleteBlobAsync(string containerName, string fileName)
    {
        var container = await GetContainerAsync(containerName);
        await container.DeleteBlobIfExistsAsync(fileName);
    }

    public async Task<List<string>> GetBlobNamesAsync(string containerName, string pathName)
    {
        var blobs = new List<string>();

        var container = await GetContainerAsync(containerName);
        var resultSegment = container.GetBlobsAsync(prefix: pathName).AsPages(default);

        await foreach (Azure.Page<BlobItem> blobPage in resultSegment)
        {
            foreach (BlobItem blobItem in blobPage.Values)
            {
                blobs.Add(blobItem.Name);
            }
        }

        return blobs;
    }
}
