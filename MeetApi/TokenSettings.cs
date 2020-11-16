using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MeetApi.MeetApi
{
    public static class TokenSettings
    {
        public const string Issuer = "This";
        public const string Audience = "App";
        public const int Lifetime = 10;

        public const string SecretKey = "1234567890!$%^&*+qazcxvbeadsop";

        public static SymmetricSecurityKey GetSymmetricKey()
        {
            return new SymmetricSecurityKey(Encoding.Unicode.GetBytes(SecretKey));
        }
    }
}