using BookQuoteApi.Models;

namespace BookQuoteApi.Services
{
    public class BookService
    {
        private readonly List<Book> _books = new();

        public List<Book> GetAllForUser(int ownerId)
        {
            return _books.Where(b => b.OwnerId == ownerId).ToList();
        }

        public Book? GetById(int id, int ownerId)
        {
            return _books.FirstOrDefault(b => b.Id == id && b.OwnerId == ownerId);
        }

        public Book Add(Book book, int ownerId)
        {
            book.Id = _books.Count + 1;
            book.OwnerId = ownerId;

            _books.Add(book);

            return book;
        }

        public bool Delete(int id, int ownerId)
        {
            var book = GetById(id, ownerId);

            if (book == null)
            {
                return false;
            }

            _books.Remove(book);

            return true;
        }

        public bool Update(int id, int ownerId, Book updatedBook)
        {
            var book = GetById(id, ownerId);

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
