using System.Collections.Generic;

using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Server.Core
{
    public interface IBookQuery
    {
        IEnumerable<BookEntity> GetAll();
        IEnumerable<BookEntity> GetByTitle(string title);
        BookEntity GetByIsbn(string isbn);
    }
}
