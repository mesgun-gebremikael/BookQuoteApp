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

        [HttpGet("{id}")]
        public IActionResult GetQuoteById(int id)
        {
            var quote = quotes.FirstOrDefault(q => q.Id == id);

            if (quote == null)
            {
                return NotFound("Quote not found.");
            }

            return Ok(quote);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateQuote(int id, Quote updatedQuote)
        {
            var quote = quotes.FirstOrDefault(q => q.Id == id);

            if (quote == null)
            {
                return NotFound("Quote not found.");
            }

            if (string.IsNullOrWhiteSpace(updatedQuote.Text))
            {
                return BadRequest("Quote text is required.");
            }

            quote.Text = updatedQuote.Text;
            quote.Author = updatedQuote.Author;

            return Ok(quote);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuote(int id)
        {
            var quote = quotes.FirstOrDefault(q => q.Id == id);

            if (quote == null)
            {
                return NotFound("Quote not found.");
            }

            quotes.Remove(quote);

            return Ok("Quote deleted.");
        }
    }
}
