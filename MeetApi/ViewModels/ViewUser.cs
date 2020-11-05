using System.ComponentModel.DataAnnotations;

namespace MeetApi.ViewModels
{
    public class ViewUser
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
