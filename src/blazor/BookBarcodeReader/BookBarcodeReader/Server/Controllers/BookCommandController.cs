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
            var book = MapStoreRequest(newBookRequest);
            await _books.InsertOneAsync(book);
            return Ok(book);
        }

        private BookEntity MapStoreRequest(AddNewBookRequest request)=>
            new BookEntity
            {
                Description = request.Description,
                Identifiers = request.Identifiers,
                Images = request.Images,
                Language = request.Language,
                Link = request.Link,
                PublishedDate = request.PublishedDate,
                Title = request.Title
            };
    }
}
