using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
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

        [HttpGet("getAll")]
        //[Authorize]
        public async Task<IEnumerable<User>> Get()
        {
            return await userMan.GetAllUsers();
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await userMan.GetUserById(id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }


        [HttpPost("create")]
        //[Authorize]
        public async Task<ActionResult<User>> Post(User user)
        {
            return Ok(await userMan.CreateUser(user));
        }

        [HttpPut]
        //[Authorize]
        public void Update(User user)
        {
            if(User.IsInRole("admin") || User.Identity.Name == user.Mail)
            {
                userMan.Update(user);
            }
        }

        [HttpPost("admin")]
        //[Authorize(Roles ="admin")]

        public async Task<ActionResult> SetAdminRole(object id)
        {
            var userId = Convert.ToInt32(id.ToString());
            var user = await userMan.GetUserById(userId);
            user.IsAdmin = !user.IsAdmin;
            await userMan.Update(user);
            return Ok();
        }
    }
}
