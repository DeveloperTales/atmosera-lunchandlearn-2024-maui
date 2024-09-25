using LunchAndLearn2024Models;

namespace LunchAndLearn2024API.Services;

public interface IBookService
{
    Task<Book> AddUpdateBookAsync(Book book);
    Task<Book?> GetBookAsync(string id);
    Task RemoveBookAsync(string id);
    Task<ICollection<Book>> GetBooksByStatusAsync(CurrentStatus status);
    Task<string> AddUpdateMediaItemAsync(string fileName, Stream file);
    Task DeleteMediaItemAsync(string fileName);
}
