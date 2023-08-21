using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.ViewModels.Accounting.AppUserModels;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.Accounting
{
    public partial class ChangePassword
    {
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }

        private ChangePasswordVm _changePasswordVm = new ChangePasswordVm();

        private async Task ChangePasswordAsync()
        {
            await JsRuntime.StartLoading();

            HttpService.SetAuthorizationToken(await SecurityHelper.GetAccessTokenAsync(StateProvider));

            var resultModel = await HttpService.PostAsync
                <ResultModel<object>>(ApiUrlConsts.ChangePassword, _changePasswordVm);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.UpdateSuccess);

                _changePasswordVm = new ChangePasswordVm();
            }
        }
    }
}
