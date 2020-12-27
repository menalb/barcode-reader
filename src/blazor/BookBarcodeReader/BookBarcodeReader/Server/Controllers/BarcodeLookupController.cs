using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

using BookBarcodeReader.Server.Models;
using BookBarcodeReader.Shared;
using static BookBarcodeReader.Server.Models.GoogleApiResponse;

namespace BookBarcodeReader.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarcodeLookupController : ControllerBase
    {
        private readonly ILogger<BarcodeLookupController> logger;
        private readonly IOptions<GoogleApiConfig> _apiConfig;

        public BarcodeLookupController(ILogger<BarcodeLookupController> logger, IOptions<GoogleApiConfig> apiConfig)
        {
            _apiConfig = apiConfig;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<BarcodeLookupResult>> Get(string barcode)
        {
            return await GetISBNInfo(barcode);
        }


        private async Task<IEnumerable<BarcodeLookupResult>> GetISBNInfo(string barcode)
        {
            var apiKey = _apiConfig.Value.ApiKey;
            var queryString = $"q=isbn={barcode}&key={apiKey}";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiConfig.Value.BaseUrl);

                var response = await client.GetAsync($"books/v1/volumes?{queryString}");
                if (response.IsSuccessStatusCode)
                {

                    var message = await response.Content.ReadAsStringAsync();
                    var gApiResponse = JsonConvert
                        .DeserializeObject<GoogleApiResponse>(message);
                    return ParseResult(gApiResponse);
                }
            }
            return null;
        }

        private IEnumerable<BarcodeLookupResult> ParseResult(GoogleApiResponse result)
        {
            return result.Items.Select(item => new BarcodeLookupResult
            {
                Title = item.VolumeInfo.Title,
                Description = item.VolumeInfo.Description,
                Link = item.SelfLink,
                Images = ParseImage(item.VolumeInfo.Images),
                PublishedDate = item.VolumeInfo.PublishedDate,
                Identifiers = ParseIdentifiers(item.VolumeInfo.IndustryIdentifiers)
            });
        }
        private BookImage ParseImage(ImageLinks image)
        {
            if (!(image is null))
                return new BookImage
                {
                    SmallThumbnail = image.SmallThumbnail,
                    Thumbnail = image.Thumbnail
                };
            return null;
        }

        private IEnumerable<BookIdentifier> ParseIdentifiers(IEnumerable<IndustryIdentifiers> identifiers)
        {
            if (!(identifiers is null))
                return identifiers.Select(idetifier => new BookIdentifier
                {
                    Type = idetifier.Type,
                    Value = idetifier.Identifier
                });
            return null;
        }



    }
}
