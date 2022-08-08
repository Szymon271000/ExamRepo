using Datas.Models;
using Microsoft.EntityFrameworkCore;


namespace Datas.Context
{
    public class CodeCoolContext : DbContext
    {
        public CodeCoolContext(DbContextOptions<CodeCoolContext> opt) : base(opt)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<EducationalMaterial> EducationalMaterials { get; set; }
        public DbSet<MaterialReview> MaterialReviews { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MaterialType>().HasData(
                new MaterialType { MaterialTypeId = 1, MaterialTypeName = "Video Tutorial", DefinitionMaterialType = "Video tutorial focused on EF" },
                new MaterialType { MaterialTypeId = 2, MaterialTypeName = "Documentation", DefinitionMaterialType = "Documentation focused on EF" },
                new MaterialType { MaterialTypeId = 3, MaterialTypeName = "Exercises", DefinitionMaterialType = "Excersises focused on EF" },
                new MaterialType { MaterialTypeId = 4, MaterialTypeName = "Video explanation", DefinitionMaterialType = "Video explanation focused on EF" }
                );
            modelBuilder.Entity<MaterialReview>().HasData(
                new MaterialReview { MaterialReviewId = 1, MaterialReviewDescription = "I like the content but author has terrible accent", MaterialReviewDigit = 2, educationalMaterialId = 1},
                new MaterialReview { MaterialReviewId = 2, MaterialReviewDescription = "Good content", MaterialReviewDigit = 8, educationalMaterialId = 2},
                new MaterialReview { MaterialReviewId = 3, MaterialReviewDescription = "Bad content", MaterialReviewDigit = 1, educationalMaterialId = 3},
                new MaterialReview { MaterialReviewId = 4, MaterialReviewDescription = "Normal content", MaterialReviewDigit = 6, educationalMaterialId = 4}
                );
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, AuthorName ="Willy", Description = "New author" },
                new Author { AuthorId = 2, AuthorName ="Bobby", Description = "New author2" },
                new Author { AuthorId = 3, AuthorName ="Jack", Description = "New author3" },
                new Author { AuthorId = 4, AuthorName ="Harry", Description = "New author4" }
                );
            modelBuilder.Entity<EducationalMaterial>().HasData(
                new EducationalMaterial { EducationalMaterialId = 1, Title = "FirstOne",authorId = 1, Description = "First material", Location = "codecoolʼs library at Slusarska 9", materialTypeId = 1 },
                new EducationalMaterial { EducationalMaterialId = 2, Title = "SecondOne",authorId = 2, Description = "Second material", Location = "codecoolʼs library at Slusarska 9", materialTypeId = 2 },
                new EducationalMaterial { EducationalMaterialId = 3, Title = "ThirdOne",authorId = 3, Description = "Third material", Location = "codecoolʼs library at Slusarska 9", materialTypeId = 3 },
                new EducationalMaterial { EducationalMaterialId = 4, Title = "FourhOne",authorId = 4, Description = "Fourth material", Location = "codecoolʼs library at Slusarska 9", materialTypeId = 4 }
                );
        }
    }
}