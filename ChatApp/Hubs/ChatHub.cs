using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR; 
using Microsoft.Extensions.Logging; 

namespace ChatApp.Hubs{
    class ChatHub : Hub{
        public const string hubPath = "/ChatHub"; 
        private readonly ILogger<ChatHub> _logger;

        //construct
        public ChatHub(ILogger<ChatHub> logger){
            _logger = logger; 
        }
        public async override Task OnConnectedAsync()
        {
            _logger.LogInformation("User Connected... "); 
            await Clients.All.SendAsync("sendMessage", "TestMessage", "TestUser");
            await base.OnConnectedAsync(); 
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation("User Disonnected... "); 
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message, string userName){
            _logger.LogInformation($"{message} Received from {userName}");
            await Clients.All.SendAsync("sendMessage", message, userName);
        } 

    }
}