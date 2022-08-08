using AutoMapper;
using Datas.Models;
using ExamApi.DTOs.UserDTO;

namespace ExamApi.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserCreateDTO>();
            CreateMap<UserCreateDTO, User>();

            CreateMap<User, SimpleUserDTO>();
            CreateMap<SimpleUserDTO, User>();
        }
    }
}
