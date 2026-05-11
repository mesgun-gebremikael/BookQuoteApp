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
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(_bookService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookService.GetById(id);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook(BookDto dto)
        {
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

            var createdBook = _bookService.Add(book);

            return Ok(createdBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookDto dto)
        {
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

            var updated = _bookService.Update(id, updatedBook);

            if (!updated)
            {
                return NotFound("Book not found.");
            }

            return Ok(_bookService.GetById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var deleted = _bookService.Delete(id);

            if (!deleted)
            {
                return NotFound("Book not found.");
            }

            return Ok("Book deleted.");
        }
    }
}