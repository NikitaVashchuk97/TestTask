using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Test_task.Models;
using Test_task.Controllers;

namespace Test_task.CustomMiddleware
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserManager userManager)
        {
            var userAuthorizedMail = context.User.Identity.Name;
            var user = await userManager.GetUserByMail(userAuthorizedMail);

            var userLanguage = user?.Language ?? "en-US";

            var cultureInfo = new CultureInfo(userLanguage);

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next.Invoke(context);
        }
    }
}
