using AutoMapper;
using Datas.Models;
using Datas.Repositories.Interfaces;
using ExamApi.DTOs.ReviewDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]

    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ReviewsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all reviews with educational material
        /// </summary>
        /// <returns>All reviews with educational materials in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "materialReviewId": "",
        ///        "educationalMaterial": "",
        ///        {
        ///        "title": "",
        ///        "description": "",
        ///        "location": ""
        ///        }
        ///        "materialReviewDescription":"",
        ///        "materialReviewDigit":""
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns all reviews with educational materials</response>
        /// <response code="404">If the item is null</response>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
        public async Task<IActionResult> GetAllReviewsMaterial()
        {
            var educationalMaterials = await _unitOfWork.MaterialReviewRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SimpleReviewDTO>>(educationalMaterials));
        }

        /// <summary>
        /// Get review with educational material by specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Review with educational materials in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "materialReviewId": "",
        ///        "educationalMaterial": "",
        ///        {
        ///        "title": "",
        ///        "description": "",
        ///        "location": ""
        ///        }
        ///        "materialReviewDescription":"",
        ///        "materialReviewDigit":""
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns review with educational materials</response>
        /// <response code="404">If the item is null</response>
        [HttpGet("{id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]

        public async Task<IActionResult> GetReviewById(int id)
        {
            var educationalMaterial = await _unitOfWork.MaterialReviewRepository.GetById(id);
            if (educationalMaterial == null)
            {
                return this.NotFound("This educational material does not exist");
            }
            return Ok(_mapper.Map<SimpleReviewDTO>(educationalMaterial));
        }

        /// <summary>
        /// Add new review
        /// </summary>
        /// <returns>Add new review</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///       "materialReviewDescription": "",
        ///       "materialReviewDigit": ""
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
        public async Task<IActionResult> CreateReview(ReviewToCreateDTO reviewDTO)
        {
            if (ModelState.IsValid)
            {
                var reviewToAdd = _mapper.Map<MaterialReview>(reviewDTO);
                await _unitOfWork.MaterialReviewRepository.Add(reviewToAdd);
                return Ok(_mapper.Map<SimpleReviewDTO>(reviewToAdd));

            }
            return this.BadRequest("MaterialReviewDescription field is required and must have min 3 characters and max 20 and MaterialReviewDigit must be between 0 and 10");
        }

        /// <summary>
        /// Update review description
        /// </summary>
        /// <returns>Update review description</returns>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///       "op": "replace",
        ///       "path": "MaterialReviewDescription",
        ///       "value":"New Description"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="400">If the item is null</response>
        [HttpPatch("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
        public async Task<ActionResult> PartialEntityUpdate(int id, JsonPatchDocument<ReviewToUpdateDTO> patchDoc)
        {
            var modelFromRepo = await _unitOfWork.MaterialReviewRepository.GetById(id);
            if (modelFromRepo == null)
            {
                return this.NotFound("This review does not exist");
            }
            var entityToPatch = _mapper.Map<ReviewToUpdateDTO>(modelFromRepo);
            patchDoc.ApplyTo(entityToPatch, ModelState);
            if (!TryValidateModel(entityToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(entityToPatch, modelFromRepo);
            await _unitOfWork.MaterialReviewRepository.Update(modelFromRepo);
            return NoContent();
        }

        /// <summary>
        /// Delete review 
        /// </summary>
        /// <returns>Delete review</returns>
        /// <param name="id"></param>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _unitOfWork.MaterialReviewRepository.GetById(id);
            if (result == null) return NotFound();
            await _unitOfWork.MaterialReviewRepository.Delete(result);
            return NoContent();
        }
    }
}
