using CheckerService.reposetorys;
using Microsoft.AspNetCore.SignalR.Client;

namespace CheckerService
{

    
    
    public class Sample : BackgroundService
    {
        private readonly ILogger<Sample> _logger;
        private readonly IBookReposetory _bookReposetory;
        private DateTime _lastBookCreateDate;
        private IStartupConfig _startupConfig;
        private ILibraryHub _libraryHub;    
        private HubConnection _hubConnection;

        public Sample(ILogger<Sample> logger, IBookReposetory bookReposetory, IStartupConfig startupConfig, ILibraryHub libraryHub)
        {
            _logger = logger;
            _bookReposetory = bookReposetory;
            _startupConfig = startupConfig;
            _libraryHub = libraryHub;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("here start");
            
            string hubUrl = _startupConfig.HubUrl;
            Console.WriteLine(hubUrl);

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

            await _hubConnection.StartAsync(cancellationToken);
            _logger.LogInformation("Connected to SignalR Hub.");

            _lastBookCreateDate = await _bookReposetory.GetLastCreateDate();
            await base.StartAsync(cancellationToken);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("here execute");

            while (!stoppingToken.IsCancellationRequested)
            {
               ;
                var currentLastDate = await _bookReposetory.GetLastCreateDate();
                if (_lastBookCreateDate < currentLastDate)
                {
                    Console.WriteLine("book added . sending a message");
                    _lastBookCreateDate = currentLastDate;
                    var books = await _bookReposetory.Get();
                    await _libraryHub.SendAddBook("book added");
                }


                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                await Task.Delay(_startupConfig.SampleIntervalInMs, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _hubConnection.StopAsync(cancellationToken);
            await _hubConnection.DisposeAsync();
            _logger.LogInformation("Disconnected from SignalR Hub.");
            await base.StopAsync(cancellationToken);
        }


    }
}
