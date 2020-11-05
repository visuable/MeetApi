using MeetApi.Models;
using MeetApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetApi.Services
{
    public interface IDatabaseManager
    {
        Task Add(ViewMeeting meeting);
        Task<List<ViewMeeting>> GetAsync(MeetingGetParams meetingGetParams);
    }
}