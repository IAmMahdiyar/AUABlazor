using System.Threading.Tasks;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Models.ListModes.Accounting.UserAccessModels;
using AUA.ProjectName.Models.ViewModels.Accounting.UserAccessModels;
using AUA.ProjectName.Models.ViewModels.Accounting.UserRoleAccessModels;
using AUA.ProjectName.Services.EntitiesService.Accounting.Contracts;
using AUA.ProjectName.Services.ListService.Accounting.Contracts;
using AUA.ProjectName.ValidationServices.Accounting.UserAccessValidations.Contracts;
using AUA.ProjectName.WebApi.Controllers;
using AUA.ProjectName.WebApi.Utility.ApiAuthorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AUA.ProjectName.WebApi.Areas.Accounting.Controllers
{
    [WebApiAuthorize(EUserAccess.UserAccess)]
    public class UserAccessController : BaseApiController
    {
        private readonly IUserAccessListService _userAccessListService;
        private readonly IUserAccessService _userAccessService;
        private readonly IUserAccessDtoInsertValidationService _accessDtoInsertValidation;
        private readonly IUserAccessDtoUpdateValidationService _accessDtoUpdateValidation;

        public UserAccessController(IUserAccessListService userAccessListService, IUserAccessService userAccessService, IUserAccessDtoInsertValidationService accessDtoInsertValidation, IUserAccessDtoUpdateValidationService accessDtoUpdateValidation)
        {
            _userAccessListService = userAccessListService;
            _userAccessService = userAccessService;
            _accessDtoInsertValidation = accessDtoInsertValidation;
            _accessDtoUpdateValidation = accessDtoUpdateValidation;
        }

        [HttpPost]
        public async Task<ResultModel<ListResultVm<UserAccessListDto>>> ListAsync(UserAccessSearchVm userAccessSearchVm)
        {
            ValidationSearchVm(userAccessSearchVm);

            if (HasError)
                return CreateInvalidResult<ListResultVm<UserAccessListDto>>();

            var result = await _userAccessListService
                                        .ListAsync(userAccessSearchVm);


            return CreateSuccessResult(result);
        }

        [HttpGet]
        public async Task<ResultModel<List<UserAccessDto>>> ListAsync()
        {
            var result = await _userAccessService
                .GetAllDto()
                .AsNoTracking()
                .ToListAsync();

            return CreateSuccessResult(result);
        }

        [HttpPost]
        public async Task<ResultModel<int>> InsertAsync(UserAccessInsertVm userAccessInsertVm)
        {
            await ValidationInsertVm(userAccessInsertVm);

            if (HasError)
                return CreateInvalidResult<int>();

            var id = await _userAccessService.InsertCustomVmAsync(userAccessInsertVm);

            return id == 0 ?
                CreateInvalidResult<int>(EResultStatus.Success) :
                CreateSuccessResult(id);
        }

        private async Task ValidationInsertVm(UserAccessInsertVm userAccessInsertVm)
        {
            ValidationResultVm = await _accessDtoInsertValidation.ValidationAsync(userAccessInsertVm);
        }

        [HttpDelete]
        public async Task<ResultModel<bool>> DeleteAsync(int id)
        {
            var isDeleted = await _userAccessService.TryDeleteAsync(id);

            return isDeleted ? CreateSuccessResult(isDeleted) : CreateInvalidResult<bool>(EResultStatus.ErrorOperations);
        }

        [HttpPost]
        public async Task<ResultModel<bool>> UpdateAsync(UserAccessDto userAccessDto)
        {
            await ValidationUpdateVmAsync(userAccessDto);

            if (HasError)
                return CreateInvalidResult<bool>();

            var result = await _userAccessService.UpdateCustomVmAsync(userAccessDto);

            return result ?
                CreateSuccessResult(true) :
                CreateInvalidResult<bool>();
        }

        private async Task ValidationUpdateVmAsync(UserAccessDto userAccessDto)
        {
            ValidationResultVm = await _accessDtoUpdateValidation
                .ValidationAsync(userAccessDto);
        }
    }
}