using Database.Entities;
using Services;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;

namespace BackendEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public List<User> Get()
        {
            return _userService.UserList();
        }


   

        // POST api/<UsersController>
        [HttpPost]
        public User Post([FromBody]User user)
        {
            return _userService.RegisterUser(user);
        }

        

       
    }
}
