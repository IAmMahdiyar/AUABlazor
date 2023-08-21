using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Models.ViewModels.Accounting.AppUserModels;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.Accounting
{
    public partial class EditProfile
    {
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }

        private AppUserDto _appUserDto = new AppUserDto();
        protected override async Task OnInitializedAsync()
        {
            HttpService.SetAuthorizationToken(await SecurityHelper.GetAccessTokenAsync(StateProvider));

            await JsRuntime.StartLoading();

            var resultModel = await HttpService.GetAsync<ResultModel<AppUserDto>>(ApiUrlConsts.GetCurrentUser);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                _appUserDto = resultModel.Result;
                _appUserDto.AppTypeCode = EAppType.WebSite;
            }

            await JsRuntime.StopLoading();
        }

        private async Task UpdateAppUserAsync()
        {
            var resultModel = await HttpService.PostAsync<ResultModel<object>>(ApiUrlConsts.UpdateAppUser, _appUserDto);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.UpdateSuccess);
            }
        }
    }
}
