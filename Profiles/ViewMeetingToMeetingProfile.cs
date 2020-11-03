using AutoMapper;
using MeetApi.Models;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;

namespace MeetApi.Profiles
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