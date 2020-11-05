using MeetApi.Models;
using MeetApi.Services;
using MeetApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

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
        public async Task Add([AllowNull][FromBody] ViewMeeting meeting)
        {
            await _manager.Add(meeting);
        }

        [HttpGet]
        [Route(nameof(Get))]
        public async Task<IActionResult> Get([AllowNull] MeetingGetParams meetingGetParams)
        {
            return Json(await _manager.GetAsync(meetingGetParams));
        }
    }
}