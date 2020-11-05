using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetApi.ViewModels
{
    public class ViewUserRegister : ViewUser
    {
        [Required]
        public ViewPerson Person { get; set; }
    }
}
