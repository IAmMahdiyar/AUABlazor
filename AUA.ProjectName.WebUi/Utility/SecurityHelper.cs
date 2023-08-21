using System.Security.Claims;
using System.Security.Cryptography;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Common.Extensions;
using AUA.ProjectName.Common.Tools.Security;
using AUA.ProjectName.Models.GeneralModels.LoginModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using static System.String;

namespace AUA.ProjectName.WebUi.Utility
{
    public static class SecurityHelper
    {
        public static bool IsLoggedIn(ClaimsPrincipal user) =>
            user.Identity != null && user.Identity.IsAuthenticated;
        public static async Task<bool> IsAuthenticatedAsync(AuthenticationStateProvider stateProvider)
        {
            var state = await stateProvider.GetAuthenticationStateAsync();

            return state.User.Identity != null &&
                   state.User.Identity.IsAuthenticated;
        }

        public static UserLoggedInVm GetUserLoggedInVm(ClaimsPrincipal user)
        {
            if (!IsLoggedIn(user))
                return new UserLoggedInVm();

            var userData = GetClimesData(user);

            if (IsNullOrWhiteSpace(userData))
                userData = GetUserDataFromContext(user);

            return IsNullOrWhiteSpace(userData) ?
                new UserLoggedInVm() :
                userData.ObjectDeserialize<UserLoggedInVm>();

        }
        private static string GetUserDataFromContext(ClaimsPrincipal user)
        {
            var userData = user.Claims.FirstOrDefault(p => p.Type == AppConsts.UserDataClimeName);

            if (userData == null)
                return null;

            return userData.ToString();
        }
        public static string GetSha512Hash(string value)
        {
            return EncryptionHelper.GetSha512Hash(value);
        }

        private static string GetClimesData(ClaimsPrincipal user)
        {
            return (from c in user.Claims
                    where c.Type == AppConsts.UserDataClimeName
                    select c.Value)
            .FirstOrDefault()!;
        }

        private static async Task CreateFormsAuthenticationTicketAsync(bool rememberMe, string userData, dynamic context)
        {
            var expireDate = GetExpireDate(rememberMe);

            var claimsIdentity = GetClaimsIdentity(userData);

            await context.SignInAsync(new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = expireDate
                });

        }

        public static ClaimsIdentity GetClaimsIdentity(string userData)
        {
            var claims = GetClaims(userData);

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        }

        private static IEnumerable<Claim> GetClaims(string userData)
        {
            return new List<Claim>
            {
                new Claim(AppConsts.UserDataClimeName,userData )
            };
        }

        private static DateTime GetExpireDate(bool rememberMe)
        {
            return rememberMe ? DateTime.UtcNow.AddDays(60) :
                                DateTime.UtcNow.AddDays(30);
        }


        public static Guid CreateCryptographicallySecureGuid()
        {
            var rand = RandomNumberGenerator.Create();

            var bytes = new byte[16];
            rand.GetBytes(bytes);
            return new Guid(bytes);
        }

        public static string GetHashGuid()
        {
            return EncryptionHelper
                .GetSha256Hash(CreateCryptographicallySecureGuid().ToString());
        }

        public static List<EUserAccess> CreateUserAccesses(params EUserAccess[] userAccesses)
        {
            return userAccesses.ToList();
        }

        public static async Task<string> GetAccessTokenAsync(CustomAuthenticationStateProvider stateProvider)
        {
            var state = await stateProvider.GetAuthenticationStateAsync();

            return GetUserLoggedInVm(state.User).AccessToken;
        }
    }
}