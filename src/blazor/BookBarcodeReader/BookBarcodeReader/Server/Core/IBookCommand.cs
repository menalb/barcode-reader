
using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Server.Core
{
    public interface IBookCommand
    {
        void StoreBook(StoreNewBookRequest book);
    }
}
