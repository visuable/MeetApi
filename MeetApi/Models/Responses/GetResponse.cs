using MeetApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetApi.Models.Responses
{
    public class GetResponse : Response
    {
        public List<ViewMeeting> Meetings { get; set; }
        public GetResponse(List<ViewMeeting> meetings) : base("Values")
        {
            Meetings = meetings;
        }
    }
}
