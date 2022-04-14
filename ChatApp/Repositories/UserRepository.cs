using ChatApp.Models; 
using System.Collections.Generic; 
using System; 

namespace ChatApp.Repositories{
    class UserRepository{
        private static Dictionary<string, User> users = new Dictionary<string, User>(); 
        public static event EventHandler Change; 

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
            Change?.Invoke(this, null); 
        }
    }
}