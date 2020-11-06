using MeetApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetApi.Models.Requests
{
    public class GetRequest : Request
    {
        public MeetingGetParams Params { get; set; }
        public GetRequest() : base("Get")
        {

        }
    }
}