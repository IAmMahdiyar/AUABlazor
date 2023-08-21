using AUA.Mapping.Mapping.Contract;
using AUA.ProjectName.DomainEntities.Entities.Accounting;

namespace AUA.ProjectName.Models.ListModes.Accounting.RoleModels
{
    public class RoleListDto : IMapFrom<Role>
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

    }
}
