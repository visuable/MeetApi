using MeetApi.Models;
using MeetApi.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MeetApi.Services
{
    public class LocalDbDatabaseManager : IDatabaseManager
    {
        private readonly Database.AppContext _context;

        public LocalDbDatabaseManager(Database.AppContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Meeting meeting)
        {
            var x = _context.Meetings.FirstOrDefault(x => x.Date.StartingDate
                .Equals(meeting.Date.StartingDate));

            if (x != null)
            {
                // Ищем ближайшую дату.
                var nearDate = _context.Dates.FirstOrDefault(x => x.StartingDate < meeting.Date.StartingDate);
                if (meeting.Date.StartingDate + meeting.Date.Duration > nearDate.StartingDate)
                    meeting.Date.StartingDate = nearDate.StartingDate + nearDate.Duration;
            }
            await _context.AddAsync(meeting);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Meeting>> GetAsync([AllowNull] MeetingGetParams meetingGetParams)
        {
            if (meetingGetParams == null)
            {
                var list = await FullList(meetingGetParams);
                if (list != null) return list;
            }
            return await OptionList(meetingGetParams);
        }

        private async Task<List<Meeting>> OptionList(MeetingGetParams meetingGetParams)
        {
            return await _context.Meetings
                .Where(x =>
                    x.Date.StartingDate == meetingGetParams.StartingDate ||
                    x.Date.Duration == meetingGetParams.Duration ||
                    x.Issue.Description == meetingGetParams.IssueDescription ||
                    x.Issue.Type == meetingGetParams.Type ||
                    x.Person.FirstName == meetingGetParams.FirstName ||
                    x.Person.LastName == meetingGetParams.LastName ||
                    x.Person.Department == meetingGetParams.Department)
                .OrderBy(x => x.Date.StartingDate)
                .Include(x => x.Date)
                .Include(x => x.Issue)
                .Include(x => x.Person).ToListAsync();
        }

        private async Task<List<Meeting>> FullList(MeetingGetParams meetingGetParams)
        {
            if (meetingGetParams == null || meetingGetParams.Department == null &&
                meetingGetParams.Duration == TimeSpan.Zero &&
                meetingGetParams.FirstName == null &&
                meetingGetParams.IssueDescription == null &&
                meetingGetParams.LastName == null &&
                meetingGetParams.ReasonDescription == null &&
                meetingGetParams.Type == null) return await RenderList();

            return await OptionList(meetingGetParams);
        }

        private async Task<List<Meeting>> RenderList()
        {
            return await _context.Meetings
                .OrderBy(x => x.Date.StartingDate)
                .Include(x => x.Date)
                .Include(x => x.Issue)
                .Include(x => x.Person).ToListAsync();
        }
    }
}