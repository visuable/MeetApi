using MeetApi.Models.ApiErrors;
using System.Collections.Generic;

namespace MeetApi.Models.ApiResponses
{
    public class JsonApiResponse<TResult>
    {
        public IEnumerable<JsonApiError> Errors { get; set; }
        public TResult Response { get; set; }
    }
}
