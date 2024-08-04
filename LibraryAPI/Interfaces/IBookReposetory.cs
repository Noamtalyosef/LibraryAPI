using LibraryModels.Models;

namespace LibraryAPI.Interfaces
{
    public interface IBookReposetory
    {
        Task<List<Book>> GetAllAsync();
        Task<List<Publisher>>GetAllPublishersAsync();
        Task<bool> DeleteAsync(Book Book);
        Task<List<Book>> SearchBooksAsync(string searchInput);
        Task UpdateAsync(Book book, List<IFormFile> bookFiles);
        Task CreateAsync(NewBook bookDto);
        Task<List<Book>>  GetBooksById(List<int> booksIds);
        Task DeletePhoto(Book book);
    }
}
