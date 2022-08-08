using AutoMapper;
using Datas.Models;
using Datas.Repositories;
using ExamApi.DTOs.EducationalMaterialDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationalMaterialController : ControllerBase
    {
        private readonly IBaseRepository<EducationalMaterial> _educationalMaterialRepository;
        private readonly IBaseRepository<MaterialReview> _materialReviewRepository;
        private readonly IMapper _mapper;

        public EducationalMaterialController(IBaseRepository<EducationalMaterial> educationalMaterialRepository, IBaseRepository<MaterialReview> materialReviewRepository, IMapper mapper)
        {
            _educationalMaterialRepository = educationalMaterialRepository;
            _materialReviewRepository = materialReviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]

        public async Task<IActionResult> GetAllEducationalMaterial()
        {
            var educationalMaterials = await _educationalMaterialRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SimpleEducationalMaterial>>(educationalMaterials));
        }


        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]

        public async Task<IActionResult> GetEducationalMaterialById(int id)
        {
            var educationalMaterial = await _educationalMaterialRepository.GetById(id);
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
                await _educationalMaterialRepository.Add(educationalMaterialToAdd);
                return Ok(_mapper.Map<SimpleEducationalMaterial>(educationalMaterialToAdd));
            }
            return this.BadRequest("Every field is required and must have min 3 characters and max 20");
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]

        public async Task<IActionResult> UpdateEducationalMaterial(int id, EducationalMaterialToUpdateDTO eduDTO)
        {
            var educationalMaterialToUpdate = await _educationalMaterialRepository.GetById(id);
            if (educationalMaterialToUpdate == null)
            {
                return this.NotFound("This material does not exist");
            }
            _mapper.Map(eduDTO, educationalMaterialToUpdate);
            await _educationalMaterialRepository.Update(educationalMaterialToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _educationalMaterialRepository.GetById(id);
            if (result == null) return NotFound();

            var MaterialswithResult = await _materialReviewRepository.GetAll();
            foreach (var item in MaterialswithResult)
            {
                if (item.educationalMaterialId == result.EducationalMaterialId)
                {
                    item.educationalMaterialId = null;
                }
            }
            await _educationalMaterialRepository.Delete(result);
            return NoContent();
        }

        [HttpGet("EducationalElementsByMaterialTypeId/{TypeId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]

        public async Task<IActionResult> GetAllEducationalMaterialByType(int TypeId)
        {
            var educationalMaterials = await _educationalMaterialRepository.GetAll();
            var filtredList = educationalMaterials.Where(x => x.materialTypeId == TypeId).ToList();
            return Ok(_mapper.Map<IEnumerable<SimpleEducationalMaterial>>(filtredList));
        }


        [HttpGet("EducationalElementsForGivenAuthor{authorId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]

        public async Task<IActionResult> GetAllMaterialsForGivenAuthor(int authorId)
        {
            var educationalMaterials = await _educationalMaterialRepository.GetAll();
            var filtredList = educationalMaterials.Where(x => x.authorId == authorId).ToList();
            return Ok(_mapper.Map<IEnumerable<SimpleEducationalMaterial>>(filtredList));
        }
    }
}
