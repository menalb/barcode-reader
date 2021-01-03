using System.ComponentModel.DataAnnotations;

using BookBarcodeReader.Shared.Book;

namespace BookBarcodeReader.Client.Components
{
    public class BookModel : BookBaseEntity
    {
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
        [Required]
        public string Id { get; set; }
    }
}
