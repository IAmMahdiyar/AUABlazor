using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.DomainEntities.Entities.Accounting;
using AUA.ProjectName.Models.BaseModel.BaseValidationModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Models.ViewModels.Accounting.UserAccessModels;
using AUA.ProjectName.Services.EntitiesService.Accounting.Contracts;
using AUA.ProjectName.ValidationServices.Accounting.UserAccessValidations.Contracts;
using AUA.ProjectName.ValidationServices.BaseValidations;
using Microsoft.EntityFrameworkCore;

namespace AUA.ProjectName.ValidationServices.Accounting.UserAccessValidations.Services
{
    public class UserAccessDtoInsertValidationService : BaseValidationService, IUserAccessDtoInsertValidationService
    {
        private UserAccessInsertVm _userAccessDto;
        private readonly IUserAccessService _userAccessService;


        public UserAccessDtoInsertValidationService(IUserAccessService userAccessService)
        {
            _userAccessService = userAccessService;
        }


        public async Task<ValidationResultVm> ValidationAsync(UserAccessInsertVm insertVm)
        {
            _userAccessDto = insertVm;

            await DoValidationAsync();

            return ValidationResultVm;
        }

        private async Task DoValidationAsync()
        {
            DefaultValidation();

            if (HasError) return;

            await ValidationAccessCodeAsync();

        }

        private void DefaultValidation()
        {
            if (IsEmpty(_userAccessDto.PageTitle))
                AddError("PageTitle", "PageTitle is empty");

            if (_userAccessDto.AccessCode==EUserAccess.None)
                AddError("AccessCode", "AccessCode is empty");

        }


        private async Task ValidationAccessCodeAsync()
        {
            var isExistsAccessCode = await IsExistsAccessCode();

            if (isExistsAccessCode)
                AddError("AccessCode", "AccessCode is a duplicate");

        }

        private async Task<bool> IsExistsAccessCode()
        {
            return await _userAccessService
                              .GetAll()
                              .AsNoTracking()
                              .AnyAsync(p => p.AccessCode == _userAccessDto.AccessCode);

        }
    }
}
