
using AutoMapper;
using MeetApi.Models;
using MeetApi.Models.ApiRequests;
using MeetApi.Models.ApiResponses;
using MeetApi.Models.DatabaseModels;
using MeetApi.Services;
using MeetApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetApi.Controllers
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MeetingController : Controller
    {
        private IDatabaseManager _manager;
        private IMapper _mapper;
        public MeetingController(IDatabaseManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        [HttpPost]
        [Route(nameof(Add))]
        [ProducesResponseType(typeof(JsonApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(JsonApiRequest<ViewMeeting> request)
        {
            var result = await _manager.AddAsync(_mapper.Map<Meeting>(request.RequestParams));
            return Ok(new JsonApiResponse<bool>()
            {
                Errors = null,
                Response = result
            });
        }
        [HttpGet]
        [Route(nameof(Get))]
        [ProducesResponseType(typeof(JsonApiResponse<List<Meeting>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(JsonApiRequest<MeetingGetParams> request)
        {
            var result = await _manager.GetAsync(request.RequestParams);
            return Ok(new JsonApiResponse<List<Meeting>>()
            {
                Errors = null,
                Response = result
            });
        }
    }
}