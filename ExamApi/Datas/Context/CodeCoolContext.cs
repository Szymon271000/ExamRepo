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
                new MaterialReview { MaterialReviewId = 1, MaterialReviewDescription = "I like the content but author has terrible accent", MaterialReviewDigit = 2, educationalMaterialId = 1}
                );
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, AuthorName ="Willy", Description = "New author" }
                );
            modelBuilder.Entity<EducationalMaterial>().HasData(
                new EducationalMaterial { EducationalMaterialId = 1, authorId = 1, Description = "First material", Location = "codecoolʼs library at Slusarska 9", materialTypeId = 1 }
                );
        }
    }
}