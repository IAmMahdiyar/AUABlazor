using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.DomainEntities.Entities.Accounting;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Models.ViewModels.Accounting.RoleModels;
using AUA.ProjectName.Models.ViewModels.Accounting.UserRoleModels;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.AppUser
{
    public partial class Roles
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter] public long UserId { get; set; }

        private List<RoleDto>? _roles;

        private List<UserRoleDto>? _userRoles;

        protected override async Task OnInitializedAsync()
        {
            HttpService.SetAuthorizationToken(await SecurityHelper.GetAccessTokenAsync(StateProvider));

            await SetRolesAsync();

            await SetUserRolesAsync();
        }

        private async Task SetRolesAsync()
        {
            var resultModel = await HttpService.GetAsync<ResultModel<List<RoleDto>>>(ApiUrlConsts.GetRoles);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                _roles = resultModel.Result;
            }
        }

        private async Task SetUserRolesAsync()
        {
            var resultModel =
                await HttpService.GetAsync<ResultModel<List<UserRoleDto>>>(ApiUrlConsts.GetUserRoleUrl(UserId));

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                _userRoles = resultModel.Result;
            }

        }

        private async Task AssignAsync(int roleId)
        {

            var resultModel =
                await HttpService.PostAsync<ResultModel<bool>>(ApiUrlConsts.InsertUserRole, new UserRoleDto
                {
                    AppUserId = UserId,
                    IsActive = true,
                    RoleId = roleId,
                });

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.UpdateSuccess);

                await ReloadAsync();
            }

        }

        private async Task DismissAsync(int roleId)
        {

            var resultModel =
                await HttpService.PostAsync<ResultModel<bool>>(ApiUrlConsts.DeleteUserRole, new UserRoleDto
                {
                    AppUserId = UserId,
                    IsActive = true,
                    RoleId = roleId,
                });

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.UpdateSuccess);

                await ReloadAsync();
            }

        }

        private async Task ReloadAsync()
        {
            await SetRolesAsync();

            await SetUserRolesAsync();

            StateHasChanged();
        }
    }
}
