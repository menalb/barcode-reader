using System.Collections.Generic;

namespace BookBarcodeReader.Shared
{
    public class BarcodeLookupResult
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Language { get; set; }
        public string PublishedDate { get; set; }
        public IEnumerable<BookIdentifier> Identifiers { get; set; }
        public BookImage Images { get; set; }
    }

    public class BookImage
    {
        public string Thumbnail { get; set; }
        public string SmallThumbnail { get; set; }
    }

    public class BookIdentifier
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
