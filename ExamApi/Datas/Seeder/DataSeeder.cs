using Microsoft.EntityFrameworkCore;


namespace Datas.Seeder
{
    public static class DataSeeder
    {
        public static void SeedDB(this ModelBuilder builder)
        {
            builder.SeedAuthors();
            builder.SeedEducationalMaterial();
            builder.SeedMaterialReview();
            builder.SeedMaterialType();
            builder.SeedRoles();
        }

    }
}
