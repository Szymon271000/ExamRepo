using Datas.Context;
using Datas.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace ExamApi
{
    public static class DefaultAdminCreation
    {
        public static async Task AddAdmin(this WebApplication app)
        {
            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var login = app.Configuration["AdminCredentials:Login"];
                var password = app.Configuration["AdminCredentials:Password"];

                var context = scope.ServiceProvider.GetRequiredService<CodeCoolContext>();
                if (!await context.Users.AnyAsync(x => x.Login == login))
                {
                    HMACSHA512 hmac = new HMACSHA512();
                    var user = new User
                    {
                        Login = login,
                        PasswordSalt = hmac.Key,
                        PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
                    };
                    await context.AddAsync(user);
                    await context.SaveChangesAsync();
                }

                var admin = await context.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Login == login);
                if (admin != null && admin.Roles.Count == 0)
                {
                    var role = await context.Roles.FirstOrDefaultAsync(x => x.Name == login);
                    if (role != null)
                    {
                        admin.Roles.Add(role);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
