using Datas.Models;
using Datas.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]

    public class MaterialTypesController : ControllerBase
    {
        private readonly IBaseRepository<MaterialType> _materialTypeRepository;

        public MaterialTypesController(IBaseRepository<MaterialType> materialTypeRepository)
        {
            _materialTypeRepository = materialTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMaterialTypes()
        {
            var materialTypes = await _materialTypeRepository.GetAll();
            return Ok();
        }
    }
}
