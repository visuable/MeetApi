using AutoMapper;
using MeetApi.Models.DatabaseModels;
using MeetApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AppContext = MeetApi.Database.AppContext;

namespace MeetApi.Services
{
    public class JwtAuthorizer : IAuthorizer
    {
        private readonly AppContext _context;
        private readonly IMapper _mapper;

        public JwtAuthorizer(Database.AppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string Login(ViewUser user)
        {
            var dbUserModel = _mapper.Map<User>(user);
            var dbUser = _context.UsersRegister
                .Include(x => x.Person)
                .FirstOrDefault(x => x.Login == dbUserModel.Login);
            if (dbUser == null || dbUser.Password != dbUserModel.Password) return "";
            var token = new JwtSecurityToken(TokenSettings.Issuer, audience: TokenSettings.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(TokenSettings.Lifetime), signingCredentials:
                new SigningCredentials(TokenSettings.GetSymmetricKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public void Register(ViewUserRegister user)
        {
            var dbUserModel = _mapper.Map<UserRegister>(user);
            var dbUser = _context.Users.Find(user.Login);
            if (dbUser != null) return;
            _context.UsersRegister.Add(dbUserModel);
            _context.SaveChanges();
        }
    }
}