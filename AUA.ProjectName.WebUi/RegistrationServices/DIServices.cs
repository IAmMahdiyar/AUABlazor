using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using AUA.ProjectName.Services.GeneralService.Http.Services;
using AUA.ProjectName.WebUi.Utility;
using AUA.ProjectName.WebUi.Utility.AuthorizationRequirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace AUA.ProjectName.WebUi.RegistrationServices
{
    public static class DIServices
    {
        public static void RegistrationService(this IServiceCollection services)
        {
            services.HttpServices();
            services.AuthenticationServices();
            services.AuthenticationPolicy();
        }

        private static void HttpServices(this IServiceCollection services)
        {
            services.AddHttpClient<IHttpService, HttpService>();
            services.AddScoped<IHttpService, HttpService>();
        }

        private static void AuthenticationServices(this IServiceCollection services)
        {
            services.AddScoped<CustomAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthenticationStateProvider>());
            services.AddSingleton<IAuthorizationHandler, UserAccessAuthorize>();
        }
        private static void AuthenticationPolicy(this IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy(AppConsts.UserAccessAuthorize, policy => policy.Requirements.Add(new UserAccessRequirement()));
            });
        }
    }
}
