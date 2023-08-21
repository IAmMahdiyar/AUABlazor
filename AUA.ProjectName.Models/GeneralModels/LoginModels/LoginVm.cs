using System.ComponentModel.DataAnnotations;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;

namespace AUA.ProjectName.Models.GeneralModels.LoginModels
{
    public class LoginVm : GeneralBaseVm
    {
        [Required]
        [MaxLength(LengthConsts.MaxStringLen50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(LengthConsts.MaxStringLen50)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = String.Empty;

    }
}
