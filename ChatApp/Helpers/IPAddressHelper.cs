using System.Net;

namespace ChatApp.Helpers
{
    public static class IPAddressHelper
    {
        public static string ToString(IPAddress ipAddress)
        {
            if (ipAddress?.IsIPv4MappedToIPv6 == true)
            {
                return ipAddress.MapToIPv4().ToString();
            }

            return ipAddress?.ToString();
        }
    }
}