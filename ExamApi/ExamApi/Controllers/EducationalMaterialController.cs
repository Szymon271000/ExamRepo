using AutoMapper;
using Datas.Models;
using Datas.Repositories.Interfaces;
using ExamApi.DTOs.EducationalMaterialDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]

    public class EducationalMaterialController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EducationalMaterialController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all educational materials
        /// </summary>
        /// <returns>All educational materials in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "title": "",
        ///        "description": "",
        ///        "location":""
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns all educational materials</response>
        /// <response code="404">If the item is null</response>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]

        public async Task<IActionResult> GetAllEducationalMaterial()
        {
            var educationalMaterials = await _unitOfWork.EducationalMaterialRepository.GetAll();
            if (educationalMaterials == null)
            {
                return this.NotFound("There is no educational materials in context");
            }
            return Ok(_mapper.Map<IEnumerable<SimpleEducationalMaterial>>(educationalMaterials));
        }


        /// <summary>
        /// Get educational material by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Educational material in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "title": "",
        ///        "description": "",
        ///        "location":""
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns educational material with specific ID</response>
        /// <response code="404">If the item is null</response>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]

        public async Task<IActionResult> GetEducationalMaterialById(int id)
        {
            var educationalMaterial = await _unitOfWork.EducationalMaterialRepository.GetById(id);
            if (educationalMaterial == null)
            {
                return this.NotFound("This educational material does not exist");
            }
            return Ok(_mapper.Map<SimpleEducationalMaterial>(educationalMaterial));
        }


        /// <summary>
        /// Add new educational material
        /// </summary>
        /// <returns>Add new educational material</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///       "title": "",
        ///       "description": "",
        ///       "location":""
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]

        public async Task<IActionResult> CreateEducationalMateria(EducationalMaterialToCreateDTO eduDTO)
        {
            if (ModelState.IsValid)
            {
                var educationalMaterialToAdd = _mapper.Map<EducationalMaterial>(eduDTO);
                await _unitOfWork.EducationalMaterialRepository.Add(educationalMaterialToAdd);
                return Ok(_mapper.Map<SimpleEducationalMaterial>(educationalMaterialToAdd));
            }
            return this.BadRequest("Every field is required and must have min 3 characters and max 20");
        }

        /// <summary>
        /// Update educational material
        /// </summary>
        /// <returns>Update educational material</returns>
        /// <param name="id"></param>
        /// <param name="eduDTO"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///       "title": "",
        ///       "description": "",
        ///       "location":""
        ///     }
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="400">If the item is null</response>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]

        public async Task<IActionResult> UpdateEducationalMaterial(int id, EducationalMaterialToUpdateDTO eduDTO)
        {
            var educationalMaterialToUpdate = await _unitOfWork.EducationalMaterialRepository.GetById(id);
            if (educationalMaterialToUpdate == null)
            {
                return this.NotFound("This material does not exist");
            }
            _mapper.Map(eduDTO, educationalMaterialToUpdate);
            await _unitOfWork.EducationalMaterialRepository.Update(educationalMaterialToUpdate);
            return NoContent();
        }

        /// <summary>
        /// Delete educational material
        /// </summary>
        /// <returns>Delete educational material</returns>
        /// <param name="id"></param>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _unitOfWork.EducationalMaterialRepository.GetById(id);
            if (result == null) return this.NotFound("There is noe educational material with this id");

            var MaterialswithResult = await _unitOfWork.MaterialReviewRepository.GetAll();
            foreach (var item in MaterialswithResult)
            {
                if (item.educationalMaterialId == result.EducationalMaterialId)
                {
                    item.educationalMaterialId = null;
                }
            }
            await _unitOfWork.EducationalMaterialRepository.Delete(result);
            return NoContent();
        }

        /// <summary>
        /// Get all educational materials with this TypeId
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns>Educational material in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "title": "",
        ///        "description": "",
        ///        "location":""
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns educational material with this TypeId</response>
        /// <response code="404">If the item is null</response>
        [HttpGet("EducationalElementsByMaterialTypeId/{TypeId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]

        public async Task<IActionResult> GetAllEducationalMaterialByType(int TypeId)
        {
            var educationalMaterials = await _unitOfWork.EducationalMaterialRepository.GetAll();
            var filtredList = educationalMaterials.Where(x => x.materialTypeId == TypeId).ToList();
            if (filtredList == null)
            {
                return this.NotFound("There is no educational material with this type");
            }
            return Ok(_mapper.Map<IEnumerable<SimpleEducationalMaterial>>(filtredList));
        }

        /// <summary>
        /// Get all educational materials with this author
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns>Educational material in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "title": "",
        ///        "description": "",
        ///        "location":""
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns educational material with this authorId</response>
        /// <response code="404">If the item is null</response>
        [HttpGet("EducationalElementsForGivenAuthor/{authorId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]

        public async Task<IActionResult> GetAllMaterialsForGivenAuthor(int authorId)
        {
            var educationalMaterials = await _unitOfWork.EducationalMaterialRepository.GetAll();
            var filtredList = educationalMaterials.Where(x => x.authorId == authorId).ToList();
            return Ok(_mapper.Map<IEnumerable<SimpleEducationalMaterial>>(filtredList));
        }

        /// <summary>
        /// Update reviews list of specific material
        /// </summary>
        /// <returns>Update reviews list</returns>
        /// <param name="id"></param>
        /// <param name="reviewId"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///       "id": "",
        ///       "reviewId": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="400">If the item is null</response>
        [HttpPut("{id}/review/{reviewId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
        public async Task<IActionResult> AddReviewToEducationalMaterial(int id, int reviewId)
        {
            var educationalMaterial = await _unitOfWork.EducationalMaterialRepository.GetById(id);
            if (educationalMaterial == null)
            {
                return NotFound();
            }
            var review = await _unitOfWork.MaterialReviewRepository.GetById(reviewId);
            if (review == null)
            {
                return NotFound();
            }
            educationalMaterial.Reviews.Add(review);
            await _unitOfWork.EducationalMaterialRepository.Update(educationalMaterial);
            return NoContent();
        }

    }
}
