using BookQuoteApi.Models;

namespace BookQuoteApi.Services
{
    public class QuoteService
    {
        private readonly List<Quote> _quotes = new();

        public List<Quote> GetAll()
        {
            return _quotes;
        }

        public Quote? GetById(int id)
        {
            return _quotes.FirstOrDefault(q => q.Id == id);
        }

        public Quote Add(Quote quote)
        {
            quote.Id = _quotes.Count + 1;

            _quotes.Add(quote);

            return quote;
        }

        public bool Update(int id, Quote updatedQuote)
        {
            var quote = GetById(id);

            if (quote == null)
            {
                return false;
            }

            quote.Text = updatedQuote.Text;
            quote.Author = updatedQuote.Author;

            return true;
        }

        public bool Delete(int id)
        {
            var quote = GetById(id);

            if (quote == null)
            {
                return false;
            }

            _quotes.Remove(quote);

            return true;
        }
    }
}
