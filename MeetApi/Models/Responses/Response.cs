using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetApi.Models.Responses
{
    public class Response
    {
        public string Value { get; set; }
        public Response(string value)
        {
            Value = value;
        }
    }
}
