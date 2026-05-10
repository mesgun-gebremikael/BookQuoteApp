using BookQuoteApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookQuoteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private static List<Quote> quotes = new List<Quote>();

        [HttpGet]
        public IActionResult GetAllQuotes()
        {
            return Ok(quotes);
        }

        [HttpPost]
        public IActionResult AddQuote(Quote quote)
        {
            if (string.IsNullOrWhiteSpace(quote.Text))
            {
                return BadRequest("Quote text is required.");
            }

            quote.Id = quotes.Count + 1;

            quotes.Add(quote);

            return Ok(quote);
        }
    }
}
