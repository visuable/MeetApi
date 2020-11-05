using AutoMapper;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;

namespace MeetApi.Profiles
{
    public class ViewDateToDateProfile : Profile
    {
        public ViewDateToDateProfile()
        {
            CreateMap<ViewDate, Date>().ForMember(d => d.StartingDate, opt => opt.MapFrom(s => s.StartingDate))
                .ForMember(d => d.Duration, opt => opt.MapFrom(s => s.Duration));
        }
    }
}