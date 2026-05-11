using BookQuoteApi.Models;
using BookQuoteApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookQuoteApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly QuoteService _quoteService;

        public QuoteController(QuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet]
        public IActionResult GetAllQuotes()
        {
            var quotes = _quoteService.GetAll();

            return Ok(quotes);
        }

        [HttpGet("{id}")]
        public IActionResult GetQuoteById(int id)
        {
            var quote = _quoteService.GetById(id);

            if (quote == null)
            {
                return NotFound("Quote not found.");
            }

            return Ok(quote);
        }

        [HttpPost]
        public IActionResult AddQuote(Quote quote)
        {
            if (string.IsNullOrWhiteSpace(quote.Text))
            {
                return BadRequest("Quote text is required.");
            }

            var createdQuote = _quoteService.Add(quote);

            return Ok(createdQuote);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateQuote(int id, Quote updatedQuote)
        {
            if (string.IsNullOrWhiteSpace(updatedQuote.Text))
            {
                return BadRequest("Quote text is required.");
            }

            var updated = _quoteService.Update(id, updatedQuote);

            if (!updated)
            {
                return NotFound("Quote not found.");
            }

            return Ok(updatedQuote);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuote(int id)
        {
            var deleted = _quoteService.Delete(id);

            if (!deleted)
            {
                return NotFound("Quote not found.");
            }

            return Ok("Quote deleted.");
        }
    }
}
