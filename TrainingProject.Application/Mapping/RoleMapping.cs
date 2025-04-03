using AutoMapper;
using TrainingProject.Domain.Dto.Role;
using TrainingProject.Domain.Entity;

namespace TrainingProject.Application.Mapping;

public class RoleMapping : Profile
{
    public RoleMapping()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
    }
}
