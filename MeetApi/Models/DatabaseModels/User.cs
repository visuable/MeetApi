using System.ComponentModel.DataAnnotations;

namespace MeetApi.MeetApi.Models.DatabaseModels
{
    public class User
    {
        [Key] public string Login { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }
    }
}