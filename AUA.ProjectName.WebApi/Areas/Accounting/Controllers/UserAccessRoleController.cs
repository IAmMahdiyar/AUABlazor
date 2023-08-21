using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Models.ViewModels.Accounting.UserRoleAccessModels;
using AUA.ProjectName.Services.EntitiesService.Accounting.Contracts;
using AUA.ProjectName.WebApi.Controllers;
using AUA.ProjectName.WebApi.Utility.ApiAuthorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AUA.ProjectName.WebApi.Areas.Accounting.Controllers
{
    [WebApiAuthorize(EUserAccess.UserRoleAccess)]
    public class UserRoleAccessController : BaseApiController
    {
        private readonly IUserRoleAccessService _userRoleAccessService;

        public UserRoleAccessController(IUserRoleAccessService userRoleAccessService)
        {
            _userRoleAccessService = userRoleAccessService;
        }

        [HttpGet]
        public async Task<ResultModel<IEnumerable<UserRoleAccessDto>>> GetUserAccessRoleByRoleIdAsync(int roleId)
        {
            var result = await _userRoleAccessService.GetUserAccessRoleByRoleId(roleId);

            return CreateSuccessResult(result);
        }

        [HttpPost]
        public async Task<ResultModel<bool>> InsertManyUserRoleAccessAsync(IEnumerable<UserRoleAccessInsertVm> userRoleAccessInsertVm)
        {

            await _userRoleAccessService.InsertVms(userRoleAccessInsertVm, UserId);

            return CreateSuccessResult(true);
        }

        [HttpPost]
        public async Task<ResultModel<bool>> InsertUserRoleAccessAsync(UserRoleAccessInsertVm userRoleAccessInsertVm)
        {

            await _userRoleAccessService.InsertCustomVmAsync(userRoleAccessInsertVm);

            return CreateSuccessResult(true);
        }


        [HttpPost]
        public async Task<ResultModel<bool>> DeleteUserRoleAccessAsync(UserRoleAccessInsertVm userRoleAccessInsertVm)
        {
            var userRole = await _userRoleAccessService.GetAllDto()
                .Where(p => p.RoleId == userRoleAccessInsertVm.RoleId)
                .FirstAsync(p => p.UserAccessId == userRoleAccessInsertVm.UserAccessId);

            var isDeleted = await _userRoleAccessService.TryDeleteAsync(userRole);

            return isDeleted ? CreateSuccessResult(true) : CreateSuccessResult(false);
        }

    }
}