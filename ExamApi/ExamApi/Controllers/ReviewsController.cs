using AutoMapper;
using Datas.Models;
using Datas.Repositories;
using Datas.Repositories.Interfaces;
using ExamApi.DTOs.ReviewDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
        public async Task<IActionResult> GetAllReviewsMaterial()
        {
            var educationalMaterials = await _unitOfWork.MaterialReviewRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SimpleReviewDTO>>(educationalMaterials));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]

        public async Task<IActionResult> GetReviewById(int id)
        {
            var educationalMaterial = await _unitOfWork.MaterialReviewRepository.GetById(id);
            if (educationalMaterial == null)
            {
                return this.NotFound("This educational material does not exist");
            }
            return Ok(_mapper.Map<SimpleReviewDTO>(educationalMaterial));
        }

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
