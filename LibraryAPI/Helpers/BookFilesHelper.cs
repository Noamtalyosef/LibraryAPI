using LibraryAPI.Interfaces;

namespace LibraryAPI.Helpers
{
    public class BookFilesHelper : IBookFilesHelper
    {

        private const string _photosUrl = "wwwroot/photos/";
        private const string _copysUrl = "wwwroot/copys/";
        public async Task<string> CreateCopy(IFormFile copyFile)
        {
            var uniqueCopyPath = $"{Path.GetFileNameWithoutExtension(copyFile.FileName)}_{Guid.NewGuid()}{Path.GetExtension(copyFile.FileName)}";
            var copyFilePath = Path.Combine(_copysUrl, uniqueCopyPath);
            using (var stream = new FileStream(copyFilePath, FileMode.Create))
            {
                await copyFile.CopyToAsync(stream);
            }

            return uniqueCopyPath;
        }

        public async Task<string> CreatePhoto(IFormFile photoFile)
        {
           
            var uniquePicturePath = $"{Path.GetFileNameWithoutExtension(photoFile.FileName)}_{Guid.NewGuid()}{Path.GetExtension(photoFile.FileName)}";
            var pictureFilePath = Path.Combine(_photosUrl, uniquePicturePath);
            using (var stream = new FileStream(pictureFilePath, FileMode.Create))
            {
                await photoFile!.CopyToAsync(stream);
            }

            return uniquePicturePath;
        }

        public void DeleteCopy(string copyFilePath)
        {
            var fullPath = Path.Combine(_copysUrl, copyFilePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public void DeletePhoto(string photoFilePath)
        {
            var fullPath = Path.Combine(_photosUrl, photoFilePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
