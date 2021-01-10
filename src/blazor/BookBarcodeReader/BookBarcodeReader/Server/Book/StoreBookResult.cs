using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Server.Book
{
    public abstract class StoreBookResult { }
    public class SuccessfulStoreBookResult : StoreBookResult
    {
        public SuccessfulStoreBookResult(BookEntity storedBook) => Book = storedBook;
        public BookEntity Book { get; }
    }
    public class ISBNAlreadyStoredStoreBookResult : StoreBookResult { }
}
