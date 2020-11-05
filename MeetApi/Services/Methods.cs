using MeetApi.Models;
using MeetApi.Models.Requests;
using MeetApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetApi.Services
{
    public class Methods
    {
        private IDatabaseManager _manager;
        public Methods(IDatabaseManager manager)
        {
            _manager = manager;
        }
        public async Task<IActionResult> Add(Request request)
        {
            var req = request as AddRequest;
            await _manager.AddAsync(req.Meeting);
            return new JsonResult(new Response("Added."));
        }
        public async Task<IActionResult> Get(Request request)
        {
            var req = request as GetRequest;
            var result = await _manager.GetAsync(req.Params);
            return new JsonResult(new GetResponse(result));
        }
    }
}
