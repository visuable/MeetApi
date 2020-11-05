using AutoMapper;
using MeetApi.Models;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;
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
        private readonly IMapper _mapper;
        private readonly Database.AppContext _context;

        public LocalDbDatabaseManager(Database.AppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(ViewMeeting meeting)
        {
            var meet = _mapper.Map<Meeting>(meeting);
            var x = _context.Meetings.FirstOrDefault(x => x.Date.StartingDate
                .Equals(meet.Date.StartingDate));

            if (x != null)
            {
                // Ищем ближайшую дату.
                var nearDate = _context.Dates.FirstOrDefault(x => x.StartingDate < meet.Date.StartingDate);
                if (meet.Date.StartingDate + meet.Date.Duration > nearDate.StartingDate)
                    meet.Date.StartingDate = nearDate.StartingDate + nearDate.Duration;
            }
            await _context.AddAsync(meet);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<List<ViewMeeting>> GetAsync([AllowNull] MeetingGetParams meetingGetParams)
        {
            var list = await FullList(meetingGetParams);
            if (list != null) return list.Select(x => _mapper.Map<ViewMeeting>(x)).ToList();
            return (await OptionList(meetingGetParams)).Select(x => _mapper.Map<ViewMeeting>(x)).ToList();
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
            if (meetingGetParams.Department == null &&
                meetingGetParams.Duration == TimeSpan.Zero &&
                meetingGetParams.FirstName == null &&
                meetingGetParams.IssueDescription == null &&
                meetingGetParams.LastName == null &&
                meetingGetParams.ReasonDescription == null &&
                meetingGetParams.Type == null) return await RenderList();
            return null;
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