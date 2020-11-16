using AutoMapper;
using MeetApi.MeetApi.Models.DatabaseModels;
using MeetApi.MeetApi.ViewModels;

namespace MeetApi.MeetApi.Profiles
{
    public class ViewMeetingToMeetingProfile : Profile
    {
        public ViewMeetingToMeetingProfile()
        {
            CreateMap<ViewMeeting, Meeting>().ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date))
                .ForMember(d => d.Issue, opt => opt.MapFrom(s => s.Issue))
                .ForMember(d => d.Person, opt => opt.MapFrom(s => s.Person));
        }
    }
}