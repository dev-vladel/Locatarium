using Locatarium.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Locatarium.Web.Utils
{
    public class ResidenceAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, ResidenceModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ResidenceModel resource)
        {
            if (context.User.FindFirst(ClaimTypes.SerialNumber).Value == resource.UserId.ToString())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
