using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BookBarcodeReader.Shared.Book
{
    public class BookEntity: BookBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
    public class BookBaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Language { get; set; }
        public string PublishedDate { get; set; }
        public IEnumerable<BookIdentifier> Identifiers { get; set; }
        public BookImages Images { get; set; }
    }

    public class BookImages
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
