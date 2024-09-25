using System.Net.Http.Headers;
using System.Net.Http.Json;
using LunchAndLearn2024Models;

namespace LunchAndLearn2024MauiApp.Services;

public class ApiService : IApiService
{
    public readonly string _url = "";
    private readonly HttpClient _client;

    public ApiService()
    {
        _client = new HttpClient();
    }

    public async Task<Book?> AddUpdateBookAsync(Book book)
    {
        var response = await _client.PutAsJsonAsync($"{_url}/api/books", book);
        if (response.IsSuccessStatusCode){
            return await response.Content.ReadFromJsonAsync<Book>();
        }
        
        return null;
    }

    public async Task<Book?> UploadBookImageAsync(string bookId, string filePath)
    {
        using var multipartFormContent = new MultipartFormDataContent();
        //Load the file and set the file's Content-Type header
        var fileStreamContent = new StreamContent(File.OpenRead(filePath));
        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

        //Add the file
        multipartFormContent.Add(fileStreamContent, name: "files", fileName: "house.png");

        //Send it
        var response = await _client.PostAsync($"{_url}/api/books/files/{bookId}", multipartFormContent);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Book>();
        }

        return null;
    }

    public async Task<ICollection<Book>> GetBooksByStatusAsync(CurrentStatus status)
    {
        var response = await _client.GetAsync($"{_url}/api/books?status={status}");
        if (response.IsSuccessStatusCode){
            var books = await response.Content.ReadFromJsonAsync<ICollection<Book>>();
            return books ?? [];
        }
        
        return [];
    }

    public async Task<ICollection<Quote>> GetQuotesAsync()
    {
        var response = await _client.GetAsync($"{_url}/api/quotes?amount={5}");
        if (response.IsSuccessStatusCode){
            var quotes = await response.Content.ReadFromJsonAsync<ICollection<Quote>>();
            return quotes ?? [];
        }
        
        return [];
    }
}

