using Microsoft.AspNetCore.Http;

namespace LibraryModels.Models
{
    public class NewBook
    {

        public  List<Author> NewAuthors { get; set; } = new List<Author>(); 
        public Book Book { get; set; } = new Book();
        public List<int>? ExistingAuthorsIds { get; set; } = new List<int>();
        public IFormFile? BookPicture { get; set; } 
        public IFormFile? BookCopy { get; set; }

    }
}