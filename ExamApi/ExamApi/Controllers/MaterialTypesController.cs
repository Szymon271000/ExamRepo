﻿using Datas.Models;
using Datas.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var playlists = await _materialTypeRepository.GetAll();
            return Ok();
        }
    }
}