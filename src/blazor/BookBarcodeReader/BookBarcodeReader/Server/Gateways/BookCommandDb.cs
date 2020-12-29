﻿using BookBarcodeReader.Server.Core;
using BookBarcodeReader.Server.Infrastructure;
using BookBarcodeReader.Shared.Book;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace BookBarcodeReader.Server.Gateways
{
    public class BookCommandDb : IBookCommand
    {
        private readonly IMongoCollection<BookEntity> _books;

        public BookCommandDb(DbSettings dbSettings) =>
           _books = new MongoClient(dbSettings.ConnectionString)
               .GetDatabase(dbSettings.DatabaseName)
               .GetCollection<BookEntity>(dbSettings.CollectionName);

        public async Task<BookEntity> StoreBook(StoreNewBookRequest newBook)
        {
            var book = MapStoreRequest(newBook);
            await _books.InsertOneAsync(book);
            return book;
        }

        private BookEntity MapStoreRequest(StoreNewBookRequest request) =>
          new BookEntity
          {
              Description = request.Description,
              Identifiers = request.Identifiers,
              Images = request.Images,
              Language = request.Language,
              Link = request.Link,
              PublishedDate = request.PublishedDate,
              Title = request.Title
          };
    }
}