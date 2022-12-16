﻿using CWProject.Models.Models;
using CWProject.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks.Dataflow;

namespace CWProject.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //json web token Method
        public string CreateJwt(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



        //Password Create and Verify methods
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
    /*
            string plainPass = "NiakvaParola";
            string hashedPass = BCrypt.Net.BCrypt.HashPassword(plainPass);
            bool doTheyMath = BCrypt.Net.BCrypt.Verify(plainPass, hashedPass);
            bool doTheyMath1 = BCrypt.Net.BCrypt.Verify(plainPass, "$2a$12$VfdIKpv4fLpyHXHl9ZeYv.VYn7gAR9aH9tdDHst2zGRU23Rx7LCLy");
            var t = 1;
    */
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);    //comparing the computedHash value byte by byte with the passwordHash
            }
        }
    }
}