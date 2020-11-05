using MeetApi.Models;
using MeetApi.Models.Errors;
using MeetApi.Models.Requests;
using MeetApi.Services;
using MeetApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MeetApi.Controllers
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MeetingController : Controller
    {
        private Methods methods;

        public MeetingController(IDatabaseManager manager, Methods methods)
        {
            this.methods = methods;
        }
        [HttpPost]
        [Route(nameof(Method))]
        public async Task<IActionResult> Method([FromBody]Request request)
        {
            switch (request.MethodName)
            {
                case "Add":
                    return await methods.Add(request);
                case "Get":
                    return await methods.Get(request);
            }
            return Json(new DefaultError());
        }
    }
}