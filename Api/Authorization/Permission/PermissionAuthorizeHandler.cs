using Api.Services.Users;
using Infastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Authorization.Permission
{
    public class PermissionAuthorizeHandler : AuthorizationHandler<PermissionRequirement>
    {
        IHttpContextAccessor _httpContextAccessor = null;
        private readonly IUserService userService;

        public PermissionAuthorizeHandler(
            IHttpContextAccessor httpContextAccessor,
            IUserService userService
            )
        {
            _httpContextAccessor = httpContextAccessor;
            this.userService = userService;
        }

        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if(context.User == null || !context.User.Identity.IsAuthenticated)
            {

                return Task.CompletedTask;
            }

            var identity = context.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var id = int.Parse( identity.FindFirst("id").Value);
                _httpContextAccessor.HttpContext.Items["Id"] = id;


                var permission =  await userService.GetAllPermissionByUserId(id);

                if (permission.Any(x => x == requirement.Permission))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    return Task.CompletedTask;
                }
            }

            return Task.CompletedTask;
        }
    }
}
