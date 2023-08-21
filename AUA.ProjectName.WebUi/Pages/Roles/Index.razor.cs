using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.Roles
{
    public partial class Index
    {
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        private List<RoleDto>? _roleDtos;

        protected override async Task OnInitializedAsync() => await SetRoleDtos();

        private RoleDto GetRoleDtoById(long id)
        {
            return _roleDtos.First(p => p.Id == id);
        }

        private Dictionary<string, object> GetDetails(RoleDto role)
        {
            return new Dictionary<string, object>
            {
                { nameof(role.Title), role.Title },
                { nameof(role.Description), role.Description },
                { nameof(role.IsActive), role.IsActive },
                { nameof(role.RegistrationDate), role.RegistrationDate }
            };
        }


        private async Task DeleteAsync(long id)
        {
            var resultModel = await HttpService.DeleteAsync<ResultModel<object>>(ApiUrlConsts.DeleteRoleUrl(id));

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.DeleteSuccess);

                await ReloadAsync();
            }
        }

        private async Task SetRoleDtos()
        {
            HttpService.SetAuthorizationToken(await SecurityHelper.GetAccessTokenAsync(StateProvider));

            var resultModel = await HttpService.GetAsync<ResultModel<List<RoleDto>>>(ApiUrlConsts.GetRoles);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                _roleDtos = resultModel.Result;
            }
        }

        private async Task ReloadAsync()
        {
            await SetRoleDtos();

            StateHasChanged();
        }

    }
}
