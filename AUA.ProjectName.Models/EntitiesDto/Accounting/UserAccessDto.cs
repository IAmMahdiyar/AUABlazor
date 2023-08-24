using System.ComponentModel.DataAnnotations;
using AUA.Mapping.Mapping.Contract;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.DomainEntities.Entities.Accounting;
using AUA.ProjectName.Models.BaseModel.BaseDto;

namespace AUA.ProjectName.Models.EntitiesDto.Accounting
{
    public class UserAccessDto : BaseEntityDto, IMapFrom<UserAccess>
    {
        [Required]
        [MaxLength(LengthConsts.MaxStringLen100)]
        public string? PageTitle { get; set; }

        [Required]
        [MaxLength(LengthConsts.MaxStringLen100)]
        public string Title { get; set; }

        [Required]
        public int ParentId { get; set; }

        public EUserAccess AccessCode { get; set; }

        [MaxLength(LengthConsts.MaxStringLen100)]
        public string? Url { get; set; }

        [Required]
        public bool IsIndirect { get; set; }

        public string? PageDescription { get; set; }

    }
}
