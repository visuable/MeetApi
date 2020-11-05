using System.ComponentModel.DataAnnotations;

namespace MeetApi.ViewModels
{
    public class ViewUserRegister : ViewUser
    {
        [Required]
        public ViewPerson Person { get; set; }
    }
}
