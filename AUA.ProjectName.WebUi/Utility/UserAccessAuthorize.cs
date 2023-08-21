using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.GeneralModels.LoginModels;
using AUA.ProjectName.WebUi.Utility.AuthorizationRequirements;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Serialization;

namespace AUA.ProjectName.WebUi.Utility
{
    public sealed class UserAccessAuthorize : AuthorizationHandler<UserAccessRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAccessRequirement requirement)
        {
            if (SecurityHelper.IsLoggedIn(context.User))
            {
                var loggedInVm = SecurityHelper.GetUserLoggedInVm(context.User);

                var requiredAccesses = context.Resource as List<EUserAccess>;

                if (HasUserAccess(requiredAccesses, loggedInVm))
                    context.Succeed(requirement);

                else
                    context.Fail(new AuthorizationFailureReason(this, MessageConsts.UserForbidden));

            }
            return Task.CompletedTask;
        }

        private bool HasUserAccess(IEnumerable<EUserAccess> accessesRequired, UserLoggedInVm loggedInVm)
        {
            return loggedInVm.UserAccessIds.Any(x => accessesRequired.Any(y => y == x));
        }

    }

}
