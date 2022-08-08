using Datas.Models;
using Datas.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IBaseRepository<User> _userRepository;

        public AdminsController(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
