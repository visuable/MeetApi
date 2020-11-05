using AutoMapper;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;

namespace MeetApi.Profiles
{
    public class ViewUserToUserProfile : Profile
    {
        public ViewUserToUserProfile()
        {
            CreateMap<ViewUser, User>()
                .ForMember(d => d.Login, opt => opt.MapFrom(s => s.Login))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password));
        }
    }
}
