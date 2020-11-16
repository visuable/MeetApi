using System.Threading.Tasks;
using MeetApi.MeetApi.Models.DatabaseModels;

namespace MeetApi.MeetApi.Services
{
    public interface IAuthorizer
    {
        Task<bool> Register(UserRegister user);
        Task<string> Login(User user);
    }
}