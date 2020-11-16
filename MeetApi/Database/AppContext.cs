using System.Diagnostics.CodeAnalysis;
using MeetApi.MeetApi.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace MeetApi.MeetApi.Database
{
    public class AppContext : DbContext
    {
        public AppContext([NotNull] DbContextOptions<AppContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Date> Dates { get; set; }
        public DbSet<UserRegister> UsersRegister { get; set; }
        public DbSet<User> Users { get; set; }
    }
}