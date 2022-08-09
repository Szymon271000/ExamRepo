using AutoMapper;
using Datas.Models;
using Datas.Repositories;
using ExamApi.DTOs.MaterialType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]

    public class MaterialTypesController : ControllerBase
    {
        private readonly IBaseRepository<MaterialType> _materialTypeRepository;
        private readonly IBaseRepository<EducationalMaterial> _educationalMaterialRepository;
        private readonly IMapper _mapper;


        public MaterialTypesController(IBaseRepository<MaterialType> materialTypeRepository, IBaseRepository<EducationalMaterial> educationalMaterialRepository, IMapper mapper)
        {
            _materialTypeRepository = materialTypeRepository;
            _mapper = mapper;
            _educationalMaterialRepository = educationalMaterialRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMaterialTypes()
        {
            var materialTypes = await _materialTypeRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SimpleMaterialTypeDTO>>(materialTypes));
        }

        [HttpPut("{id}/materialTypes/{educationalMaterialId}")]
        public async Task<IActionResult> AddTypeToEducationalMaterial(int id, int educationalMaterialId)
        {
            var typelMaterial = await _materialTypeRepository.GetById(id);
            if (typelMaterial == null)
            {
                return NotFound();
            }
            var educationalMaterial = await _educationalMaterialRepository.GetById(educationalMaterialId);
            if (educationalMaterial == null)
            {
                return NotFound();
            }
            typelMaterial.educationalMaterials.Add(educationalMaterial);
            await _materialTypeRepository.Update(typelMaterial);
            return NoContent();
        }
    }
}
