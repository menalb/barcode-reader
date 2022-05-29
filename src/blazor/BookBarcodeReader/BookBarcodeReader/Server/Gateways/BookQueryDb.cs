using BookBarcodeReader.Server.Book;
using BookBarcodeReader.Server.Infrastructure;
using BookBarcodeReader.Shared.Book;

using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookBarcodeReader.Server.Gateways
{
    public class BookQueryDb : IBookQuery
    {
        private readonly IMongoCollection<BookEntity> _books;

        public BookQueryDb(DbSettings dbSettings) =>
            _books = new MongoClient(dbSettings.ConnectionString)
                .GetDatabase(dbSettings.DatabaseName)
                .GetCollection<BookEntity>(dbSettings.CollectionName);

        public async Task<IEnumerable<BookEntity>> GetAll() =>
            await _books.Find(Builders<BookEntity>.Filter.Empty).SortBy(book => book.Title).ToListAsync();

        public async Task<BookEntity> GetById(string id) =>
            await _books.Find(book => book.Id == id).SingleOrDefaultAsync();

        public async Task<BookEntity> GetByIsbn(string isbn) =>
            await _books.Find(book => book.ISBN == isbn).FirstOrDefaultAsync();

        public async Task<IEnumerable<BookEntity>> GetByTitle(string title) =>
            await _books.Find(book => book.Title.Contains(title, System.StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
    }
}
