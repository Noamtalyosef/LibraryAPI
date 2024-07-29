using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckerService
{

    public interface ILibraryHub
    {
        public Task SendAddBook(string message);
    }
    public class LibraryHub : Hub, ILibraryHub  
    {
        public async Task SendAddBook(string message)
        {
            await Clients.All.SendAsync("reciveAddBook",message);
        }
    }
}
