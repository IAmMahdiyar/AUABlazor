using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Extensions;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.Access
{
    public partial class Index
    {
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        private List<UserAccessDto>? _accessDtos;

        protected override async Task OnInitializedAsync() => await SetAccessDtos();

        private UserAccessDto GetAccessDtoById(long id)
        {
            return _accessDtos.First(p => p.Id == id);
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

        private async Task DeleteAsync(long id)
        {
            var resultModel = await HttpService.DeleteAsync<ResultModel<object>>(ApiUrlConsts.DeleteUserAccessUrl(id));

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.DeleteSuccess);

                await ReloadAsync();
            }
        }

        private async Task SetAccessDtos()
        {
            HttpService.SetAuthorizationToken(await SecurityHelper.GetAccessTokenAsync(StateProvider));

            var resultModel = await HttpService.GetAsync<ResultModel<List<UserAccessDto>>>(ApiUrlConsts.GetUserAccesses);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                _accessDtos = resultModel.Result;
            }
        }

        private async Task ReloadAsync()
        {
            await SetAccessDtos();

            StateHasChanged();
        }

        private UserAccessDto? GetParent(int parentId)
        {
            return _accessDtos.FirstOrDefault(p => p.Id == parentId);
        }
    }
}
