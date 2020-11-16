using Microsoft.AspNetCore.SignalR;

namespace MeetApi.MeetApi.Hubs
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MeetHub : Hub
    {
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