using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace MeetApi.Helpers
{
    public static class Encrypts
    {
        public static string EncryptPassword(string pass, string salt)

        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(

                password: pass,

                salt: Convert.FromBase64String(salt),

                prf: KeyDerivationPrf.HMACSHA1,

                iterationCount: 10000,

                numBytesRequested: 256 / 8));

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
