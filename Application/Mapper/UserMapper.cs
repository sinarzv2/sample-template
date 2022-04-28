using Application.Dto;
using AutoMapper;
using Domain.IdentityModel;

namespace Application.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
