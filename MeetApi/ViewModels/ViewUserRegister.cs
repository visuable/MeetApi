using System.ComponentModel.DataAnnotations;

namespace MeetApi.MeetApi.ViewModels
{
    public class ViewUserRegister : ViewUser
    {
        [Required] public ViewPerson Person { get; set; }
    }
}