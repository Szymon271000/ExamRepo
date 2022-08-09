using Datas.Models;
using Microsoft.EntityFrameworkCore;


namespace Datas.Seeder
{
    public static class AuthorSeeder
    {
        public static void SeedAuthors(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, AuthorName = "Willy", Description = "New author" },
                new Author { AuthorId = 2, AuthorName = "Bobby", Description = "New author2" },
                new Author { AuthorId = 3, AuthorName = "Jack", Description = "New author3" },
                new Author { AuthorId = 4, AuthorName = "Harry", Description = "New author4" }
                );
        }
    }
}
