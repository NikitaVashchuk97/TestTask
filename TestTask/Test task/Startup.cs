using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Test_task.Controllers;
using Test_task.CustomMiddleware;
using Test_task.Loggers;
using Test_task.Models;

namespace Test_task
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            var cultureRu = new CultureInfo("ru-RU");
            var cultureEn = new CultureInfo("en-US");
            var supportedCultures = new[] { cultureEn, cultureRu };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.DefaultRequestCulture = new RequestCulture(cultureEn);
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");


            string connection = Configuration.GetConnectionString("DefaultConnectionDb");
            services.AddDbContext<UsersContext>(options => options.UseMySql(connection,
                new MySqlServerVersion(new Version(8, 0, 11))));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();


            services.AddScoped<IUserManager, DbUserManager>();
           

            services.AddControllers().AddDataAnnotationsLocalization();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseRequestCultureMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
