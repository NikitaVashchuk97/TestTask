using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;

namespace Test_task.CustomMiddleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCultureMiddleware(
           this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }
    }
}
