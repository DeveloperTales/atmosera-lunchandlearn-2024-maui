using Microsoft.Azure.Cosmos;
using System.Text.Json;

namespace LunchAndLearn2024API.Services;

public class CosmosService : ICosmosService
{
    private readonly CosmosClient _client;
    private readonly Dictionary<string, Container> _containers;
    private readonly string? _databaseName;
    private Database? _database;

    public CosmosService(IConfiguration configuration)
    {
        _databaseName = configuration["DATABASE_NAME"];
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var cosmosOptions = new CosmosClientOptions
        {
            Serializer = new CosmosNetSerializer(options)
        };
        _client = new CosmosClient(configuration["COSMOS_CONNECTION_STRING"], cosmosOptions);
        _containers = new Dictionary<string, Container>();
    }

    public async Task<Database> GetDatabaseAsync(string? databaseName = null)
    {
        if (_database == null)
        {
            var dbCreated = await _client.CreateDatabaseIfNotExistsAsync(string.IsNullOrWhiteSpace(databaseName) ? _databaseName : databaseName);
            _database = dbCreated.Database;
        }

        return _database;
    }

    public async Task<Container> GetContainerAsync(string containerValue, string? databaseName = null)
    {
        if (!_containers.ContainsKey(containerValue))
        {
            var dataBase = await GetDatabaseAsync(databaseName);
            var container = await dataBase.CreateContainerIfNotExistsAsync(containerValue, "/dataGroup");
            _containers[containerValue] = container;
        }

        return _containers[containerValue];
    }
}
