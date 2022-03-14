using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Authorization
{
    public class IsEmployeeHandler : AuthorizationHandler<IsAllowedToEditProductRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAllowedToEditProductRequirement requirement)
        {
            var identity = context.User.Identity as ClaimsIdentity;

            if(identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                var name = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault();

                if(name.Value == "member")
                {
                    context.Succeed(requirement);
                }

               
            }

            //if(context.User.HasClaim(f => f.Type == "Employee"))
            //{
            //    context.Succeed(requirement);
            //}

            return Task.CompletedTask;
        }
    }
}
