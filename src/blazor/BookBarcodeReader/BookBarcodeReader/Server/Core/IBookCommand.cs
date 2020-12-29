
using System.Threading.Tasks;

using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Server.Core
{
    public interface IBookCommand
    {
        Task<BookEntity> StoreBook(StoreNewBookRequest book);
    }
}
