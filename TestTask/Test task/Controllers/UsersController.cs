using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_task.Models;

namespace Test_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUserManager userMan;

        public UsersController(IUserManager um)
        {
            userMan = um;

        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await userMan.GetAllUsers();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {

             var qweqwedqwd = BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await userMan.CreateUser(user));
        }
    }
}
