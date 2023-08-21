using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.AppUser
{
    public partial class Index
    {
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        private List<AppUserDto>? _appUserDtos;

        protected override async Task OnInitializedAsync() => await SetAppUserDtos();

        private AppUserDto GetAppUserDtoById(long id)
        {
            return _appUserDtos.First(p => p.Id == id);
        }

        private Dictionary<string, object> GetDetails(AppUserDto appUser)
        {
            return new Dictionary<string, object>
            {
                { nameof(appUser.FirstName), appUser.FirstName },
                { nameof(appUser.LastName), appUser.LastName },
                { nameof(appUser.UserName), appUser.UserName },
                { nameof(appUser.Phone), appUser.Phone },
                { nameof(appUser.Email), appUser.Email },
                { nameof(appUser.IsActive), appUser.IsActive },
                { nameof(appUser.RegistrationDate), appUser.RegistrationDate },
            };
        }

        
        private async Task DeleteAsync(long id)
        {
            var resultModel = await HttpService.DeleteAsync<ResultModel<object>>(ApiUrlConsts.DeleteAppUserUrl(id));

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.DeleteSuccess);

                await ReloadAsync();
            }
        }

        private async Task SetAppUserDtos()
        {
            HttpService.SetAuthorizationToken(await SecurityHelper.GetAccessTokenAsync(StateProvider));

            var resultModel = await HttpService.GetAsync<ResultModel<List<AppUserDto>>>(ApiUrlConsts.GetAppUsers);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                _appUserDtos = resultModel.Result;
            }
        }

        private async Task ReloadAsync()
        {
            await SetAppUserDtos();

            StateHasChanged();
        }
    }
}
