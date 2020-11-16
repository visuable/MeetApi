using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MeetApi.MeetApi.Hubs;
using MeetApi.MeetApi.Models;
using MeetApi.MeetApi.Models.ApiRequests;
using MeetApi.MeetApi.Models.ApiResponses;
using MeetApi.MeetApi.Models.DatabaseModels;
using MeetApi.MeetApi.Services;
using MeetApi.MeetApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MeetApi.MeetApi.Controllers
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MeetingController : Controller
    {
        private readonly IHubContext<MeetHub> _context;
        private readonly IDatabaseManager _manager;
        private readonly IMapper _mapper;

        public MeetingController(IDatabaseManager manager, IMapper mapper, IHubContext<MeetHub> context)
        {
            _manager = manager;
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        ///     Добавляет встречу в общий список.
        /// </summary>
        /// <param name="request">Request -- сущность.</param>
        /// <returns>True, в случае успешного добавления, False, если произошли неполадки.</returns>
        /// <response code="200">Объект внесен в общий список.</response>
        /// <response code="400">Некорректные данные.</response>
        [HttpPost]
        [Route(nameof(Add))]
        //[ProducesResponseType(typeof(JsonApiResponse<bool>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(JsonApiResponse<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] JsonApiRequest<ViewMeeting> request)
        {
            var result = await _manager.AddAsync(_mapper.Map<Meeting>(request.RequestParams));
            var model = new JsonApiResponse<bool>
            {
                Errors = null,
                Response = result
            };
            if (result)
            {
                await _context.Clients.All.SendAsync("AddResponse", true);
                return Ok(model);
            }

            await _context.Clients.All.SendAsync("AddResponse", false);
            return BadRequest(model);
        }

        /// <summary>
        ///     Возвращает список встреч по опциональным параметрам.
        /// </summary>
        /// <param name="request">Request -- сущность.</param>
        /// <returns>Список ViewMeeting, в случае успешного добавления, null, если произошли неполадки.</returns>
        /// <response code="200">Успешно.</response>
        [HttpPost]
        [Route(nameof(Get))]
        //[ProducesResponseType(typeof(JsonApiResponse<List<ViewMeeting>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromBody] [AllowNull] JsonApiRequest<MeetingGetParams> request)
        {
            var result = await _manager.GetAsync(request.RequestParams);
            var model = new JsonApiResponse<List<ViewMeeting>>
            {
                Errors = null,
                Response = result.Select(x => _mapper.Map<ViewMeeting>(x)).ToList()
            };
            await _context.Clients.All.SendAsync("GetResponse", model.Response);
            return Ok(model);
        }
    }
}