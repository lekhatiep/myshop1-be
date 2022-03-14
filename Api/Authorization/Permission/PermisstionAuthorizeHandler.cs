using Infastructure.Data;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Authorization.Permission
{
    public class PermisstionAuthorizeHandler : AuthorizationHandler<PermissionRequirement>
    {
      
        public PermisstionAuthorizeHandler()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if(context == null)
            {
                context.Fail();
            }


            var identity = context.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                var name = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
                var role = claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;

                var issuer = claims.Where(x => x.Issuer == "adminMyshop@gmail.com");

                //identity.AddClaim(new Claim("Permission","Value"));

                var permissions = context.User.Claims
               .Where(x => x.Type == "Permission" && x.Value == requirement.Permission
               && x.Issuer == "adminMyshop@gmail.com");

                var per = claims.Where(x => x.Type == "Permission"
                    && x.Issuer == "adminMyshop@gmail.com").FirstOrDefault().Value.Split(' ');

                if (permissions.Any())
                {
                    context.Succeed(requirement);

                }

                if (per.Any(x=>x == requirement.Permission))
                {
                    context.Succeed(requirement);
                }

            }

            return Task.CompletedTask;
        }
    }
}
