using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.IO;
using Test_task.Controllers;
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
            
            string connection = Configuration.GetConnectionString("DefaultConnectionDb");
            services.AddDbContext<UsersContext>(options => options.UseMySql(connection,
                new MySqlServerVersion(new Version(8, 0, 11))));
           
            
            services.AddSingleton<IUserManager, FileUserManager>();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
