using AutoMapper;
using Datas.Models;
using ExamApi.DTOs;

namespace ExamApi.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserCreateDto>();
            CreateMap<UserCreateDto, User>();

            CreateMap<User, SimpleUserDTO>();
            CreateMap<SimpleUserDTO, User>();
        }
    }
}
