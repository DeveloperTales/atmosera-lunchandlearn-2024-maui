using LunchAndLearn2024Models;
using Microsoft.Azure.Cosmos;

namespace LunchAndLearn2024API.Services;

public class BookService : IBookService
{
    private readonly ICosmosService _cosmosService;
    private readonly IStorageService _storageService;
    private const string _partitionKey = "Book";
    private const string _containerName = "LunchAndLearn2024";
    private const string _storageContainerName = "lunchandlearn2024";

    public BookService(ICosmosService cosmosService, IStorageService storageService)
    {
        _cosmosService = cosmosService;
        _storageService = storageService;
    }

    public async Task<Book> AddUpdateBookAsync(Book book)
    {
        var container = await _cosmosService.GetContainerAsync(_containerName);

        if (!string.IsNullOrWhiteSpace(book.Id))
        {
            var updatedProperty = await GetBookAsync(book.Id);

            if (updatedProperty == null)
            {
                throw new ArgumentNullException(nameof(updatedProperty));
            }

            updatedProperty.Author = book.Author ?? updatedProperty.Author;
            updatedProperty.Status = book.Status;
            updatedProperty.Description = book.Description ?? updatedProperty.Description;
            updatedProperty.Notes = book.Notes ?? updatedProperty.Notes;
            updatedProperty.Title = book.Title ?? updatedProperty.Title;
            updatedProperty.ImageUrl = book.ImageUrl ?? updatedProperty.ImageUrl;
            await container.UpsertItemAsync(
                updatedProperty,
                new PartitionKey(_partitionKey),
                requestOptions: new ItemRequestOptions() { EnableContentResponseOnWrite = false });
            return updatedProperty;
        }

        // Create Item
        book.Id = Guid.NewGuid().ToString();
        await container.CreateItemAsync(
            book,
            new PartitionKey(_partitionKey),
            requestOptions: new ItemRequestOptions() { EnableContentResponseOnWrite = false });
        return book;
    }

    public async Task<string> AddUpdateMediaItemAsync(string fileName, Stream file)
    {
        return await _storageService.UploadBlobAsync(_storageContainerName, $"book/{fileName}", file);
    }

    public virtual async Task DeleteMediaItemAsync(string fileName)
    {
        try
        {
            await _storageService.DeleteBlobAsync(_storageContainerName, $"book/{fileName}");
        }
        catch (Exception) { }
    }

    public async Task<Book?> GetBookAsync(string id)
    {
        var container = await _cosmosService.GetContainerAsync(_containerName);
        ItemResponse<Book> response = await container.ReadItemAsync<Book>(id, new PartitionKey(_partitionKey));
        return response.Resource;
    }

    public async Task<ICollection<Book>> GetBooksByStatusAsync(CurrentStatus status)
    {
        var results = new List<Book>();
        var container = await _cosmosService.GetContainerAsync(_containerName);
        using (var query = container.GetItemQueryIterator<Book>(
            $"select * from Book b where b.status=\"{status}\" ",
            requestOptions: new QueryRequestOptions()
            {
                PartitionKey = new PartitionKey(_partitionKey)
            }))
        {
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange([.. response]);
            }
        }

        return results;
    }

    public async Task RemoveBookAsync(string id)
    {
        var container = await _cosmosService.GetContainerAsync(_containerName);
        await container.DeleteItemAsync<Book>(
            id,
            new PartitionKey(_partitionKey),
            requestOptions: new ItemRequestOptions() { EnableContentResponseOnWrite = false }
            );
        await DeleteMediaItemAsync(id);
    }
}
