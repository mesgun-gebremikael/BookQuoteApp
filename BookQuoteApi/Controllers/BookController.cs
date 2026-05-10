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
            books.Add(book);

            return Ok(book);
        }
    }
}