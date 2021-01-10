using System.Threading.Tasks;

using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Server.Book
{
    public class BookService
    {
        private readonly IBookQuery _query;
        private readonly IBookCommand _command;
        public BookService(IBookQuery query, IBookCommand command)
        {
            _query = query;
            _command = command;
        }

        public async Task<StoreBookResult> Store(StoreNewBookRequest book)
        {
            if (await _query.GetByIsbn(book.ISBN) == null)
            {
                var storedBook = await _command.StoreBook(book);
                return new SuccessfulStoreBookResult(storedBook);
            }
            return new ISBNAlreadyStoredStoreBookResult();
        }
    }
}
