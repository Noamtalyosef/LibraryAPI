using CheckerService.reposetorys;

namespace CheckerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Register your services
                services.AddSingleton<ILibraryHub, LibraryHub>();
                services.AddSingleton<IStartupConfig, StartupConfig>();
                services.AddSingleton<IBookReposetory, BookReposetory>();       
                services.AddHostedService<Sample>();
            });
    }
}