using LunchAndLearn2024API.Services;
using LunchAndLearn2024Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LunchAndLearn2024API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService) : ControllerBase
{
    private readonly IBookService _bookService = bookService;

    // GET: api/<BooksController>
    [HttpGet]
    public async Task<IEnumerable<Book>> Get([FromQuery] CurrentStatus? status)
    {
        status ??= CurrentStatus.started;
        return await _bookService.GetBooksByStatusAsync(status.Value);
    }

    // GET api/<BooksController>/5
    [HttpGet("{bookId}")]
    public async Task<Book?> Get(string bookId)
    {
        if (string.IsNullOrWhiteSpace(bookId))
        {
            throw new ArgumentNullException(nameof(bookId));
        }

        return await _bookService.GetBookAsync(bookId);
    }

    // POST api/<BooksController>
    [HttpPut]
    public async Task<Book> Post([FromBody] Book book)
    {
        ArgumentNullException.ThrowIfNull(book);

        return await _bookService.AddUpdateBookAsync(book);
    }

    // POST api/<BooksController>
    [HttpPost("files/{bookId}")]
    public async Task<Book> Post(string bookId, List<IFormFile> files)
    {
        ArgumentNullException.ThrowIfNull(bookId);

        if (files == null || files.Count == 0)
        {
            throw new BadHttpRequestException("Please provide an image.");
        }

        var book = await _bookService.GetBookAsync(bookId) ?? throw new BadHttpRequestException("Book does not exist.");
        var file = files.FirstOrDefault();
        var content = file?.ContentType;
        if (content == null || !content.Contains("image", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new BadHttpRequestException("Only images allowed.");
        }
        
        book.ImageUrl = await _bookService.AddUpdateMediaItemAsync($"{bookId}.jpg", file!.OpenReadStream()); 
        return await _bookService.AddUpdateBookAsync(book);
    }

    // DELETE api/<BooksController>/{id}
    [HttpDelete("{bookId}")]
    public async Task Delete(string bookId)
    {
        if (string.IsNullOrWhiteSpace(bookId))
        {
            throw new ArgumentNullException(nameof(bookId));
        }

       await _bookService.RemoveBookAsync(bookId);
    }
}
