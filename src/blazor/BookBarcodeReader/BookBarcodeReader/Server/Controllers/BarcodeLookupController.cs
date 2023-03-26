using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

using BookBarcodeReader.Server.Models;
using BookBarcodeReader.Shared;
using static BookBarcodeReader.Server.Models.GoogleApiResponse;
using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class BarcodeLookupController : ControllerBase
{
    private readonly ILogger<BarcodeLookupController> _logger;
    private readonly IOptions<GoogleApiConfig> _apiConfig;

    public BarcodeLookupController(ILogger<BarcodeLookupController> logger, IOptions<GoogleApiConfig> apiConfig)
    {
        _apiConfig = apiConfig;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<BarcodeLookupResult>> Get(string barcode)
    {
        var books = await GetISBNInfo(barcode);
        return books ?? new List<BarcodeLookupResult>();
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
                var gApiResponse = System.Text.Json.JsonSerializer.Deserialize<GoogleApiResponse>(message);
                return ParseResult(gApiResponse);
            }
        }
        return null;
    }

    private IEnumerable<BarcodeLookupResult> ParseResult(GoogleApiResponse result) =>
        result.Items?.Select(item => new BarcodeLookupResult
        {
            Id = item.Id,
            Title = item.VolumeInfo.Title,
            Description = item.VolumeInfo.Description,
            Link = item.SelfLink,
            ISBN = ParseISBN(ParseIdentifiers(item.VolumeInfo.IndustryIdentifiers)),
            Images = ParseImage(item.VolumeInfo.Images),
            PublishedDate = item.VolumeInfo.PublishedDate,
            Identifiers = ParseIdentifiers(item.VolumeInfo.IndustryIdentifiers),
            SubTitle = item.VolumeInfo.SubTitle,
            Publisher = item.VolumeInfo.Publisher,
            Language = item.VolumeInfo.Language,
            Categories = item.VolumeInfo.Categories?.Select(category => new CategoryInfo { Name = category }),
            Authors = item.VolumeInfo.Authors?.Select(author => new AuthorInfo { Name = author }),
        });
    private string ParseISBN(IEnumerable<BookIdentifier> identifiers)
    {
        var isbn = identifiers?.FirstOrDefault(id => id.Type == "ISBN_13");
        if (isbn != null)
        {
            return isbn.Value;
        }
        return identifiers?.FirstOrDefault().Value;
    }
    private static BookImages ParseImage(ImageLinks image) =>
        image is not null ?
            new BookImages
            {
                SmallThumbnail = image.SmallThumbnail,
                Thumbnail = image.Thumbnail
            }
            : null;

    private static IEnumerable<BookIdentifier> ParseIdentifiers(IEnumerable<IndustryIdentifiers> identifiers) =>
        identifiers?.Select(idetifier => new BookIdentifier
        {
            Type = idetifier.Type,
            Value = idetifier.Identifier
        });
}
