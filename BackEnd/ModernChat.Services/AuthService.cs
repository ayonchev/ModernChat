using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ModernChat.Domain.Entities;
using ModernChat.Models.InputModels.Auth;
using ModernChat.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ModernChat.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> Register(RegisterIM inputModel)
        {
            var user = new ApplicationUser
            {
                UserName = inputModel.Email,
                Email = inputModel.Email
            };

            var result = await this.userManager.CreateAsync(user, inputModel.Password);

            if(result.Succeeded)
            {
                string token = await GetToken(user);

                return token;
            }

            throw new Exception();
        }

        public async Task<string> Login(LoginIM inputModel)
        {
            var user = await userManager.FindByNameAsync(inputModel.Email);

            if (user == null)
            {
                throw new Exception("Incorrect email!");
            }

            bool isPasswordCorrect = await userManager.CheckPasswordAsync(user, inputModel.Password);

            if (!isPasswordCorrect)
            {
                throw new Exception("Incorrect password!");
            }
            string token = await GetToken(user);

            return token;
        }

        private async Task<string> GetToken(ApplicationUser user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ExtremelySecretKey!"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = await GetUserClaims(user);

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:5000",
                audience: "http://localhost:4200",
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        private async Task<List<Claim>> GetUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.UserName)
            };

            return claims;
        }
    }
}
