using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MeetApi.MeetApi.Hubs
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MeetHub : Hub
    {
        public MeetHub()
        {
        }

        public async void Get(string response)
        {
            await Clients.Caller.SendAsync("GetResponse", response);
        }

        public async void Add(bool result)
        {
            await Clients.Others.SendAsync("AddResponse", result);
        }
    }
}
