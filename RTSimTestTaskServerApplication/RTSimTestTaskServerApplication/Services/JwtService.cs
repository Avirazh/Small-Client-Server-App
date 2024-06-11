using Microsoft.IdentityModel.Tokens;
using RTSimTestTaskServerApplication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RTSimTestTaskServerApplication.Services
{
    public static class JwtService
    {
        private const int tokenExpirationMinutes = 5;
        private const string tokenSigningAlgorithm = SecurityAlgorithms.HmacSha256Signature;

        public static string CreateAccessToken(User user, string secretKey)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, GetProfileTypeTranslated(user))
                }),

                Expires = DateTime.UtcNow.AddMinutes(tokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), tokenSigningAlgorithm),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
        private static string GetProfileTypeTranslated(User user)
        {
            string userProfileType;

            bool isStudentStudent = user.ProfileType == "Студент";
            switch (user.ProfileType)
            {
                case "Студент":
                    userProfileType = "Student";
                    break;
                case "Преподаватель":
                    userProfileType = "Teacher";
                    break;
                case "Сотрудник":
                    userProfileType = "Employee";
                    break;
                case "Бот":
                    userProfileType = "Bot";
                    break;
                default: 
                    userProfileType = "Undefined";
                    break;
            }
            return userProfileType;
        }
    }
}
