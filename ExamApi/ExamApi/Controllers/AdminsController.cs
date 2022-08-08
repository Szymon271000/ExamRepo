using AutoMapper;
using Datas.Models;
using Datas.Repositories;
using ExamApi.DTOs;
using ExamApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http.Cors;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]

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
        public async Task<IActionResult> Create(AdminCreateDto userDTO)
        {
            if (ModelState.IsValid)
            {
                var userToAdd = _mapper.Map<User>(userDTO);
                Hashing.CreatePassword(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userToAdd.PasswordSalt = passwordSalt;
                userToAdd.PasswordHash = passwordHash;
                int adminsRoleId = 1;
                var role = await _roleRepository.GetById(adminsRoleId);
                role.Users.Add(userToAdd);
                await _userRepository.Add(userToAdd);
                return Ok(_mapper.Map<SimpleAdminDTO>(userToAdd));
            }
            return this.BadRequest("The login must have at least 3 character and the password 10.");
        }


    }
}
