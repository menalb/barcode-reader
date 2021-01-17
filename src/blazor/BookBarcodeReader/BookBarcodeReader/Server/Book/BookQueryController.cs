using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BookBarcodeReader.Server.Book
{
    [ApiController]
    [Route("book")]
    public class BookQueryController : ControllerBase
    {
        private readonly ILogger<BookQueryController> _logger;
        private readonly IBookQuery _query;

        public BookQueryController(ILogger<BookQueryController> logger, IBookQuery query)
        {
            _query = query;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var result = await _query.GetAll();
            return Ok(result);
        }
    }
}
