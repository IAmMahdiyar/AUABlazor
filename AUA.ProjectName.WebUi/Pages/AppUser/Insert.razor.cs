﻿using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Pages.AppUser
{
    public partial class Insert
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public CustomAuthenticationStateProvider StateProvider { get; set; }
        [Inject] public IHttpService HttpService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        private readonly AppUserDto _appUserDto = new AppUserDto();

        private async Task InsertAsync()
        {
            HttpService.SetAuthorizationToken(await SecurityHelper.GetAccessTokenAsync(StateProvider));

            var resultModel = await HttpService.PostAsync<ResultModel<object>>(ApiUrlConsts.InsertAppUser, _appUserDto);

            if (await HttpService.IsValidAsync(JsRuntime, resultModel))
            {
                await JsRuntime.SuccessMessage(MessageConsts.InsertSuccess);

                NavigationManager.NavigateTo("/Users");
            }
        }
    }
}
