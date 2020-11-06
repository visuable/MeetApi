using MeetApi.Models;
using MeetApi.Models.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetApi.Services
{
    public interface IDatabaseManager
    {
        Task<bool> AddAsync(Meeting meeting);
        Task<List<Meeting>> GetAsync(MeetingGetParams meetingGetParams);
    }
}