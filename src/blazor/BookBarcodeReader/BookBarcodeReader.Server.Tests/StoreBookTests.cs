using Bogus;
using BookBarcodeReader.Server.Models;
using BookBarcodeReader.Shared.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BookBarcodeReader.Server.Tests
{
    public class Given_a_storeBook_request
    {
        [Fact]
        public void When_there_is_not_already_a_book_with_that_isbn_It_store()
        {
            var isbn = "9781910055410";
            var book = GenerateBaseBook(isbn);

            var booksQuery = new BookQueryFake();
            var bookCommand = new BookCommandFake();
            var service = new BookService(booksQuery, bookCommand);
            booksQuery.AddBookToReturn(ToBookEntity(book as BookBaseEntity));

            var result = service.Store(book);

            Assert.IsNotType<SuccessfulStoreBookResult>(result);
            Assert.DoesNotContain(bookCommand.StoredBooks, book => book.ISBN == isbn);
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

    public class BookService
    {
        private readonly IBookQuery _query;
        private readonly IBookCommand _command;
        public BookService(IBookQuery query, IBookCommand command)
        {
            _query = query;
            _command = command;
        }

        public StoreBookResult Store(StoreNewBookRequest book)
        {
            if (_query.GetByIsbn(book.ISBN) == null)
            {
                _command.StoreBook(book);
                return new SuccessfulStoreBookResult();
            }
            return new ISBNAlreadyStoredStoreBookResult();
        }
    }

    public abstract class StoreBookResult { }
    public class SuccessfulStoreBookResult : StoreBookResult { }
    public class ISBNAlreadyStoredStoreBookResult : StoreBookResult { }

    public class BookCommandFake : IBookCommand
    {
        private readonly IList<StoreNewBookRequest> _storedBooks;
        public BookCommandFake()
        {
            _storedBooks = new List<StoreNewBookRequest>();
        }
        public void StoreBook(StoreNewBookRequest book)
        {
            _storedBooks.Add(book);
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
        public IEnumerable<BookEntity> GetAll()
        {
            return _booksToReturn;
        }

        public BookEntity GetByIsbn(string isbn)
        {
            return _booksToReturn.FirstOrDefault(book => book.ISBN.ToLower() == isbn.ToLower());
        }

        public IEnumerable<BookEntity> GetByTitle(string title)
        {
            return _booksToReturn.Where(book => book.Title.ToLower() == title.ToLower());
        }
    }
    public interface IBookCommand
    {
        void StoreBook(StoreNewBookRequest book);
    }
    public interface IBookQuery
    {
        IEnumerable<BookEntity> GetAll();
        IEnumerable<BookEntity> GetByTitle(string title);
        BookEntity GetByIsbn(string isbn);
    }
}
