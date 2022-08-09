using Datas.Models;
using Datas.Seeder;
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
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.SeedDB();
        }
    }
}