using BookQuoteApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookQuoteApi.Services
{
    public class AuthService
    {
        private readonly List<User> _users = new();
        private readonly string _jwtKey = "ThisIsASecretKeyForJwtToken12345";

        public User Register(string username, string password)
        {
            var user = new User
            {
                Id = _users.Count + 1,
                Username = username,
                Password = password
            };

            _users.Add(user);

            return user;
        }

        public string? Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u =>
                u.Username == username &&
                u.Password == password);

            if (user == null)
            {
                return null;
            }

            return CreateToken(user);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
