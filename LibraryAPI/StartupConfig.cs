using LibraryAPI.Interfaces;

namespace LibraryAPI
{
    public class StartupConfig : IStartupConfig
    {
        public string ConnectionString {  get; set; }
        public string PhotosPath { get; set; }  
        public string CopysPath { get; set; }
        public string DefaultImagePath { get; set; }

        public StartupConfig(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Library")!;
            PhotosPath = configuration["PhotosPath"]!;
            CopysPath = configuration["CopysPath"]!;
            DefaultImagePath = configuration["DefaultImagePath"]!;
        }
    }
}
