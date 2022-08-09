using AutoMapper;
using Datas.Models;
using Datas.Repositories;
using Datas.Repositories.Interfaces;
using ExamApi.DTOs.MaterialType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]

    public class MaterialTypesController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public MaterialTypesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
        public async Task<IActionResult> GetAllMaterialTypes()
        {
            var materialTypes = await _unitOfWork.MaterialTypeRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SimpleMaterialTypeDTO>>(materialTypes));
        }

        [HttpPut("{id}/materialTypes/{educationalMaterialId}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> AddTypeToEducationalMaterial(int id, int educationalMaterialId)
        {
            var typelMaterial = await _unitOfWork.MaterialTypeRepository.GetById(id);
            if (typelMaterial == null)
            {
                return NotFound();
            }
            var educationalMaterial = await _unitOfWork.EducationalMaterialRepository.GetById(educationalMaterialId);
            if (educationalMaterial == null)
            {
                return NotFound();
            }
            typelMaterial.educationalMaterials.Add(educationalMaterial);
            await _unitOfWork.MaterialTypeRepository.Update(typelMaterial);
            return NoContent();
        }
    }
}
