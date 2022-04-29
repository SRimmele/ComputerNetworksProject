using System.Net;
using System;
using System.Collections.Generic;
using System.Text.Json;
using ChatApp.Models;
using Microsoft.Extensions.Logging;

namespace ChatApp.Repositories{
    public class ConnectedUserRepository{
        private readonly ILogger<ConnectedUserRepository> _logger;

        private static readonly Dictionary<string, ConnectedUser> ConnectedUsers = new();
        public static event EventHandler Change;

        public ConnectedUserRepository(ILogger<ConnectedUserRepository> logger)
        {
            _logger = logger;
        }
        
        public IEnumerable<ConnectedUser> ReadAll(){
            return ConnectedUsers.Values; 
        }

        public void Create(string connectionId, ConnectedUser user)
        {
            ConnectedUsers[connectionId] = user;
            _logger.LogInformation("Connected users repository: {ConnectedUserRepository}", JsonSerializer.Serialize(ConnectedUsers, new JsonSerializerOptions { WriteIndented = true }));
            Change?.Invoke(this, null); 
        }

        public void Delete(string connectionId){
            if (ConnectedUsers.ContainsKey(connectionId))
            {
                ConnectedUsers.Remove(connectionId);
            }
            _logger.LogInformation("Connected users repository: {ConnectedUserRepository}", JsonSerializer.Serialize(ConnectedUsers, new JsonSerializerOptions { WriteIndented = true }));
            Change?.Invoke(this, null);
        }
    }
}