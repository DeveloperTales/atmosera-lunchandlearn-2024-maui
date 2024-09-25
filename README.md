# lunchandlearn-2024-maui
We explore how to create a simple app on Maui. The app simply tracks books you a currently reading/completed or plan to read. Created by Juandré `Joe` Swart.

## Technologies used:
- [.NET 8.x](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui?view=net-maui-8.0)
- [Visual Studio](https://visualstudio.microsoft.com/vs/)
- [C# WebAPI](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio)
- [Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/)
- [Azure Storage](https://learn.microsoft.com/en-us/azure/storage/common/storage-introduction)

## Major Libraries
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)

## To Run Web Api with Visual Studio
- In appsettings.json please add the connection strings for `COSMOS_CONNECTION_STRING` and `STORAGE_CONNECTION_STRING`. Follow the links above to create cosmosdb and storage account.
- `select` webapi application
- `rebuild`
- `run`

## To Run Maui Application with Visual Studio
- Add a webapi `url` that will be used to retreive/update the data in Services/ApiService.cs. Since a local device will not work with locahost a [api service](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net80&pivots=development-environment-vs) in azure will be needed. 
- `select` maui application
- `rebuild`
- `select` desired platform (Android/iOS)
- `run`

## Implementations

### General
Using CommunityToolkit.Mvvm for our Model/View/ViewModel pattern, splitting forms/views and services code.
Calling a webapi that stores the data for the application. The api uses cosmosdb for the data and storage account for the images.

### Books

Tabs for books that allow creating/updating book items.
Simple collection view displays the books.
Data stored in cosmosdb in azure.

### Quotes

List of random quotes, pull list to receive new quotes.