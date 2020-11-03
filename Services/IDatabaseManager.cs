using System.Collections.Generic;
using System.Threading.Tasks;
using MeetApi.Models;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;

namespace MeetApi.Services
{
    public interface IDatabaseManager
    {
        void Add(ViewMeeting meeting);
        Task<List<Meeting>> GetAsync(MeetingGetParams meetingGetParams);
    }
}