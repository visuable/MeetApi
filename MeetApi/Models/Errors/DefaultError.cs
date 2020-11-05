using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetApi.Models.Errors
{
    public class DefaultError : Error
    {
        public DefaultError() : base("Error")
        {

        }
    }
}
