﻿using System.Net;

namespace MeetApi.MeetApi.Models.ApiErrors
{
    public class JsonApiError
    {
        public HttpStatusCode Code { get; set; }
        public string Description { get; set; }
    }
}