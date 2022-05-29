using MongoDB.Driver;
using System.Threading.Tasks;

using BookBarcodeReader.Server.Book;
using BookBarcodeReader.Server.Infrastructure;
using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Server.Gateways
{
    public class BookCommandDb : IBookCommand
    {
        private readonly IMongoCollection<BookEntity> _books;

        public BookCommandDb(DbSettings dbSettings) =>
           _books = new MongoClient(dbSettings.ConnectionString)
               .GetDatabase(dbSettings.DatabaseName)
               .GetCollection<BookEntity>(dbSettings.CollectionName);

        public async Task<BookEntity> StoreBook(StoreNewBookRequest newBook)
        {
            var book = MapStoreRequest(newBook);
            await _books.InsertOneAsync(book);
            return book;
        }

        public async Task<BookEntity> UpdateBook(UpdateBookRequest bookToUpdate)
        {
            var book = MapStoreRequest(bookToUpdate);
            book.Id = bookToUpdate.Id;
            await _books.ReplaceOneAsync(f => f.Id == book.Id, book);
            return book;
        }

        private BookEntity MapStoreRequest(BookBaseEntity request) =>
          new BookEntity
          {
              Description = request.Description,
              Identifiers = request.Identifiers,
              Images = request.Images,
              Language = request.Language,
              Link = request.Link,
              PublishedDate = request.PublishedDate,
              Title = request.Title,
              ISBN = request.ISBN,
              Authors = request.Authors,
              Categories = request.Categories,
              Publisher = request.Publisher,
              SubTitle = request.SubTitle
          };
    }
}
