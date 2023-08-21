using System.ComponentModel.DataAnnotations;
using AUA.Mapping.Mapping.Contract;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.DomainEntities.Entities.Accounting;
using AUA.ProjectName.Models.BaseModel.BaseDto;
using AutoMapper;

namespace AUA.ProjectName.Models.EntitiesDto.Accounting
{
    public sealed class AppUserDto : BaseEntityDto<long>, IHaveCustomMappings
    {
        [Required]
        [MaxLength(LengthConsts.MaxStringLen50)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(LengthConsts.MaxStringLen50)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(LengthConsts.MaxStringLen50)]
        public string? UserName { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(LengthConsts.MaxStringLen250)]
        public string? Password { get; set; }

        [Required]
        [MaxLength(11)]
        public string? Phone { get; set; }

        [Required]
        [MaxLength(LengthConsts.MaxStringLen50)]
        public string? Email { get; set; }

        public string? FullName => $"{FirstName}  {LastName}";

        public ICollection<UserRoleDto> UserRoles { get; set; }

        public void ConfigureMapping(Profile configuration)
        {
            configuration.CreateMap<AppUser, AppUserDto>()
                .ForMember(p => p.UserRoles, p => p.MapFrom(q => q.UserRoles));

            configuration.CreateMap<AppUserDto, AppUser>();
        }
    }
}
