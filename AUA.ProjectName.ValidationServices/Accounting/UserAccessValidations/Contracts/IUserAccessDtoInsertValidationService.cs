using AUA.ProjectName.Models.BaseModel.BaseValidationModels;
using AUA.ProjectName.Models.ViewModels.Accounting.UserAccessModels;

namespace AUA.ProjectName.ValidationServices.Accounting.UserAccessValidations.Contracts
{
    public interface IUserAccessDtoInsertValidationService
    {
        Task<ValidationResultVm> ValidationAsync(UserAccessInsertVm insertVm);
    }
}
