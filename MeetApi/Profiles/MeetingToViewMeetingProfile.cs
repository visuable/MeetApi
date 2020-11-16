using AutoMapper;
using MeetApi.MeetApi.Models.DatabaseModels;
using MeetApi.MeetApi.ViewModels;

namespace MeetApi.MeetApi.Profiles
{
    public class MeetingToViewMeetingProfile : Profile
    {
        public MeetingToViewMeetingProfile()
        {
            CreateMap<Meeting, ViewMeeting>().ForMember(s => s.Date, opt => opt.MapFrom(d => d.Date))
                .ForMember(s => s.Issue, opt => opt.MapFrom(d => d.Issue))
                .ForMember(s => s.Person, opt => opt.MapFrom(d => d.Person))
                .ForMember(s => s.Reason, opt => opt.MapFrom(d => d.Reason));
        }
    }
}