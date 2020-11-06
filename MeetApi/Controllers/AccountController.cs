using AutoMapper;
using MeetApi.Models.ApiResponses;
using MeetApi.Models.DatabaseModels;
using MeetApi.Services;
using MeetApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
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
        /// <summary>
        /// Авторизация по логину и паролю.
        /// </summary>
        /// <param name="user">User -- структура: login, password</param>
        /// <returns>True, в случае успешной авторизации, False, если авторизация не прошла.</returns>
        /// <response code="200">Если авторизация прошла успешно.</response>
        /// <response code="400">Скорее всего, введенные данные неправильны.</response>
        [HttpPost]
        [Route(nameof(Login))]
        [ProducesResponseType(typeof(JsonApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonApiResponse<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([AllowNull][FromBody] ViewUser user)
        {
            var loginResult = await ValidateAndLogin(user);
            var model = new JsonApiResponse<bool>()
            {
                Response = loginResult
            };
            if (loginResult)
            {
                return Ok(model);
            }
            return Unauthorized(model);
        }

        /// <summary>
        /// Регистрация по логину, паролю и Person.
        /// </summary>
        /// <param name="user">User -- структура: login, password, person</param>
        /// <returns>True, в случае успешной регистрации, False, если регистрация не прошла.</returns>
        /// <remarks>
        /// Важно! Person в запросе не может быть null.
        /// </remarks>
        /// <response code="200">Если регистрация прошла успешно.</response>
        /// <response code="400">Скорее всего, аккаунт уже зарегестрирован.</response>
        [HttpPost]
        [Route(nameof(Register))]
        [ProducesResponseType(typeof(JsonApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([AllowNull][FromBody] ViewUserRegister user)
        {
            var dbUser = _mapper.Map<UserRegister>(user);
            var registerStatus = await _authorizer.Register(dbUser);
            var model = new JsonApiResponse<bool>()
            {
                Response = registerStatus
            };
            if (registerStatus)
            {
                return Ok(model);
            }
            return BadRequest(model);
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

    }
}