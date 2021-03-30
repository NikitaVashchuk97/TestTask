using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Test_task.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Threading;

namespace Test_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IUserManager _userManager;
        private readonly IStringLocalizer<UsersController> _localizer;
        public AccountsController(IUserManager userManager, IStringLocalizer<UsersController> localizer)
        {
            _localizer = localizer;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var users = await _userManager.GetAllUsers();
                var user = users.FirstOrDefault(u => u.Mail == model.Mail && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Mail, user.IsAdmin == true ? "admin" : "user");

                    return Ok();
                }
                ModelState.AddModelError("Login", _localizer["Login"]);
            }
            return BadRequest(model);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var users = await _userManager.GetAllUsers();
                var user = users.FirstOrDefault(u => u.Mail == model.Mail);
                if (user == null)
                {
                    await _userManager.CreateUser(
                        new User { Mail = model.Mail, Password = model.Password, Surname = model.Surname, Name = model.Name });

                    await Authenticate(model.Mail, "user");

                    return Ok();
                }
                else
                    ModelState.AddModelError("Registration", _localizer["Registration"]);
            }
            return BadRequest(model);
        }

        private async Task Authenticate(string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
