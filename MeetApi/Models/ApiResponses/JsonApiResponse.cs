using System.Collections.Generic;
using MeetApi.MeetApi.Models.ApiErrors;

namespace MeetApi.MeetApi.Models.ApiResponses
{
    public class JsonApiResponse<TResult>
    {
        public IEnumerable<JsonApiError> Errors { get; set; }
        public TResult Response { get; set; }
    }
}