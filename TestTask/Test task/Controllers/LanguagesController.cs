using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Test_task.Models;

namespace Test_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly IOptions<RequestLocalizationOptions> _locOptions;


        public LanguagesController(IOptions<RequestLocalizationOptions> localOptions)
        {
            _locOptions = localOptions;
        }
    

        [HttpGet]      
        public List<string> GetCultures()
        {
            var cultureItems = _locOptions.Value.SupportedUICultures.Select(c => c.Name).ToList<string>();

            return cultureItems;

        }
    }
}
