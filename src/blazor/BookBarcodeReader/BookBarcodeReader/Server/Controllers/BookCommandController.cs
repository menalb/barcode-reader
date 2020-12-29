using BookBarcodeReader.Server.Core;
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
        private readonly ILogger<BookCommandController> _logger;
        private readonly BookService _service;

        public BookCommandController(ILogger<BookCommandController> logger, BookService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Store(StoreNewBookRequest newBookRequest)
        {
            var book = await _service.Store(newBookRequest);
            return Ok(book);
        }

      
    }
}
