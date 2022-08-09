using Datas.Models;
using Microsoft.EntityFrameworkCore;


namespace Datas.Seeder
{
    public static class MaterialTypeSeeder
    {
        public static void SeedMaterialType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaterialType>().HasData(
                new MaterialType { MaterialTypeId = 1, MaterialTypeName = "Video Tutorial", DefinitionMaterialType = "Video tutorial focused on EF" },
                new MaterialType { MaterialTypeId = 2, MaterialTypeName = "Documentation", DefinitionMaterialType = "Documentation focused on EF" },
                new MaterialType { MaterialTypeId = 3, MaterialTypeName = "Exercises", DefinitionMaterialType = "Excersises focused on EF" },
                new MaterialType { MaterialTypeId = 4, MaterialTypeName = "Video explanation", DefinitionMaterialType = "Video explanation focused on EF" }
                );
        }
           
    }
}
