namespace MeetApi.MeetApi.Models.DatabaseModels
{
    public class Meeting
    {
        public int Id { get; set; }
        public Date Date { get; set; }
        public Person Person { get; set; }
        public Issue Issue { get; set; }
        public Reason Reason { get; set; }
    }
}