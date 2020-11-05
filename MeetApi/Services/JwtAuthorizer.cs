using MeetApi.Helpers;
using MeetApi.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AppContext = MeetApi.Database.AppContext;

namespace MeetApi.Services
{
    public class JwtAuthorizer : IAuthorizer
    {
        private readonly AppContext _context;

        public JwtAuthorizer(Database.AppContext context)
        {
            _context = context;
        }

        public async Task<string> Login(User user)
        {
            if (!await ValidUser(user)) return string.Empty;
            var token = new JwtSecurityToken(TokenSettings.Issuer, audience: TokenSettings.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(TokenSettings.Lifetime), signingCredentials:
                new SigningCredentials(TokenSettings.GetSymmetricKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<bool> Register(UserRegister user)
        {
            var dbUser = _context.Users.Find(user.Login);
            if (dbUser != null) return false;
            var salt = Encrypts.GenerateSalt();
            user.Password = Encrypts.EncryptPassword(user.Password, salt);
            user.Salt = salt;
            await _context.UsersRegister.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
        private async Task<bool> ValidUser(User user)
        {
            var dbUser = await _context.UsersRegister
                .Include(x => x.Person)
                .FirstOrDefaultAsync(x => x.Login == user.Login);
            if (dbUser == null) return false;
            var encryptedPasswordUser = Encrypts.EncryptPassword(user.Password, dbUser.Salt);
            if (!string.Equals(dbUser.Password, encryptedPasswordUser)) return false;
            return true;
        }
    }
}