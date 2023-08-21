using AUA.ProjectName.WebUi.RegistrationServices;
using Blazored.SessionStorage;

namespace AUA.ProjectName.WebUi.AppConfiguration
{
    public static class StartupConfigExtension
    {
        public static void Configuration(this IServiceCollection services)
        {
            services.RegistrationServices();

            services.AddOptions();

            services.AddAuthorizationCore();

            services.AddBlazoredSessionStorage();
        }
    }

}
