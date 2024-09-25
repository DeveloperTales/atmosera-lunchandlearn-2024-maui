using Microsoft.Azure.Cosmos;

namespace LunchAndLearn2024API.Services;

public interface ICosmosService
{
    Task<Database> GetDatabaseAsync(string? databaseName = null);
    Task<Container> GetContainerAsync(string containerValue, string? databaseName = null);
}
