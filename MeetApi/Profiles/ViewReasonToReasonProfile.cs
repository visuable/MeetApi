using AutoMapper;
using MeetApi.MeetApi.Models.DatabaseModels;
using MeetApi.MeetApi.ViewModels;

namespace MeetApi.MeetApi.Profiles
{
    public class ViewReasonToReasonProfile : Profile
    {
        public ViewReasonToReasonProfile()
        {
            CreateMap<ViewReason, Reason>().ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));
        }
    }
}