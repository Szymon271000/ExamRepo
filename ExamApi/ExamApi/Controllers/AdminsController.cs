using AutoMapper;
using Datas.Models;
using Datas.Repositories.Interfaces;
using ExamApi.DTOs;
using ExamApi.DTOs.AuthorDTO;
using ExamApi.DTOs.EducationalMaterialDTO;
using ExamApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]

    public class AdminsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new admin
        /// </summary>
        /// <returns>Add new admin</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "login": "string",
        ///         "password": ""stringstri"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(AdminCreateDto userDTO)
        {
            if (ModelState.IsValid)
            {
                var userToAdd = _mapper.Map<User>(userDTO);
                Hashing.CreatePassword(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userToAdd.PasswordSalt = passwordSalt;
                userToAdd.PasswordHash = passwordHash;
                int adminsRoleId = 1;
                var role = await _unitOfWork.RoleRepository.GetById(adminsRoleId);
                role.Users.Add(userToAdd);
                await _unitOfWork.UserRepository.Add(userToAdd);
                return Ok(_mapper.Map<SimpleAdminDTO>(userToAdd));
            }
            return this.BadRequest("The login must have at least 3 character and the password 10.");
        }


        /// <summary>
        /// Get all authors and with educational materials ordered by materials counter
        /// </summary>
        /// <returns>All authors in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "authorName": "",
        ///        "description": "",
        ///        "simpleEducationalMaterials":
        ///        {
        ///             "title":"",
        ///             "description":"",
        ///             "location":""
        ///          
        ///        }
        ///        "EducationalMaterialsCounter":""
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns all authors</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        public async Task<IActionResult> GetAllAuthorsOrderedByMaterialWritten()
        {
            var authors = await _unitOfWork.AuthorsRepository.GetAll();
            if (authors == null)
            {
                return this.NotFound("There are no authors in context");
            }
            var result = authors.OrderByDescending(x => x.EducationalMaterialsCounter).ToList();
            return Ok(_mapper.Map<IEnumerable<SimpleAuthorDTO>>(result));
        }
    }
}
