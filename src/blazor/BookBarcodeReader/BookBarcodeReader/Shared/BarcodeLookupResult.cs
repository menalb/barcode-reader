namespace BookBarcodeReader.Shared
{
    public class BarcodeLookupResult
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public BookImage Images { get; set; }
    }

    public class BookImage
    {
        public string Thumbnail { get; set; }
        public string SmallThumbnail { get; set; }
    }
}
