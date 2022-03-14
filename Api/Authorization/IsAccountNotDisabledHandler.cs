using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Authorization
{
    public class IsAccountNotDisabledHandler : AuthorizationHandler<IsAccountEnabledRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAccountEnabledRequirement requirement)
        {
            if(context.User.HasClaim(x =>x.Type == "Disabled"))
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
