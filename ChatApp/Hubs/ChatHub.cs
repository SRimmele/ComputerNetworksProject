using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR; 
using Microsoft.Extensions.Logging; 
using ChatApp.Models; 
using ChatApp.Repositories;

namespace ChatApp.Hubs{
    public class ChatHub : Hub{
        public const string hubPath = "/ChatHub"; 
        private readonly ILogger<ChatHub> _logger;
        private readonly ConnectedUserRepository _connectedUserRepository;

        //construct
        public ChatHub(ILogger<ChatHub> logger, ConnectedUserRepository connectedUserRepository){
            _logger = logger; 
            _connectedUserRepository = connectedUserRepository; 
        }
        
        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("User Connected with ConnectionId = {ConnectionId}", Context.ConnectionId);

            await Clients.All.SendAsync("sendMessage", "TestMessage", "TestUser");
            await base.OnConnectedAsync(); 
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation("User Disconnected with ConnectionId = {ConnectionId}", Context.ConnectionId);

            _connectedUserRepository.Delete(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(Message message){
            _logger.LogInformation($"{message}");
            await Clients.All.SendAsync("SendMessage", message);
        }

        public async Task OnNewConnectedUser(ConnectedUser connectedUser)
        {
            _logger.LogInformation("User connected with ConnectionId = {ConnectionId}, and IpAddress = {IpAddress}", Context.ConnectionId, connectedUser.IPAddress);
            _connectedUserRepository.Create(Context.ConnectionId, connectedUser);
        }
    }
}