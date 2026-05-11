using BookQuoteApi.Models;
using BookQuoteApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
            var books = _bookService.GetAll();

            return Ok(books);
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
        public IActionResult AddBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                return BadRequest("Title is required.");
            }

            var createdBook = _bookService.Add(book);

            return Ok(createdBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            if (string.IsNullOrWhiteSpace(updatedBook.Title))
            {
                return BadRequest("Title is required.");
            }

            var updated = _bookService.Update(id, updatedBook);

            if (!updated)
            {
                return NotFound("Book not found.");
            }

            return Ok(updatedBook);
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