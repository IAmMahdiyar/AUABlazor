using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Extensions;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Models.ViewModels.Accounting.UserRoleAccessModels;
using AUA.ProjectName.Models.ViewModels.Accounting.UserRoleModels;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.Roles
{
    public partial class Access
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter] public int RoleId { get; set; }

        private List<UserAccessDto>? _accesses;

        private List<UserRoleAccessDto>? _roleAccesses;

        protected override async Task OnInitializedAsync()
        {
            HttpService.SetAuthorizationToken(await SecurityHelper.GetAccessTokenAsync(StateProvider));

            await SetAccessAsync();

            await SetRoleAccessAsync();
        }

        private async Task SetAccessAsync()
        {
            var resultModel = await HttpService.GetAsync<ResultModel<List<UserAccessDto>>>(ApiUrlConsts.GetAccesses);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                _accesses = resultModel.Result;
            }
        }

        private async Task SetRoleAccessAsync()
        {
            var resultModel = await HttpService.GetAsync<ResultModel
                    <List<UserRoleAccessDto>>>(ApiUrlConsts.GetUserAccessRoleByRoleIdUrl(RoleId));

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                _roleAccesses = resultModel.Result;
            }

        }

        private async Task AssignAsync(int accessId)
        {

            var resultModel = await HttpService.PostAsync<ResultModel<bool>>
                (ApiUrlConsts.InsertUserRoleAccess, CreateUserRoleAccessInsertVm(accessId));

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.UpdateSuccess);

                await ReloadAsync();
            }

        }

        private async Task DismissAsync(int accessId)
        {

            var resultModel = await HttpService.PostAsync<ResultModel<bool>>
                (ApiUrlConsts.DeleteUserRoleAccess, CreateUserRoleAccessInsertVm(accessId));

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.UpdateSuccess);

                await ReloadAsync();
            }

        }

        private UserRoleAccessInsertVm CreateUserRoleAccessInsertVm(int accessId)
        {
            return new UserRoleAccessInsertVm()
            {
                IsActive = true,
                UserAccessId = accessId,
                RoleId = RoleId,
            };
        }

        private async Task ReloadAsync()
        {
            await SetRoleAccessAsync();

            await SetAccessAsync();

            StateHasChanged();
        }

        private Dictionary<string, object> GetDetails(UserAccessDto userAccess)
        {
            return new Dictionary<string, object>
            {
                {nameof(userAccess.Title), userAccess.Title},
                {nameof(userAccess.PageTitle), userAccess.PageTitle},
                {nameof(userAccess.PageDescription), userAccess.PageDescription},
                {nameof(userAccess.Url), userAccess.Url},
                {nameof(userAccess.AccessCode), userAccess.AccessCode.ToDescription()},
                {nameof(userAccess.IsIndirect), userAccess.IsIndirect},
                {nameof(userAccess.ParentId), userAccess.ParentId},
                {nameof(userAccess.IsActive), userAccess.IsActive},
                {nameof(userAccess.RegistrationDate), userAccess.RegistrationDate},
            };
        }

    }
}
