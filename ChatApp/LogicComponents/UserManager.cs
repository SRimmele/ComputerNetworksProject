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
        public bool registerUser(IHttpContextAccessor httpContextAccessor, string username)
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
                    IPAddress = UserManager.getIPAdress(httpContextAccessor)
                };
                _userRepository.Create(newUser);
            }
            return true;
        }
    }
}