using LibraryAPI.Interfaces;

namespace LibraryAPI
{
    public class StartupConfig : IStartupConfig
    {
        public string ConnectionString {  get; set; }

        public StartupConfig(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Library")!;
        }
    }
}
