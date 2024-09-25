using LunchAndLearn2024API.Services;
using LunchAndLearn2024Models;
using Microsoft.AspNetCore.Mvc;

namespace LunchAndLearn2024API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class QuotesController(IQuoteService quoteService) : ControllerBase
{
    private readonly IQuoteService _quoteService = quoteService;

    // GET: api/<QuotesController>
    [HttpGet]
    public async Task<IEnumerable<Quote>> Get([FromQuery]int? amount)
    {
        var randomQuotes = new List<Quote>();
        var quotes = await _quoteService.GetQuotesAsync();
        if (amount == null || amount >= quotes.Count)
        {
            return quotes;
        }

        var indexes = new HashSet<int>();
        var random = new Random();        
        while (randomQuotes.Count < amount)
        {
            var randomIndex = random.Next(quotes.Count);
            if (indexes.Add(randomIndex))
            {
                randomQuotes.Add(quotes[randomIndex]);
            }
        }

        return randomQuotes;
    }

    // GET api/<QuotesController>/{id}
    [HttpGet("{quoteId}")]
    public async Task<Quote?> Get(string quoteId)
    {
        if (string.IsNullOrWhiteSpace(quoteId))
        {
            throw new ArgumentNullException(nameof(quoteId));
        }

        return await _quoteService.GetQuoteAsync(quoteId);
    }

    // POST api/<QuotesController>
    [HttpPut]
    public async Task<Quote> Put([FromBody] Quote quote)
    {
        if (quote == null)
        {
            throw new ArgumentNullException(nameof(quote));
        }

        return await _quoteService.AddUpdateQuoteAsync(quote);
    }

    // DELETE api/<QuotesController>/{id}
    [HttpDelete("{quoteId}")]
    public async Task Delete(string quoteId)
    {
        if (string.IsNullOrWhiteSpace(quoteId))
        {
            throw new ArgumentNullException(nameof(quoteId));
        }

        await _quoteService.RemoveQuoteAsync(quoteId);
    }
}
