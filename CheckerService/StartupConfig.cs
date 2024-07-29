using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckerService
{
    public class StartupConfig : IStartupConfig
    {
        public string ConnectionString { get; set; }
        public int SampleIntervalInMs { get; set; }
        public string HubUrl { get; set; }

        public StartupConfig(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Library")!;
            SampleIntervalInMs = int.Parse(configuration["SampleIntervalInMs"]!);
            HubUrl = configuration["HubUrl"]!;
        }
    }

    public interface IStartupConfig
    {
        string  ConnectionString { get; set; }
         int SampleIntervalInMs { get; set; }

        string HubUrl {  get; set; }         
    }
}
