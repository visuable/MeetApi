using System.Collections.Generic;
using System.Threading.Tasks;
using MeetApi.MeetApi.Models;
using MeetApi.MeetApi.Models.DatabaseModels;

namespace MeetApi.MeetApi.Services
{
    public interface IDatabaseManager
    {
        Task<bool> AddAsync(Meeting meeting);
        Task<List<Meeting>> GetAsync(MeetingGetParams meetingGetParams);
    }
}