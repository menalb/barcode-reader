using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookBarcodeReader.Server.Models
{
    public class GoogleApiResponse
    {
        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }
        [JsonPropertyName("items")]
        public IEnumerable<ApiItem> Items { get; set; }

        public class ApiItem
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }
            [JsonPropertyName("volumeInfo")]
            public VolumeInfo VolumeInfo { get; set; }
            [JsonPropertyName("selfLink")]
            public string SelfLink { get; set; }
            [JsonPropertyName("etag")]
            public string ETag { get; set; }
        }

        public class VolumeInfo
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }
            [JsonPropertyName("subtitle")]
            public string SubTitle { get; set; }
            [JsonPropertyName("description")]
            public string Description { get; set; }
            [JsonPropertyName("imageLinks")]
            public ImageLinks Images { get; set; }
            [JsonPropertyName("language")]
            public string Language { get; set; }
            [JsonPropertyName("authors")]
            public string[] Authors { get; set; }
            [JsonPropertyName("categories")]
            public string[] Categories { get; set; }
            [JsonPropertyName("publisher")]
            public string Publisher { get; set; }
            [JsonPropertyName("industryIdentifiers")]
            public IEnumerable<IndustryIdentifiers> IndustryIdentifiers { get; set; }
            [JsonPropertyName("publishedDate")]
            public string PublishedDate { get; set; }
        }

        public class ImageLinks
        {
            [JsonPropertyName("smallThumbnail")]
            public string SmallThumbnail { get; set; }
            [JsonPropertyName("thumbnail")]
            public string Thumbnail { get; set; }
        }

        public class IndustryIdentifiers
        {
            [JsonPropertyName("type")]
            public string Type { get; set; }
            [JsonPropertyName("identifier")]
            public string Identifier { get; set; }
        }
    }
}
