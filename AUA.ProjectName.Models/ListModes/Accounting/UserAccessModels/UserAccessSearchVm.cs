using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;

namespace AUA.ProjectName.Models.ListModes.Accounting.UserAccessModels
{
    public class UserAccessSearchVm : BaseSearchVm
    {
        public int? Id { get; set; }

        public string? PageTitle { get; set; }

        public string? Title { get; set; }

        public EUserAccess AccessCode { get; set; }

        public string? Url { get; set; }

        public string? PageDescription { get; set; }

        public bool? IsActive { get; set; }

        public bool IsIndirect { get; set; }

        public int? ParentId { get; set; }
    }
}
