using System.Collections.Generic;
using System.Threading.Tasks;

using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Server.Book
{
    public interface IBookQuery
    {
        Task<IEnumerable<BookEntity>> GetAll();
        Task<IEnumerable<BookEntity>> GetByTitle(string title);
        Task<BookEntity> GetByIsbn(string isbn);
    }
}
