using Bogus;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Threading.Tasks;

using BookBarcodeReader.Server.Core;
using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Server.Tests
{
    public class Given_a_storeBook_request
    {
        private readonly BookQueryFake _query;
        private readonly BookCommandFake _command;

        public Given_a_storeBook_request()
        {
            _query = new BookQueryFake();
            _command = new BookCommandFake();
        }

        [Fact]
        public async Task When_there_is_not_already_a_book_with_that_isbn_It_store()
        {
            var isbn = "9781910055410";
            var book = GenerateBaseBook(isbn);

            var service = new BookService(_query, _command);
            _query.AddBookToReturn(ToBookEntity(book));

            var result = await service.Store(book);

            Assert.IsNotType<SuccessfulStoreBookResult>(result);
            Assert.DoesNotContain(_command.StoredBooks, book => book.ISBN == isbn);
        }


        private StoreNewBookRequest GenerateBaseBook(string isbn = "") =>
         new Faker<StoreNewBookRequest>()
            .StrictMode(true)
            .RuleFor(p => p.ISBN, f => string.IsNullOrEmpty(isbn) ? f.Commerce.Ean13() : isbn)
            .RuleFor(p => p.Title, f => f.Name.Random.AlphaNumeric(10))
            .RuleFor(p => p.Description, f => f.Lorem.Random.AlphaNumeric(100))
            .RuleFor(p => p.Language, f => "en")
            .RuleFor(p => p.Images, f => null)
            .RuleFor(p => p.Identifiers, f => null)
            .RuleFor(p => p.Link, f => f.Internet.Url())
            .RuleFor(p => p.PublishedDate, f => f.Date.Past().Year.ToString())
            .Generate();

        private BookEntity ToBookEntity(BookBaseEntity book) =>
            new BookEntity
            {
                Id = string.Empty,
                Title = book.Title,
                Description = book.Description,
                Identifiers = book.Identifiers,
                Images = book.Images,
                ISBN = book.ISBN,
                Language = book.Language,
                Link = book.Link,
                PublishedDate = book.PublishedDate
            };

    }

    public class BookCommandFake : IBookCommand
    {
        private readonly IList<StoreNewBookRequest> _storedBooks;
        public BookCommandFake()
        {
            _storedBooks = new List<StoreNewBookRequest>();
        }
        public Task StoreBook(StoreNewBookRequest book)
        {
            return Task.Factory.StartNew(() => _storedBooks.Add(book));
        }

        public IEnumerable<StoreNewBookRequest> StoredBooks => _storedBooks;
    }
    public class BookQueryFake : IBookQuery
    {
        private readonly IList<BookEntity> _booksToReturn;
        public BookQueryFake()
        {
            _booksToReturn = new List<BookEntity>();
        }

        public void AddBookToReturn(BookEntity book)
        {
            _booksToReturn.Add(book);
        }
        public async Task<IEnumerable<BookEntity>> GetAll()
        {
            return await Task.FromResult(_booksToReturn);
        }

        public Task<BookEntity> GetByIsbn(string isbn)
        {
            return Task.FromResult(_booksToReturn.FirstOrDefault(book => book.ISBN.ToLower() == isbn.ToLower()));
        }

        public Task<IEnumerable<BookEntity>> GetByTitle(string title)
        {
            return Task.FromResult(_booksToReturn.Where(book => book.Title.ToLower() == title.ToLower()));
        }
    }

}
