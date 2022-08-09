using AutoMapper;
using Datas.Repositories.Interfaces;
using ExamApi.DTOs.AuthorDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]

    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all authors and with educational materials
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _unitOfWork.AuthorsRepository.GetAll();
            if (authors == null)
            {
                return this.NotFound("There are no authors in context");
            }
            return Ok(_mapper.Map<IEnumerable<SimpleAuthorDTO>>(authors));
        }


        /// <summary>
        /// Update list of materials by given author
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorId"></param>
        /// <returns>Update list of materials</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "id": "",
        ///        "author": "",
        ///     }
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="400">If the item is null</response>
        [HttpPut("{id}/author/{authorId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> AddAuthorToEducationalMaterial(int id, int authorId)
        {
            var educationalMaterial = await _unitOfWork.EducationalMaterialRepository.GetById(id);
            if (educationalMaterial == null)
            {
                return this.NotFound("There is no educational material with this id");
            }
            var author = await _unitOfWork.AuthorsRepository.GetById(authorId);
            if (author == null)
            {
                return this.NotFound("There is no author with this id");
            }
            
            if (educationalMaterial.author != null)
            {
                educationalMaterial.author.EducationalMaterialsCounter -= 1;
                if (educationalMaterial.author.EducationalMaterialsCounter == 0)
                {
                    educationalMaterial.author.EducationalMaterialsCounter = 0;
                }
            }
            author.EducationalMaterials.Add(educationalMaterial);
            int amount = author.EducationalMaterials.Count;
            author.EducationalMaterialsCounter = amount;
            await _unitOfWork.AuthorsRepository.Update(author);
            return NoContent();
        }
    }
}
