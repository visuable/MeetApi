using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetApi.Models
{
    public class Request
    {
        public string MethodName { get; set; }
        public Request(string name)
        {
            MethodName = name;
        }
    }
}
