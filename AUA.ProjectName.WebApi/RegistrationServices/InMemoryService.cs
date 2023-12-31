﻿using AUA.ProjectName.InMemoryServices.Contracts;
using AUA.ProjectName.InMemoryServices.Services;

namespace AUA.ProjectName.WebApi.RegistrationServices
{
    public static class InMemoryService
    {
        public static void RegistrationInMemoryService(this IServiceCollection services)
        {

            services.AddSingleton<IInMemoryUserAccessService>(new InMemoryUserAccessService());

        }
    }
}
