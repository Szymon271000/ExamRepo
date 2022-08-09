using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
