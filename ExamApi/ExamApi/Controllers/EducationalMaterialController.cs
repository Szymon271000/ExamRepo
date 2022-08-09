using AutoMapper;
using Datas.Models;
using Datas.Repositories;
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]

        public async Task<IActionResult> GetAllEducationalMaterial()
        {
            var educationalMaterials = await _unitOfWork.EducationalMaterialRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SimpleEducationalMaterial>>(educationalMaterials));
        }


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

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _unitOfWork.EducationalMaterialRepository.GetById(id);
            if (result == null) return NotFound();

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

        [HttpGet("EducationalElementsByMaterialTypeId/{TypeId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]

        public async Task<IActionResult> GetAllEducationalMaterialByType(int TypeId)
        {
            var educationalMaterials = await _unitOfWork.EducationalMaterialRepository.GetAll();
            var filtredList = educationalMaterials.Where(x => x.materialTypeId == TypeId).ToList();
            return Ok(_mapper.Map<IEnumerable<SimpleEducationalMaterial>>(filtredList));
        }


        [HttpGet("EducationalElementsForGivenAuthor/{authorId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]

        public async Task<IActionResult> GetAllMaterialsForGivenAuthor(int authorId)
        {
            var educationalMaterials = await _unitOfWork.EducationalMaterialRepository.GetAll();
            var filtredList = educationalMaterials.Where(x => x.authorId == authorId).ToList();
            return Ok(_mapper.Map<IEnumerable<SimpleEducationalMaterial>>(filtredList));
        }

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
