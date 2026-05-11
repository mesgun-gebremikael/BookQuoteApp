using BookQuoteApi.Dtos;
using BookQuoteApi.Models;
using BookQuoteApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(_quoteService.GetAll());
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
        public IActionResult AddQuote(QuoteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Text))
            {
                return BadRequest("Quote text is required.");
            }

            var quote = new Quote
            {
                Text = dto.Text,
                Author = dto.Author
            };

            var createdQuote = _quoteService.Add(quote);

            return Ok(createdQuote);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateQuote(int id, QuoteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Text))
            {
                return BadRequest("Quote text is required.");
            }

            var updatedQuote = new Quote
            {
                Text = dto.Text,
                Author = dto.Author
            };

            var updated = _quoteService.Update(id, updatedQuote);

            if (!updated)
            {
                return NotFound("Quote not found.");
            }

            return Ok(_quoteService.GetById(id));
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
