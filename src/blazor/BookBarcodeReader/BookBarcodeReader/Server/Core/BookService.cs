using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Server.Core
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

        public StoreBookResult Store(StoreNewBookRequest book)
        {
            if (_query.GetByIsbn(book.ISBN) == null)
            {
                _command.StoreBook(book);
                return new SuccessfulStoreBookResult();
            }
            return new ISBNAlreadyStoredStoreBookResult();
        }
    }
}
