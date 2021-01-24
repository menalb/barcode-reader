using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.IO;
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

        [HttpGet("export")]
        public async Task<IActionResult> AllToExcel()
        {
            var result = await _query.GetAll();

            //save file to memory stream and return it as byte array
            using var ms = new MemoryStream();
            using var be = new BooksToExcel(result);
            be.Build().SaveAs(ms);
            var stream = ms.ToArray();

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Books.xlsx");
        }
    }
}

