using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;

namespace AUA.ProjectName.Models.ViewModels.Accounting.AppUserModels
{
    public class ChangePasswordVm : GeneralBaseVm
    {
        [Required]
        public string? OldPassword { get; set; }

        [Required]
        [MinLength(8)]
        public string? NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = MessageConsts.ConfirmPasswordInvalid)]
        public string? ConfirmPassword { get; set; }
    }
}
