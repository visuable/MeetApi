using AutoMapper;
using MeetApi.Models.DatabaseModels;
using MeetApi.Services;
using MeetApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace MeetApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthorizer _authorizer;
        private readonly IMapper _mapper;

        public AccountController(IAuthorizer authorizer, IMapper mapper)
        {
            _authorizer = authorizer;
            _mapper = mapper;
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login([AllowNull][FromBody] ViewUser user)
        {
            var loginResult = await ValidateAndLogin(user);
            return Json(loginResult);
        }

        private async Task<bool> ValidateAndLogin(ViewUser user)
        {
            var dbUser = _mapper.Map<User>(user);
            var loginResult = await _authorizer.Login(dbUser);
            if (string.Equals(loginResult, string.Empty))
            {
                return false;
            }
            HttpContext.Response.Headers["Authorization"] = string.Join(' ', JwtBearerDefaults.AuthenticationScheme, loginResult);
            return true;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register([AllowNull][FromBody] ViewUserRegister user)
        {
            var dbUser = _mapper.Map<UserRegister>(user);
            var registerStatus = await _authorizer.Register(dbUser);
            return Json(registerStatus);
        }

    }
}