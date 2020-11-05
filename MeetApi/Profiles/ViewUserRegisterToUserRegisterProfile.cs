using AutoMapper;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;

namespace MeetApi.Profiles
{
    public class ViewUserRegisterToUserRegisterProfile : Profile
    {
        public ViewUserRegisterToUserRegisterProfile()
        {
            CreateMap<ViewUserRegister, UserRegister>()
                .ForMember(d => d.Login, opt => opt.MapFrom(s => s.Login))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
                .ForMember(d => d.Person, opt => opt.MapFrom(s => s.Person));
        }
    }
}
