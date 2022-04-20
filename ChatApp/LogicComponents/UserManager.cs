using System.Linq;
using ChatApp.Models;
using ChatApp.Repositories;
using Microsoft.AspNetCore.Http;

namespace ChatApp.LogicComponents
{
    public class UserManager
    {
        private readonly UserRepository _userRepository; 
        public UserManager(UserRepository userRepository){
            _userRepository = userRepository; 
        }
        public static string getIPAdress(IHttpContextAccessor httpContextAccessor)
        {
            var remoteIpAddress = httpContextAccessor.HttpContext.Connection?.RemoteIpAddress;
            if (remoteIpAddress?.IsIPv4MappedToIPv6 == true)
            {
                return remoteIpAddress.MapToIPv4().ToString();
            }

            return remoteIpAddress?.ToString();
        }
        public bool registerUser(IHttpContextAccessor httpContextAccessor, string username, string color)
        {
            if (_userRepository.Read(UserManager.getIPAdress(httpContextAccessor)) == null)
            {
                if (_userRepository.ReadAll().Any(u => u.Username == username))
                {
                    return false;
                }
                var newUser = new User
                {
                    Username = username,
                    Color = color, 
                    IPAddress = UserManager.getIPAdress(httpContextAccessor)
                };
                _userRepository.Create(newUser);
            }
            return true;
        }

        public User readLoggedInUser(IHttpContextAccessor httpContextAccessor){
            var ipAddress = getIPAdress(httpContextAccessor);
            var loggedInUser = _userRepository.Read(ipAddress); 
            return loggedInUser;  
        }

    }
}