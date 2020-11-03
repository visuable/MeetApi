using System.ComponentModel.DataAnnotations;

namespace MeetApi.Models.DatabaseModels
{
    public class User
    {
        [Key] public string Login { get; set; }

        public string Password { get; set; }
    }
}