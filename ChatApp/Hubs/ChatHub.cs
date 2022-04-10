using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR; 
using Microsoft.Extensions.Logging; 

namespace ChatApp.Hubs{
    class ChatHub : Hub{
        public const string hubPath = "/ChatHub"; 
        public const string sendPath = "/Send"; 
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

        public async Task SendMessage(Message message){
            _logger.LogInformation($"{message}");
            await Clients.All.SendAsync("SendMessage", message);
        } 
    }

    public class Message{
        public string userName{get; set;}
        public string messageText{get; set;}
    }
}