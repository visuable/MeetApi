using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MeetApi.MeetApi.Helpers
{
    public static class Encrypts
    {
        public static string EncryptPassword(string pass, string salt)

        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                pass,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));

            return hashed;
        }

        public static string GenerateSalt()

        {
            var salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())

            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
    }
}