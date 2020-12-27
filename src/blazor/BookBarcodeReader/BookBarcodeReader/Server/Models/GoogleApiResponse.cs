using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BookBarcodeReader.Server.Models
{
    public class GoogleApiResponse
    {
        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }
        [JsonProperty("items")]
        public IEnumerable<ApiItem> Items { get; set; }

        public class ApiItem
        {
            [JsonProperty("volumeInfo")]
            public VolumeInfo VolumeInfo { get; set; }
            [JsonProperty("selfLink")]
            public string SelfLink { get; set; }
            [JsonProperty("etag")]
            public string ETag { get; set; }

        }

        public class VolumeInfo
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("description")]
            public string Description { get; set; }
            [JsonProperty("imageLinks")]
            public ImageLinks Images { get; set; }
            [JsonProperty("language")]
            public string Language { get; set; }
            [JsonProperty("industryIdentifiers")]
            public IEnumerable<IndustryIdentifiers> IndustryIdentifiers { get; set; }
            [JsonProperty("publishedDate")]
            public string PublishedDate { get; set; }
        }

        public class ImageLinks
        {
            [JsonProperty("smallThumbnail")]
            public string SmallThumbnail { get; set; }
            [JsonProperty("thumbnail")]
            public string Thumbnail { get; set; }
        }

        public class IndustryIdentifiers
        {
            [JsonProperty("type")]
            public string Type { get; set; }
            [JsonProperty("identifier")]
            public string Identifier { get; set; }
        }

    }
}
