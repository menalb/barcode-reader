
using System.Threading.Tasks;

using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Server.Book
{
    public interface IBookCommand
    {
        Task<BookEntity> StoreBook(StoreNewBookRequest book);
        Task<BookEntity> UpdateBook(UpdateBookRequest book);
    }
}
