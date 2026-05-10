using BookQuoteApi.Models;

namespace BookQuoteApi.Services
{
    public class BookService
    {
        private readonly List<Book> _books = new();

        public List<Book> GetAll()
        {
            return _books;
        }

        public Book? GetById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public Book Add(Book book)
        {
            book.Id = _books.Count + 1;

            _books.Add(book);

            return book;
        }

        public bool Delete(int id)
        {
            var book = GetById(id);

            if (book == null)
            {
                return false;
            }

            _books.Remove(book);

            return true;
        }

        public bool Update(int id, Book updatedBook)
        {
            var book = GetById(id);

            if (book == null)
            {
                return false;
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.PublishedDate = updatedBook.PublishedDate;

            return true;
        }
    }
}
