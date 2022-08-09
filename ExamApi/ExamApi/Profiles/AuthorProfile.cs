using AutoMapper;
using Datas.Models;
using ExamApi.DTOs.AuthorDTO;

namespace ExamApi.Profiles
{
    public class AuthorProfile: Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, SimpleAuthorDTO>()
                                .ForMember(dest => dest.simpleEducationalMaterials, opt => opt.MapFrom(src => src.EducationalMaterials));
        }
    }
}
