using AutoMapper;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;

namespace MeetApi.Profiles
{
    public class ViewReasonToReasonProfile : Profile
    {
        public ViewReasonToReasonProfile()
        {
            CreateMap<ViewReason, Reason>().ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));
        }
    }
}