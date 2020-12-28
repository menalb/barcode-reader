using BookBarcodeReader.Server.Infrastructure;
using BookBarcodeReader.Shared.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BookBarcodeReader.Server.Controllers
{
    [ApiController]
    [Route("book")]
    public class BookCommandController : ControllerBase
    {
        private readonly ILogger<BookCommandController> logger;
        private readonly IMongoCollection<BookEntity> _books;

        public BookCommandController(ILogger<BookCommandController> logger, IOptions<DbSettings> dbSettings)
        {
            _books = new MongoClient(dbSettings.Value.ConnectionString)
                .GetDatabase(dbSettings.Value.DatabaseName)
                .GetCollection<BookEntity>(dbSettings.Value.CollectionName);

            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Store(AddNewBookRequest newBookRequest)
        {
            var book = new BookEntity
            {
                Description = newBookRequest.Description,
                Identifiers = newBookRequest.Identifiers,
                Images = newBookRequest.Images,
                Language = newBookRequest.Language,
                Link = newBookRequest.Link,
                PublishedDate = newBookRequest.PublishedDate,
                Title = newBookRequest.Title
            };
            await _books.InsertOneAsync(book);
            return Ok(book);
        }
    }

    public class Book
    {

    }
}
