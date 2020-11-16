using AutoMapper;
using MeetApi.MeetApi.Models.DatabaseModels;
using MeetApi.MeetApi.ViewModels;

namespace MeetApi.MeetApi.Profiles
{
    public class ViewIssueToIssueProfile : Profile
    {
        public ViewIssueToIssueProfile()
        {
            CreateMap<ViewIssue, Issue>().ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));
        }
    }
}