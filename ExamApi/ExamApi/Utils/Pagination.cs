using Datas.Models;
using Datas.Repositories.Interfaces;

namespace ExamApi.Utils
{
    public static class Pagination
    {
        public static async Task<IEnumerable<EducationalMaterial>> GetSeries(SerieParameter serieParameters, IUnitOfWork _unitOfWork)
        {
            var educationalMaterials = await _unitOfWork.EducationalMaterialRepository.GetAll();
            var result = educationalMaterials.OrderBy(x => x.EducationalMaterialId)
                .Skip((serieParameters.PageNumber - 1) * serieParameters.PageSize)
                .Take(serieParameters.PageSize);

            return result;
        }
    }
}
