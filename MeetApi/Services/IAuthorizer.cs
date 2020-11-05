using MeetApi.Models.DatabaseModels;
using System.Threading.Tasks;

namespace MeetApi.Services
{
    public interface IAuthorizer
    {
        Task<bool> Register(UserRegister user);
        Task<string> Login(User user);
    }
}