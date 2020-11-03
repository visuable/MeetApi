using MeetApi.Models;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;

namespace MeetApi.Services
{
    public interface IAuthorizer
    {
        void Register(UserRegister user);
        string Login(User user);
    }
}