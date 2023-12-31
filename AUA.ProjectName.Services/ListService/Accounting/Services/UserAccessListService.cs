﻿using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.DataLayer.AppContext.EntityFrameworkContext;
using AUA.ProjectName.DomainEntities.Entities.Accounting;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.ListModes.Accounting.UserAccessModels;
using AUA.ProjectName.Services.ListService.Accounting.Contracts;
using AUA.ServiceInfrastructure.BaseSearchService;
using AutoMapper;

namespace AUA.ProjectName.Services.ListService.Accounting.Services
{
    public sealed class UserAccessListService : BaseListService<UserAccess, UserAccessListDto, UserAccessSearchVm>, IUserAccessListService
    {
        public UserAccessListService(IUnitOfWork unitOfWork, IMapper mapperInstance) : base(unitOfWork, mapperInstance)
        {

        }

        public async Task<ListResultVm<UserAccessListDto>> ListAsync(UserAccessSearchVm userAccessSearchVm)
        {
            SetSearchVm(userAccessSearchVm);

            ApplyTitleFilter();
            ApplyIdFilter();
            ApplyPageTitleFilter();
            ApplyDescriptionFilter();
            ApplyIsActiveFilters();
            ApplyAccessCodeFilter();


            return await CreateListVmResultAsync();
        }

        private void ApplyTitleFilter()
        {
            if (string.IsNullOrWhiteSpace(SearchVm.Title))
                return;

            Query = Query.Where(p => p.Title.Contains(SearchVm.Title));
        }

        private void ApplyIdFilter()
        {
            if (SearchVm.Id == 0 || SearchVm.Id == null)
                return;

            Query = Query.Where(p => p.Id == SearchVm.Id);
        }

        private void ApplyPageTitleFilter()
        {
            if (string.IsNullOrWhiteSpace(SearchVm.PageTitle))
                return;

            Query = Query.Where(p => p.PageTitle.Contains(SearchVm.PageTitle));
        }

        private void ApplyDescriptionFilter()
        {
            if (string.IsNullOrWhiteSpace(SearchVm.PageDescription))
                return;

            Query = Query.Where(p => p.PageDescription.Contains(SearchVm.PageDescription));
        }

        private void ApplyAccessCodeFilter()
        {
            if (SearchVm.AccessCode == EUserAccess.None)
                return;

            Query = Query.Where(p => p.AccessCode == SearchVm.AccessCode);
        }


        private void ApplyIsActiveFilters()
        {
            if (!SearchVm.IsActive.HasValue)
                return;

            Query = Query.Where(p => p.IsActive == SearchVm.IsActive);

        }


    }
}
