using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.Access
{
    public partial class Insert
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        private readonly UserAccessDto _userAccessDto = new UserAccessDto();

        private SelectAccessParent _selectAccessParent;

        private SelectUserAccess _selectUserAccess;

        private async Task InsertAsync()
        {
            HttpService.SetAuthorizationToken(await SecurityHelper.GetAccessTokenAsync(StateProvider));

            SetAccessParent();

            SetUserAccess();

            var resultModel = await HttpService.PostAsync<ResultModel<object>>(ApiUrlConsts.InsertUserAccess, _userAccessDto);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.InsertSuccess);

                NavigationManager.NavigateTo("/Accesses");
            }
        }

        private void SetUserAccess()
        {
            _userAccessDto.AccessCode = (EUserAccess) _selectUserAccess.AccessCode;
        }

        private void SetAccessParent()
        {
            _userAccessDto.ParentId = _selectAccessParent.UserAccessDto.ParentId;
        }
    }
}
