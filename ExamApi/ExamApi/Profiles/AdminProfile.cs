using AutoMapper;
using Datas.Models;
using ExamApi.DTOs;

namespace ExamApi.Profiles
{
    public class AdminProfile: Profile
    {
        public AdminProfile()
        {
            CreateMap<User, AdminCreateDto>();
            CreateMap<AdminCreateDto, User>();

            CreateMap<User, SimpleAdminDTO>();
            CreateMap<SimpleAdminDTO, User>();
        }
    }
}
