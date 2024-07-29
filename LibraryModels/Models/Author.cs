namespace LibraryModels.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public City? City { get; set; }
        public int YearOfBirth { get; set; }
        public List<Book>? Books { get; set; } = new List<Book>();
    }
}
