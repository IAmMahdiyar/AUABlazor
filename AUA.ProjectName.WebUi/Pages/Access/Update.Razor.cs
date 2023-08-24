using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Models.ListModes.Accounting.AppUserModels;
using AUA.ProjectName.Models.ListModes.Accounting.RoleModels;
using AUA.ProjectName.Models.ListModes.Accounting.UserAccessModels;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.Access
{
    public partial class Update
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        [Parameter] public int AccessId { get; set; }

        private UserAccessDto _accessDto = new UserAccessDto();

        private SelectAccessParent _selectAccessParent;

        private SelectUserAccess _selectUserAccess;

        protected override async Task OnInitializedAsync()
        {
            HttpService.SetAuthorizationToken(await SecurityHelper.GetAccessTokenAsync(StateProvider));

            await JsRuntime.StartLoading();

            await SetAccessDtoAsync();
        }

        private async Task SetAccessDtoAsync()
        {
            var resultModel = await HttpService.PostAsync<ResultModel<ListResultVm<UserAccessDto>>>(ApiUrlConsts.GetUserAccesses, new UserAccessSearchVm()
            {
                Id = AccessId,
                PageSize = 1,
                PageNumber = 1,
                TotalSize = 1,
            });

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                _accessDto = resultModel.Result.ResultVms.First();
                await JsRuntime.StopLoading();
            }
        }

        private async Task UpdateAsync()
        {
            SetUserAccess();

            SetAccessParent();

            var resultModel = await HttpService.PostAsync<ResultModel<object>>(ApiUrlConsts.UpdateUserAccess, _accessDto);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.UpdateSuccess);

                NavigationManager.NavigateTo("/Accesses");
            }
        }

        private void SetUserAccess()
        {
            _accessDto.AccessCode = (EUserAccess)_selectUserAccess.AccessCode;
        }

        private void SetAccessParent()
        {
            _accessDto.ParentId = _selectAccessParent.UserAccessDto.ParentId;
        }
    }
}
