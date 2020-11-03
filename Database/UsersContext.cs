using System.Diagnostics.CodeAnalysis;
using MeetApi.Models;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MeetApi.Database
{
    public sealed class UsersContext : DbContext
    {
        public UsersContext([NotNull] DbContextOptions<UsersContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserRegister> UsersRegister { get; set; }
        public DbSet<User> Users { get; set; }
    }
}