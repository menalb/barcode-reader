using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using BookBarcodeReader.Server.Core;
using BookBarcodeReader.Shared.Book;

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
            var result = await _service.Store(newBookRequest);
            return PareStoreResult(result);
        }

        public IActionResult PareStoreResult(StoreBookResult result)
        {
            switch (result)
            {
                case SuccessfulStoreBookResult okResult: return Ok(okResult.Book);
                case ISBNAlreadyStoredStoreBookResult _: return Conflict("ISBN");
                default: return StatusCode(500); ;
            }
        }
    }
}
