using Application.Dto.UserDto;
using AutoMapper;
using Domain.Entities.IdentityModel;

namespace Application.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterRequest, User>().ReverseMap();
        }
    }
}
