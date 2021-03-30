using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Test_task.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : Controller
    {
        IWebHostEnvironment _appEnvironment;

        public PictureController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                //string path = "/Files/" + uploadedFile.FileName;

                string path = "/Files/" + User.Identity.Name;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
            }

            return Ok();
        }

        [HttpGet]
        public string[] GetAllPictures()
        {
            var fileNames = Directory.GetFiles(@"wwwroot\Files");
            for (int i = 0;i< fileNames.Count(); i++)
            {
                fileNames[i] = fileNames[i].Substring(fileNames[i].IndexOf("\\") + 1);
            }
            return fileNames;
        }
    }
}
