using LunchAndLearn2024Models;

namespace LunchAndLearn2024API.Services;

public interface IQuoteService
{
    Task<Quote> AddUpdateQuoteAsync(Quote quote);
    Task RemoveQuoteAsync(string id);
    Task<Quote?> GetQuoteAsync(string quoteId);
    Task<IList<Quote>> GetQuotesAsync();
}
