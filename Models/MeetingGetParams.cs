using System;

namespace MeetApi.Models
{
    public class MeetingGetParams
    {
        public DateTime StartingDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Type { get; set; }
        public string IssueDescription { get; set; }
        public string ReasonDescription { get; set; }
    }
}