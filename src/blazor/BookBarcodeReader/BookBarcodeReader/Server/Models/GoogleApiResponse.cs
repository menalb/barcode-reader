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
        }

        public class VolumeInfo
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("description")]
            public string Description { get; set; }
            [JsonProperty("imageLinks")]
            public ImageLinks Images { get; set; }
        }

        public class ImageLinks
        {
            [JsonProperty("smallThumbnail")]
            public string SmallThumbnail { get; set; }
            [JsonProperty("thumbnail")]
            public string Thumbnail { get; set; }
        }

    }
}
