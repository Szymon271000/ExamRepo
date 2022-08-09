using AutoMapper;
using Datas.Models;
using ExamApi.DTOs.ReviewDTO;

namespace ExamApi.Profiles
{
    public class ReviewProfile:Profile
    {
        public ReviewProfile()
        {
            CreateMap<MaterialReview, SimpleReviewDTO>()
                                .ForMember(dest => dest.educationalMaterial, opt => opt.MapFrom(src => src.educationalMaterial.Description));
            CreateMap<ReviewToCreateDTO, MaterialReview>();
            CreateMap<ReviewToUpdateDTO, MaterialReview>();
            CreateMap<MaterialReview, ReviewToUpdateDTO>();
        }
    }
}
