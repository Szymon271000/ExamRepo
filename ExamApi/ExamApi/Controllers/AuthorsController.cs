using AutoMapper;
using Datas.Models;
using Datas.Repositories;
using Datas.Repositories.Interfaces;
using ExamApi.DTOs.AuthorDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]

    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _unitOfWork.AuthorsRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SimpleAuthorDTO>>(authors));
        }

        [HttpPut("{id}/author/{authorId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> AddAuthorToEducationalMaterial(int id, int authorId)
        {
            var educationalMaterial = await _unitOfWork.EducationalMaterialRepository.GetById(id);
            if (educationalMaterial == null)
            {
                return NotFound();
            }
            var author = await _unitOfWork.AuthorsRepository.GetById(authorId);
            if (author == null)
            {
                return NotFound();
            }
            
            if (educationalMaterial.author != null)
            {
                educationalMaterial.author.EducationalMaterialsCounter -= 1;
                if (educationalMaterial.author.EducationalMaterialsCounter == 0)
                {
                    educationalMaterial.author.EducationalMaterialsCounter = 0;
                }
            }
            author.EducationalMaterials.Add(educationalMaterial);
            int amount = author.EducationalMaterials.Count;
            author.EducationalMaterialsCounter = amount;
            await _unitOfWork.AuthorsRepository.Update(author);
            return NoContent();
        }
    }
}
