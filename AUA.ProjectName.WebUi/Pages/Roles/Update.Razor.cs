using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.ListModes.Accounting.AppUserModels;
using AUA.ProjectName.Models.ListModes.Accounting.RoleModels;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.Roles
{
    public partial class Update
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        [Parameter] public int RoleId { get; set; }

        private RoleListDto _roleDto = new RoleListDto();

        protected override async Task OnInitializedAsync()
        {
            HttpService.SetAuthorizationToken(await SecurityHelper.GetAccessTokenAsync(StateProvider));

            await JsRuntime.StartLoading();

            await SetRoleDtoAsync();
        }

        private async Task SetRoleDtoAsync()
        {
            var resultModel = await HttpService.PostAsync<ResultModel<ListResultVm<RoleListDto>>>(ApiUrlConsts.GetRoles, new AppUserSearchVm()
            {
                Id = RoleId,
                PageSize = 1,
                PageNumber = 1,
                TotalSize = 1,
            });

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                _roleDto = resultModel.Result.ResultVms.First();
                await JsRuntime.StopLoading();
            }
        }

        private async Task UpdateAsync()
        {
            var resultModel = await HttpService.PostAsync<ResultModel<object>>(ApiUrlConsts.UpdateRole, _roleDto);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.UpdateSuccess);

                NavigationManager.NavigateTo("/Roles");
            }
        }
    }
}
