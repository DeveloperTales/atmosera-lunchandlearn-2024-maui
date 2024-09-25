using LunchAndLearn2024Models;

namespace LunchAndLearn2024MauiApp.Services;

public interface IApiService
{
    Task<Book?> AddUpdateBookAsync(Book book);
    Task<Book?> UploadBookImageAsync(string bookId, string filePath);
    Task<ICollection<Book>> GetBooksByStatusAsync(CurrentStatus status);
    Task<ICollection<Quote>> GetQuotesAsync();
}

