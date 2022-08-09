using Datas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Seeder
{
    public static class MaterialReviewSeeder
    {
        public static void SeedMaterialReview(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaterialReview>().HasData(
                new MaterialReview { MaterialReviewId = 1, MaterialReviewDescription = "I like the content but author has terrible accent", MaterialReviewDigit = 2, educationalMaterialId = 1 },
                new MaterialReview { MaterialReviewId = 2, MaterialReviewDescription = "Good content", MaterialReviewDigit = 8, educationalMaterialId = 2 },
                new MaterialReview { MaterialReviewId = 3, MaterialReviewDescription = "Bad content", MaterialReviewDigit = 1, educationalMaterialId = 3 },
                new MaterialReview { MaterialReviewId = 4, MaterialReviewDescription = "Normal content", MaterialReviewDigit = 6, educationalMaterialId = 4 }
                );
        }

    }
}
