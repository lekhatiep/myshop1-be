using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Authorization.Permission
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;
        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // 1 - if the request is not authenticated, nothing to do
            if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                await _next(context);
                return;
            }

            var cancellationToken = context.RequestAborted;


            // 2. The 'sub' claim is how we find the user in our system
            var userSub = context.User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userSub))
            {
                
                return;
            }
        }
    }
}
