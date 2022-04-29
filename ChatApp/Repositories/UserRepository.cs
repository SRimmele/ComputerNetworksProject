using ChatApp.Models; 
using System.Collections.Generic; 
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace ChatApp.Repositories{
    public class UserRepository{
        private readonly ILogger<UserRepository> _logger;
        public static Dictionary<string, User> users = new Dictionary<string, User>(); 
        public static event EventHandler Change;

        public UserRepository(ILogger<UserRepository> logger)
        {
            _logger = logger;
        }

        public User Read(string ipAddress){
            if(users.ContainsKey(ipAddress)){
                return users[ipAddress]; 
            } 
            return null; 
        }

        public IEnumerable<User> ReadAll(){
            return users.Values; 
        }

        public void Create(User user){
            users[user.IPAddress] = user; 
            _logger.LogInformation("User repository: {UserRepository}", JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true }));
            Change?.Invoke(this, null); 
        }
    }
}