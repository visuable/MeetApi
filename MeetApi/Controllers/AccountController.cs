using MeetApi.Models.DatabaseModels;
using MeetApi.Services;
using MeetApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

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
        public void Login([AllowNull][FromBody] ViewUser user)
        {
            SetToken(user);
        }

        private void SetToken(ViewUser user)
        {
            HttpContext.Response.Headers["Authorzation"] = "Bearer " + _authorizer.Login(user);
        }

        [HttpPost]
        [Route(nameof(Register))]
        public void Register([AllowNull][FromBody]ViewUserRegister user)
        {
            _authorizer.Register(user);
        }

    }
}