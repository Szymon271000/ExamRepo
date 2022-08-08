using AutoMapper;
using Datas.Models;
using Datas.Repositories;
using ExamApi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public AdminsController(IBaseRepository<User> userRepository, IMapper mapper, IBaseRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto userDTO)
        {
            if (ModelState.IsValid)
            {
                var userToAdd = _mapper.Map<User>(userDTO);
                CreatePassword(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userToAdd.PasswordSalt = passwordSalt;
                userToAdd.PasswordHash = passwordHash;
                var role = await _roleRepository.GetById(1);
                role.Users.Add(userToAdd);
                await _userRepository.Add(userToAdd);
                return Ok(_mapper.Map<SimpleUserDTO>(userToAdd));
            }
            return this.BadRequest("The login must have at least 3 character and the password 10.");
        }

        private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            HMACSHA512 hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
