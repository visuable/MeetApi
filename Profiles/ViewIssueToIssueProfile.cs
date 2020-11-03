using AutoMapper;
using MeetApi.Models;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;

namespace MeetApi.Profiles
{
    public class ViewIssueToIssueProfile : Profile
    {
        public ViewIssueToIssueProfile()
        {
            CreateMap<ViewIssue, Issue>().ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));
        }
    }
}