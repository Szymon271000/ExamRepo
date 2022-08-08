using AutoMapper;
using Datas.Models;
using ExamApi.DTOs.EducationalMaterialDTO;

namespace ExamApi.Profiles
{
    public class EducationalMaterialProfile: Profile
    {
        public EducationalMaterialProfile()
        {
            CreateMap<EducationalMaterial, SimpleEducationalMaterial>();
            CreateMap<SimpleEducationalMaterial, EducationalMaterial>();

            CreateMap<EducationalMaterialToCreateDTO, EducationalMaterial>();
            CreateMap<EducationalMaterial, EducationalMaterialToCreateDTO>();

            CreateMap<EducationalMaterial, EducationalMaterialToUpdateDTO>();
            CreateMap<EducationalMaterialToUpdateDTO, EducationalMaterial>();

        }
    }
}
