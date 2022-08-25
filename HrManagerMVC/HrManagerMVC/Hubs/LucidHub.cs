using HrManagerMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Hubs
{
    public class LucidHub:Hub
    {
        private readonly UserManager<AppUser> _userManager;

        public LucidHub(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }
        public override Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                AppUser user =   _userManager.FindByNameAsync(Context.User.Identity.Name).Result;
                user.ConnectionId = Context.ConnectionId;
                var result = _userManager.UpdateAsync(user).Result;
                Clients.All.SendAsync("showAsOnline", user.Id);
            }
             
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                AppUser user = _userManager.FindByNameAsync(Context.User.Identity.Name).Result;
                user.ConnectionId = null;
                user.ConnectedAt = DateTime.Now;
                var result = _userManager.UpdateAsync(user).Result;
                Clients.All.SendAsync("showAsOffline", user.Id);
            }
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendPrivateMessage(string id , string message)
        {
            AppUser user = _userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                if (user.ConnectionId !=null )
                {
                    await Clients.Client(user.ConnectionId).SendAsync("receivePrivateMessage",user.Id,user.FullName,message);
                }
            }
        }
    }
}
