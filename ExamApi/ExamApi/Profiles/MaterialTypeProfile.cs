using AutoMapper;
using Datas.Models;
using ExamApi.DTOs.MaterialType;

namespace ExamApi.Profiles
{
    public class MaterialTypeProfile: Profile
    {
        public MaterialTypeProfile()
        {
            CreateMap<MaterialType, SimpleMaterialTypeDTO>();
        }
    }
}
