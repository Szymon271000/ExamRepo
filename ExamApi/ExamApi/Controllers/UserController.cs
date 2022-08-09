using AutoMapper;
using Datas.Models;
using Datas.Repositories;
using Datas.Repositories.Interfaces;
using ExamApi.DTOs.UserDTO;
using ExamApi.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <returns>Add new user</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "login": "",
        ///         "password": ""
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var userToAdd = _mapper.Map<User>(userDTO);
                Hashing.CreatePassword(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userToAdd.PasswordSalt = passwordSalt;
                userToAdd.PasswordHash = passwordHash;
                int userRoleId = 2;
                var role = await _unitOfWork.RoleRepository.GetById(userRoleId);
                role.Users.Add(userToAdd);
                await _unitOfWork.UserRepository.Add(userToAdd);
                return Ok(_mapper.Map<SimpleUserDTO>(userToAdd));
            }
            return this.BadRequest("The login must have at least 3 character and the password 10.");
        }
    }
}
