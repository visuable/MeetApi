using System.IO;
using System.Threading.Tasks;
using MeetApi.Models;
using MeetApi.Models.DatabaseModels;
using MeetApi.Services;
using MeetApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MeetApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthorizer _authorizer;

        public AccountController(IAuthorizer authorizer)
        {
            _authorizer = authorizer;
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async void Login()
        {
            var user = await ParseUserFromBody();
            SetToken(user);
        }

        private void SetToken(User user)
        {
            HttpContext.Response.Headers["Token"] = _authorizer.Login(user);
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async void Register()
        {
            _authorizer.Register(await ParseRegisterUserFromBody());
        }

        private async Task<User> ParseUserFromBody()
        {
            return JsonConvert.DeserializeObject<User>(
                await new StreamReader(HttpContext.Request.Body).ReadToEndAsync());
        }

        private async Task<UserRegister> ParseRegisterUserFromBody()
        {
            return JsonConvert.DeserializeObject<UserRegister>(
                await new StreamReader(HttpContext.Request.Body).ReadToEndAsync());
        }
    }
}