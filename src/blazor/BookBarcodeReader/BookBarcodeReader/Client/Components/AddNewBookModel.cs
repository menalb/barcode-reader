using System.ComponentModel.DataAnnotations;
using System.Linq;
using BookBarcodeReader.Shared;
using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Client.Components
{
    public class BookModel : BookBaseEntity
    {
        public BookModel()
        {

        }
        public BookModel(BookBaseEntity Book)
        {
            Title = Book.Title;
            SubTitle = Book.SubTitle;
            Description = Book.Description;
            AuthorsInput = Book.Authors?.Select(a => a.Name).Aggregate((a, b) => $"{a},{b}");
            CategoriesInput = Book.Categories?.Select(a => a.Name).Aggregate((a, b) => $"{a},{b}");
            IdentifiersInput = Book.Identifiers?.Select(a => $"{a.Type}:{a.Value}").Aggregate((a, b) => $"{a},{b}");
            Language = Book.Language;
            Images = Book.Images;
            Link = Book.Link;
            PublishedDate = Book.PublishedDate;
            Publisher = Book.Publisher;
        }

       

        [Required]
        public new string Title { get; set; }
        public string AuthorsInput { get; set; }
        public string CategoriesInput { get; set; }
        [Required]
        public string IdentifiersInput { get; set; }
    }

    public class AddNewBookModel : BookModel
    {        
    }
 
    public class EditBookModel : BookModel
    {
        public EditBookModel(BookEntity Book) : base(Book)
        {
            Id = Book.Id;
        }
        [Required]
        public string Id { get; set; }
    }
}
