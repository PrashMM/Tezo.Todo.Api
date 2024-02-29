using AutoMapper;

namespace Tezo_Todo.Profiles
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<Models.User, Dtos.UserDtos>()
              //  .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + "" + src.LastName))
                .ReverseMap();

            CreateMap<Models.Assignment, Dtos.AssignmentDtos>().ReverseMap();
        }
    }
}
