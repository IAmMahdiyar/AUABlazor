using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.Services.GeneralService.Http.Services;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.Access
{
    public partial class SelectAccessParent
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }
        [Parameter] public UserAccessDto UserAccessDto { get; set; }

        private List<UserAccessDto> _userAccessDtos = new List<UserAccessDto>();

        private async Task SetAccessDtos()
        {
            await JsRuntime.StartLoading();

            HttpService.SetAuthorizationToken(await SecurityHelper.GetAccessTokenAsync(StateProvider));

            var resultModel = await HttpService.GetAsync<ResultModel<List<UserAccessDto>>>(ApiUrlConsts.GetUserAccesses);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                _userAccessDtos = resultModel.Result;
                await JsRuntime.StopLoading();
            }
        }

        protected override async Task OnInitializedAsync() => await SetAccessDtos();
    }
}
