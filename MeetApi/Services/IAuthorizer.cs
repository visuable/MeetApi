using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;

namespace MeetApi.Services
{
    public interface IAuthorizer
    {
        void Register(ViewUserRegister user);
        string Login(ViewUser user);
    }
}