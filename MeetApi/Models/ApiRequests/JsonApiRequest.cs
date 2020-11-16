namespace MeetApi.MeetApi.Models.ApiRequests
{
    public class JsonApiRequest<TParams>
    {
        public TParams RequestParams { get; set; }
    }
}