using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MeetApi.Models;
using MeetApi.Services;
using MeetApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MeetApi.Controllers
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MeetingController : Controller
    {
        private readonly IDatabaseManager _manager;

        public MeetingController(IDatabaseManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route(nameof(Add))]
        public async void Add()
        {
            _manager.Add(await ParseObject());
        }

        [HttpGet]
        [Route(nameof(Get))]
        public async Task<IActionResult> Get([AllowNull] MeetingGetParams meetingGetParams)
        {
            return Json(await _manager.GetAsync(meetingGetParams));
        }

        private async Task<ViewMeeting> ParseObject()
        {
            var settings = new JsonSerializerSettings();
            var body = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var obj = JsonConvert.DeserializeObject<ViewMeeting>(body, settings);
            ConfigureWithClaims(obj);
            return obj;
        }

        private void ConfigureWithClaims(ViewMeeting obj)
        {
            obj.Person.Department = User.Claims
                .FirstOrDefault(x => x.Type == "Department")
                ?.Value;
            obj.Person.FirstName = User.Claims
                .FirstOrDefault(x => x.Type == "FirstName")
                ?.Value;
            obj.Person.LastName = User.Claims
                .FirstOrDefault(x => x.Type == "LastName")
                ?.Value;
        }
    }
}