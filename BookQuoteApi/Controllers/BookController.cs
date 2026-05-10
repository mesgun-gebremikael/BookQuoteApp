using BookQuoteApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookQuoteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private static List<Book> books = new List<Book>();

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(books);
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                return BadRequest("Title is required.");
            }

            book.Id = books.Count + 1;

            books.Add(book);

            return Ok(book);
        }
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            if (string.IsNullOrWhiteSpace(updatedBook.Title))
            {
                return BadRequest("Title is required.");
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.PublishedDate = updatedBook.PublishedDate;

            return Ok(book);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            books.Remove(book);

            return Ok("Book deleted.");
        }
    }
}