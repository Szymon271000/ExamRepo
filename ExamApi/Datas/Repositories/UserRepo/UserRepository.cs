using Datas.Context;
using Datas.Models;
using Microsoft.EntityFrameworkCore;


namespace Datas.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CodeCoolContext context) : base(context)
        {
        }

        public override async Task Add(User entity)
        {
            await _codecoolContext.Users.AddAsync(entity);
            await Save();
        }

        public override async Task Delete(User entity)
        {
            _codecoolContext.Users.Remove(entity);
            await Save();
        }

        public override async Task<List<User>> GetAll()
        {
            return await _codecoolContext.Users.Include(x => x.Roles).ToListAsync();
        }

        public override async Task<User> GetById(int id)
        {
            return await _codecoolContext.Users
                .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public override async Task Save()
        {
            await _codecoolContext.SaveChangesAsync();
        }

        public async Task Update(User entity)
        {
            _codecoolContext.Users.Update(entity);
            await Save();
        }
    }
}
