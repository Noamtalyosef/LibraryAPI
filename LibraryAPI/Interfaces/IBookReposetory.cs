using LibraryModels.Models;

namespace LibraryAPI.Interfaces
{
    public interface IBookReposetory
    {
        Task<List<Book>> GetAllAsync();
        Task<List<Publisher>>GetAllPublishersAsync();
        Task<bool> DeleteAsync(int BookId);
        Task<List<Book>> SearchBooksAsync(string searchInput);
        Task UpdateAsync(Book book);
        Task CreateAsync(NewBook bookDto);

        Task<List<Book>>  GetBooksById(List<int> booksIds);
    }
}
