using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Unsplash.Models;
using Unsplash.Models.Request;
using Unsplash.Models.ViewModels;
using Unsplash.Utils;

namespace Unsplash.Services
{
    public class UserService : IUserService
    {

        private readonly IConfiguration _config;

        public UserService(IConfiguration config)
        {
            _config = config;

        }
        public UserViewModel Auth(AuthRequest user)
        {
            UserViewModel userViewModel = new UserViewModel();
            using (var db = new UnsplashContext())
            {
                string sPassword = Encrypt.GetSHA256(user.Password);
                var dbUser = db.Users.Where(d => d.Email == user.Email &&
                                                 d.Password == sPassword).FirstOrDefault();

                if (dbUser == null) return null;

                userViewModel.Name = dbUser.Name;
                userViewModel.Token = GetToken(dbUser);
            }

            return userViewModel;
        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["JWT_SECRET"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
                    }

                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
