﻿using Datas.Models;
using Datas.Repositories;
using ExamApi.DTOs.UserDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IBaseRepository<User> _userRepository;

        public LoginController(IConfiguration configuration, IBaseRepository<User> userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserAuthorizeDTO userDto)
        {
            var user = (await _userRepository.GetAll()).FirstOrDefault(x => x.Login == userDto.Login);
            if (user != null && CorrectPassword(userDto.Password, user.PasswordHash, user.PasswordSalt))
            {

                var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("Login", user.Login.ToString()),
                    };

                if(user.Roles.Count > 0)
                {
                    claims.Add(new Claim(ClaimTypes.Role, user.Roles[0].Name));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return Unauthorized("Invalid credentials");
            }
        }


        private bool CorrectPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            HMACSHA512 hmac = new HMACSHA512(passwordSalt);
            byte[] passwordHash2 = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != passwordHash2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
