namespace LibraryAPI.Interfaces
{
    public interface IBookFilesHelper
    {
        Task<string> CreatePhoto(IFormFile photoFile);
        void DeletePhoto(string photoFilePath);
        Task<string> CreateCopy(IFormFile photoFile);
        void DeleteCopy(string photoFilePath);
    }
}
