namespace BookBarcodeReader.Server.Core
{
    public abstract class StoreBookResult { }
    public class SuccessfulStoreBookResult : StoreBookResult { }
    public class ISBNAlreadyStoredStoreBookResult : StoreBookResult { }
}
