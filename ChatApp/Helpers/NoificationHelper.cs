using System;
using System.Threading.Tasks;
using ChatApp.Models;
using Microsoft.JSInterop;

namespace ChatApp.Helpers
{
    public static class NotificationHelper{
        const string defaultResponse = "default"; 
        const string grantedResponse = "granted"; 
        const string deniedResponse = "denied"; 
        async public static Task<bool> RequestNotifPermission(IJSRuntime jSRuntime){
            var response = await jSRuntime.InvokeAsync<string>("RequestNotifPermission"); 
            switch(response){
                case defaultResponse:
                case deniedResponse:  
                    return false; 
                case grantedResponse: 
                    return true; 
                default: 
                    throw new Exception("Please choose an option."); 
            }
        }

        async public static Task Notify(string title, Notification options, IJSRuntime jSRuntime){
            if(await RequestNotifPermission(jSRuntime)){
                await jSRuntime.InvokeVoidAsync("notify", title, options); 
            }

        }
    }
    
}