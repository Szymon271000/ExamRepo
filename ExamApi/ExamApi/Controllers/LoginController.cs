using Datas.Repositories.Interfaces;
using ExamApi.DTOs.UserDTO;
using ExamApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Cors;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;


        public LoginController(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Receive a JWT Token
        /// </summary>
        /// <returns>JWT Token</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///       "login": "",
        ///       "password": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        public async Task<IActionResult> Post(AdminAuthorizeDTO userDto)
        {
            var user = (await _unitOfWork.UserRepository.GetAll()).FirstOrDefault(x => x.Login == userDto.Login);
            if (user != null && Hashing.CorrectPassword(userDto.Password, user.PasswordHash, user.PasswordSalt))
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
    }
}
