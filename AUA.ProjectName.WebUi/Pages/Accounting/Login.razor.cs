using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.GeneralModels.LoginModels;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.Accounting
{
    public partial class Login
    {
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        private readonly LoginVm _loginVm = new LoginVm();

        private async Task LoginAsync()
        {
            var resultModel = await HttpService.PostAsync<ResultModel<UserLoggedInVm>>(ApiUrlConsts.Login, _loginVm);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await StateProvider.LoginAsync(resultModel.Result);

                NavigationManager.NavigateTo("/");
            }
        }
    }
}
