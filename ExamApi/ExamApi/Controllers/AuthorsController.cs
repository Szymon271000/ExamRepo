using AutoMapper;
using Datas.Models;
using Datas.Repositories;
using ExamApi.DTOs.AuthorDTO;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;

        public AuthorsController(IBaseRepository<Author> authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<SimpleAuthorDTO>>(authors));
        }
    }
}
