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
        private readonly IMapper _mapper;


        public MaterialTypesController(IBaseRepository<MaterialType> materialTypeRepository, IMapper mapper)
        {
            _materialTypeRepository = materialTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMaterialTypes()
        {
            var materialTypes = await _materialTypeRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SimpleMaterialTypeDTO>>(materialTypes));
        }
    }
}
