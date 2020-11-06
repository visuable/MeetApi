using AutoMapper;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetApi.Profiles
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
