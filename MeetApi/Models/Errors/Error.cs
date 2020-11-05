using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetApi.Models.Errors
{
    public class Error
    {
        public string Value { get; set; }
        public Error(string value)
        {
            Value = value;
        }
    }
}
