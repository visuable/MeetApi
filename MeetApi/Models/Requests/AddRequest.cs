using MeetApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetApi.Models.Requests
{
    public class AddRequest : Request
    {
        public ViewMeeting Meeting { get; set; }
        public AddRequest() : base("Add")
        {

        }
    }
}
