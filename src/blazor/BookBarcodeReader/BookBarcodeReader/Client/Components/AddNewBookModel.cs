using BookBarcodeReader.Shared.Book;
using System.ComponentModel.DataAnnotations;

namespace BookBarcodeReader.Client.Components
{
    public class AddNewBookModel : BookBaseEntity
    {
        [Required]
        public new string Title { get; set; }
        public string AuthorsInput { get; set; }
        public string CategoriesInput { get; set; }
        [Required]
        public string IdentifiersInput { get; set; }
    }
}
