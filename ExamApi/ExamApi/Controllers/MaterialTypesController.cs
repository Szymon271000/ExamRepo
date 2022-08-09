using AutoMapper;
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


        /// <summary>
        /// Get all material types
        /// </summary>
        /// <returns>All material types in DB</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "materialTypeName": "",
        ///        "definitionMaterialType": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns all material types</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
        public async Task<IActionResult> GetAllMaterialTypes()
        {
            var materialTypes = await _unitOfWork.MaterialTypeRepository.GetAll();
            if (materialTypes == null)
            {
                return this.NotFound("There are no materials in context");
            }
            return Ok(_mapper.Map<IEnumerable<SimpleMaterialTypeDTO>>(materialTypes));
        }

        /// <summary>
        /// Update material list of specific type
        /// </summary>
        /// <returns>Update material list</returns>
        /// <param name="id"></param>
        /// <param name="educationalMaterialId"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///       "id": "",
        ///       "educationalMaterialId": "",
        ///     }
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="200">OK</response>
        /// <response code="400">If the item is null</response>
        [HttpPut("{id}/materialTypes/{educationalMaterialId}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> AddTypeToEducationalMaterial(int id, int educationalMaterialId)
        {
            var typelMaterial = await _unitOfWork.MaterialTypeRepository.GetById(id);
            if (typelMaterial == null)
            {
                return this.NotFound("There is no material with this id");
            }
            var educationalMaterial = await _unitOfWork.EducationalMaterialRepository.GetById(educationalMaterialId);
            if (educationalMaterial == null)
            {
                return this.NotFound("There is no educational material with this id");
            }
            typelMaterial.educationalMaterials.Add(educationalMaterial);
            await _unitOfWork.MaterialTypeRepository.Update(typelMaterial);
            return NoContent();
        }
    }
}
