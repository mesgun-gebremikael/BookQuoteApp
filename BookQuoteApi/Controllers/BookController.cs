using BookQuoteApi.Dtos;
using BookQuoteApi.Models;
using BookQuoteApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookQuoteApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : null;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            return Ok(_bookService.GetAllForUser(userId.Value));
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var book = _bookService.GetById(id, userId.Value);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook(BookDto dto)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                return BadRequest("Title is required.");
            }

            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                PublishedDate = dto.PublishedDate
            };

            var createdBook = _bookService.Add(book, userId.Value);

            return Ok(createdBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookDto dto)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                return BadRequest("Title is required.");
            }

            var updatedBook = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                PublishedDate = dto.PublishedDate
            };

            var updated = _bookService.Update(id, userId.Value, updatedBook);

            if (!updated)
            {
                return NotFound("Book not found.");
            }

            return Ok(_bookService.GetById(id, userId.Value));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var deleted = _bookService.Delete(id, userId.Value);

            if (!deleted)
            {
                return NotFound("Book not found.");
            }

            return Ok("Book deleted.");
        }
    }
}