using Datas.Models;
using Microsoft.EntityFrameworkCore;


namespace Datas.Seeder
{
    public static class EducationalMaterialSeeder
    {
        public static void SeedEducationalMaterial(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EducationalMaterial>().HasData(
                new EducationalMaterial { EducationalMaterialId = 1, Title = "FirstOne", authorId = 1, Description = "First material", Location = "codecoolʼs library at Slusarska 9", materialTypeId = 1 },
                new EducationalMaterial { EducationalMaterialId = 2, Title = "SecondOne", authorId = 2, Description = "Second material", Location = "codecoolʼs library at Slusarska 9", materialTypeId = 2 },
                new EducationalMaterial { EducationalMaterialId = 3, Title = "ThirdOne", authorId = 3, Description = "Third material", Location = "codecoolʼs library at Slusarska 9", materialTypeId = 3 },
                new EducationalMaterial { EducationalMaterialId = 4, Title = "FourhOne", authorId = 4, Description = "Fourth material", Location = "codecoolʼs library at Slusarska 9", materialTypeId = 4 }
                );
        }
    }
}
