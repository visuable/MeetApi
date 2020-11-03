using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using MeetApi.Database;
using MeetApi.Models;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MeetApi.Services
{
    public class JwtAuthorizer : IAuthorizer
    {
        private readonly UsersContext _context;

        public JwtAuthorizer(UsersContext context)
        {
            this._context = context;
        }

        public string Login(User user)
        {
            var dbUser = _context.UsersRegister
                .Include(x => x.Person)
                .FirstOrDefault(x => x.Login == user.Login);
            if (dbUser == null || dbUser.Password != user.Password) return "";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dbUser.Login),
                new Claim("FirstName", dbUser.Person.FirstName),
                new Claim("LastName", dbUser.Person.LastName),
                new Claim("Department", dbUser.Person.Department)
            };
            var token = new JwtSecurityToken(TokenSettings.Issuer, claims: claims, audience: TokenSettings.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(TokenSettings.Lifetime), signingCredentials:
                new SigningCredentials(TokenSettings.GetSymmetricKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public void Register(UserRegister user)
        {
            var dbUser = _context.Users.Find(user.Login);
            if (dbUser != null) return;
            _context.UsersRegister.Add(user);
            _context.SaveChanges();
        }
    }
}