using Datas.Models;
using Datas.Repositories;
using Datas.Repositories.Interfaces;

namespace ExamApi.Utils
{
    public static class RepositoryImporting
    {
        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBaseRepository<MaterialType>, MaterialTypesRepository>();
            builder.Services.AddScoped<IBaseRepository<Author>, AuthorsRepository>();
            builder.Services.AddScoped<IBaseRepository<EducationalMaterial>, EducationalMaterialsRepository>();
            builder.Services.AddScoped<IBaseRepository<MaterialReview>, MaterialReviewsRepository>();
            builder.Services.AddScoped<IBaseRepository<User>, UsersRepository>();
            builder.Services.AddScoped<IBaseRepository<Role>, RolesRepository>();
        }

    }
}
