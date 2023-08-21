using AUA.ProjectName.Models.BaseModel.BaseViewModels;

namespace AUA.ProjectName.Models.ListModes.Accounting.RoleModels
{
    public class RoleSearchVm:  BaseSearchVm
    {
        public int? Id { get; set; }

        public string? Title { get; set; }
        
        public string? Description { get; set; }

        public bool? IsActive { get; set; }

    }
}
