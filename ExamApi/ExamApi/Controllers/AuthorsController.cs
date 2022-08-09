using AutoMapper;
using Datas.Models;
using Datas.Repositories;
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
        private readonly IBaseRepository<Author> _authorRepository;
        private readonly IBaseRepository<EducationalMaterial> _educationalMaterialRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IBaseRepository<Author> authorRepository, IBaseRepository<EducationalMaterial> educationalMaterialRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _educationalMaterialRepository = educationalMaterialRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, User")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SimpleAuthorDTO>>(authors));
        }

        [HttpPut("{id}/author/{authorId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> AddAuthorToEducationalMaterial(int id, int authorId)
        {
            var educationalMaterial = await _educationalMaterialRepository.GetById(id);
            if (educationalMaterial == null)
            {
                return NotFound();
            }
            var author = await _authorRepository.GetById(authorId);
            if (author == null)
            {
                return NotFound();
            }
            author.EducationalMaterials.Add(educationalMaterial);
            await _authorRepository.Update(author);
            return NoContent();
        }
    }
}
