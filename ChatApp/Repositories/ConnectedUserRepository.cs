using System.Net;
using System;
using System.Collections.Generic;
using ChatApp.Models;

namespace ChatApp.Repositories{
    public class ConnectedUserRepository{

        private static HashSet<ConnectedUser> connectedUsers = new HashSet<ConnectedUser>();
        public static event EventHandler Change; 
        public IEnumerable<ConnectedUser> ReadAll(){
            return connectedUsers; 
        }

        public void Create(ConnectedUser user){
            connectedUsers.Add(user); 
            Change?.Invoke(this, null); 
        }

        public void Delete(ConnectedUser user){
            connectedUsers.RemoveWhere(u => u.IPAddress == user.IPAddress); 
            Change?.Invoke(this, null);
        }
    }
}