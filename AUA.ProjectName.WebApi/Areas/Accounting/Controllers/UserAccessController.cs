using System.Threading.Tasks;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Models.ListModes.Accounting.UserAccessModels;
using AUA.ProjectName.Services.EntitiesService.Accounting.Contracts;
using AUA.ProjectName.Services.ListService.Accounting.Contracts;
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

        public UserAccessController(IUserAccessListService userAccessListService, IUserAccessService userAccessService)
        {
            _userAccessListService = userAccessListService;
            _userAccessService = userAccessService;
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


    }
}