using AutoMapper;
using Tezo.Todo.Dto;
using Tezo.Todo.Dtos;
using Tezo.Todo.Models;

namespace Tezo.Todo.Api.Profiles
{
    // configures mappings between domain models(User and Assignment) and corresponding DTOs(UserDtos and AssignmentDtos)
    // Profile  : Profiles are used to define mappings between types.
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<User, UserAssignmentsDto>()
                //  .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + "" + src.LastName))
                .ReverseMap();

            //  configures a mapping from the Assignment model to the AssignmentDtos DTO
            CreateMap<Assignment, AssignmentDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
