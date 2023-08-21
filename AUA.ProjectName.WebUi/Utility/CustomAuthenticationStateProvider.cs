using System.Security.Claims;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Extensions;
using AUA.ProjectName.Models.GeneralModels.LoginModels;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace AUA.ProjectName.WebUi.Utility
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal();
        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userLoggedInVm = await _sessionStorageService.ReadItemEncryptedAsync<UserLoggedInVm?>(AppConsts.UserDataClimeName);

                return await Task.FromResult(new AuthenticationState(GetClaimsPrincipal(userLoggedInVm)));

            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task LoginAsync(UserLoggedInVm? userLoggedInVm)
        {
            if (userLoggedInVm != null)
            {
                await _sessionStorageService.SaveItemEncryptedAsync(AppConsts.UserDataClimeName, userLoggedInVm);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(GetClaimsPrincipal(userLoggedInVm))));
            }
        }

        public async Task LogoutAsync()
        {
            await _sessionStorageService.RemoveItemAsync(AppConsts.UserDataClimeName);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        private ClaimsPrincipal GetClaimsPrincipal(UserLoggedInVm userLoggedInVm)
        {
            var userData = userLoggedInVm.ObjectSerialize();

            var claimsIdentity = SecurityHelper.GetClaimsIdentity(userData);

            return new ClaimsPrincipal(claimsIdentity);
        }

    }
}
