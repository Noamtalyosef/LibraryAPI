using LibraryModels.Models;

namespace LibraryAPI.Interfaces
{
    public interface IAuthorReposetory
    {
        Task<List<Author>> GetAllAsync();
        Task<List<City>> GetAllCitysAsync();
        Task<bool> DeleteAsync(int authorId);
        Task<List<Author>> SearchBooksAsync(string searchInput);
        Task UpdateAsync(Author author);
        Task CreateAsync(Author author);
        Task<List<Author>> GetAuthorsById(List<int> AuthorsIds);
    }
}
