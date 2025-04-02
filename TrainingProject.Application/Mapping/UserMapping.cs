using AutoMapper;
using TrainingProject.Domain.Dto.User;
using TrainingProject.Domain.Entity;

namespace TrainingProject.Application.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
