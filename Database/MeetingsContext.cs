using System.Diagnostics.CodeAnalysis;
using MeetApi.Models;
using MeetApi.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace MeetApi.Database
{
    public sealed class MeetingsContext : DbContext
    {
        public MeetingsContext([NotNull] DbContextOptions<MeetingsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Date> Dates { get; set; }
    }
}