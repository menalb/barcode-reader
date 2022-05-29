using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Server.Book
{
    [ApiController]
    [Route("book")]
    public class BookCommandController : ControllerBase
    {
        private readonly ILogger<BookCommandController> _logger;
        private readonly BookService _service;
        private readonly IBookQuery _query;

        public BookCommandController(ILogger<BookCommandController> logger, BookService service, IBookQuery query)
        {
            _service = service;
            _logger = logger;
            _query = query;
        }

        [HttpPost]
        public async Task<IActionResult> Store(StoreNewBookRequest newBookRequest)
        {
            var result = await _service.Store(newBookRequest);
            return PareStoreResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBookRequest updateBookRequest)
        {
            if (updateBookRequest == null) return BadRequest();

            if (_query.GetById(updateBookRequest.Id) == null) return BadRequest();

            await _service.Update(updateBookRequest);

            return Ok();
        }

        private IActionResult PareStoreResult(StoreBookResult result)
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
