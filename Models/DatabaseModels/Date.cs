using System;

namespace MeetApi.Models.DatabaseModels
{
    public class Date
    {
        public int DateId { get; set; }
        public DateTime StartingDate { get; set; }

        public TimeSpan Duration { get; set; }
    }
}