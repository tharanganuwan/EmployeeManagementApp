
using DomainLayer.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Helpers
{
    public class CreateJwtToken
    {
        

        public static string CreateJWT(User user)
        {
            var keyBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(keyBytes);
            var tokenHandler = new JwtSecurityTokenHandler();
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, user.role),
                new Claim(ClaimTypes.Name, user.userName),
                new Claim(ClaimTypes.Email,user.email)
            };

            //////////
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = signingCredentials,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            

            ////////
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}


/*var token = new JwtSecurityToken(
                     Constants.Audience,
                     Constants.Issuer,
                     claims,
                     notBefore: DateTime.Now,
                     expires: DateTime.Now.AddHours(1),
                     signingCredentials);
*/