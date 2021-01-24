using BookBarcodeReader.Shared.Book;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.Linq;

namespace BookBarcodeReader.Server.Book
{
    public class BooksToExcel : System.IDisposable
    {
        private readonly IEnumerable<string[]> titles =
            new List<string[]> { new string[] { "Title", "Sub Title", "Authors", "Language", "Description" } };

        private readonly IEnumerable<BookEntity> _books;
        private readonly XLWorkbook _wb;
        public BooksToExcel(IEnumerable<BookEntity> books)
        {
            _books = books;
            _wb = new XLWorkbook();
        }
        public XLWorkbook Build()
        {

            var ws = _wb.Worksheets.Add("Books");
            AddFormattedTitle(ws);

            if (_books != null && _books.Any())
            {
                ws.Cell(2, 1).InsertData(BuildBooksExcelModel(_books));
                ws.Columns().AdjustToContents();
            }

            return _wb;
        }

        private void AddFormattedTitle(IXLWorksheet ws)
        {
            var rangeTitle = ws.Cell(1, 1).InsertData(titles);
            rangeTitle.AddToNamed("Titles");

            var titlesStyle = _wb.Style;
            titlesStyle.Font.Bold = true;

            titlesStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            _wb.NamedRanges.NamedRange("Titles").Ranges.Style = titlesStyle;
        }

        private record BookExcelModel(string Title, string SubTitle, string Authors, string Language, string Description);
        private static IEnumerable<BookExcelModel> BuildBooksExcelModel(IEnumerable<BookEntity> books) =>
            books.Select(book => new BookExcelModel(
                book.Title,
                book.SubTitle,
                book.Authors?.Select(author => author.Name).Aggregate((a, b) => $"{a},{b}"),
                book.Language,
                book.Description
                ));

        public void Dispose()
        {
            _wb.Dispose();
        }
    }
}

