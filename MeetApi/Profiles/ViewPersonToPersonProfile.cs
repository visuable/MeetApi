using AutoMapper;
using MeetApi.MeetApi.Models.DatabaseModels;
using MeetApi.MeetApi.ViewModels;

namespace MeetApi.MeetApi.Profiles
{
    public class ViewPersonToPersonProfile : Profile
    {
        public ViewPersonToPersonProfile()
        {
            CreateMap<ViewPerson, Person>().ForMember(d => d.Department, opt => opt.MapFrom(s => s.Department))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName));
        }
    }
}