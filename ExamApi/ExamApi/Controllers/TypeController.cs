using AutoMapper;
using Datas.Models;
using Datas.Repositories;
using ExamApi.DTOs.EducationalMaterialDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly IBaseRepository<EducationalMaterial> _educationalMaterialRepository;
        private readonly IMapper _mapper;

        public TypeController(IBaseRepository<EducationalMaterial> educationalMaterialRepository, IMapper mapper)
        {
            _educationalMaterialRepository = educationalMaterialRepository;
            _mapper = mapper;
        }


    }
}
