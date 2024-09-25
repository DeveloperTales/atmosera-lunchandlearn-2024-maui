using LunchAndLearn2024Models;
using Microsoft.Azure.Cosmos;

namespace LunchAndLearn2024API.Services;

public class QuoteService : IQuoteService
{
    private readonly ICosmosService _cosmosService;
    private const string _partitionKey = "Quote";
    private const string _containerName = "LunchAndLearn2024";

    public QuoteService(ICosmosService cosmosDbService)
    {
        _cosmosService = cosmosDbService;
    }

    public async Task<Quote> AddUpdateQuoteAsync(Quote quote)
    {
        var container = await _cosmosService.GetContainerAsync(_containerName);

        if (!string.IsNullOrWhiteSpace(quote.Id))
        {
            var oldQuote = await GetQuoteAsync(quote.Id);

            if (oldQuote == null)
            {
                throw new ArgumentNullException(nameof(oldQuote));
            }

            var updatedProperty = oldQuote with
            {
                Author = quote.Author,
                Description = quote.Description ?? oldQuote.Description,
                Title = quote.Title ?? oldQuote.Title,
            };
            await container.UpsertItemAsync(
                updatedProperty,
                new PartitionKey(_partitionKey),
                requestOptions: new ItemRequestOptions() { EnableContentResponseOnWrite = false });
            return updatedProperty;
        }

        // Create Item
        var dbItem = quote with
        {
            Id = Guid.NewGuid().ToString()
        };
        await container.CreateItemAsync(
            dbItem,
            new PartitionKey(_partitionKey),
            requestOptions: new ItemRequestOptions() { EnableContentResponseOnWrite = false });
        return dbItem;
    }

    public async Task<Quote?> GetQuoteAsync(string id)
    {
        var container = await _cosmosService.GetContainerAsync(_containerName);
        try
        {
            ItemResponse<Quote> response = await container.ReadItemAsync<Quote>(id, new PartitionKey(_partitionKey));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IList<Quote>> GetQuotesAsync()
    {
        var results = new List<Quote>();
        var container = await _cosmosService.GetContainerAsync(_containerName);
        using (var query = container.GetItemQueryIterator<Quote>(
            queryDefinition: null,
            requestOptions: new QueryRequestOptions()
            {
                PartitionKey = new PartitionKey(_partitionKey)
            }))
        {
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }
        }

        return results;
    }

    public async Task RemoveQuoteAsync(string id)
    {
        var container = await _cosmosService.GetContainerAsync(_containerName);
        await container.DeleteItemAsync<Quote>(
            id,
            new PartitionKey(_partitionKey),
            requestOptions: new ItemRequestOptions() { EnableContentResponseOnWrite = false }
            );
    }
}
