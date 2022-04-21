using System.Net;
using System.Collections;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR; 
using Microsoft.Extensions.Logging; 
using ChatApp.Models; 
using ChatApp.Repositories;
using Microsoft.AspNetCore.Http.Features;
using ChatApp.Helpers;

namespace ChatApp.Hubs{
    class ChatHub : Hub{
        public const string hubPath = "/ChatHub"; 
        private readonly ILogger<ChatHub> _logger;
        private readonly ConnectedUserRepository _connectedUserRepository; 

        //construct
        public ChatHub(ILogger<ChatHub> logger, ConnectedUserRepository connectedUserRepository){
            _logger = logger; 
            _connectedUserRepository = connectedUserRepository; 
        }
        public async override Task OnConnectedAsync()
        {
            _logger.LogInformation("User Connected... "); 
            _connectedUserRepository.Create(new ConnectedUser{
                IPAddress = IPAddressHelper.ToString(Context.Features.Get<IHttpConnectionFeature>().RemoteIpAddress)
            }); 
            await Clients.All.SendAsync("sendMessage", "TestMessage", "TestUser");
            await base.OnConnectedAsync(); 
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation("User Disonnected... "); 
            _connectedUserRepository.Delete(new ConnectedUser{
                IPAddress = IPAddressHelper.ToString(Context.Features.Get<IHttpConnectionFeature>().RemoteIpAddress)
            }); 
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(Message message){
            _logger.LogInformation($"{message}");
            await Clients.All.SendAsync("SendMessage", message);
        } 
    }



}