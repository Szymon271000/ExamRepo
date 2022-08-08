using Datas.Context;
using Datas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Repositories
{
    public class UsersRepository: IBaseRepository<User>
    {
        private readonly CodeCoolContext _codecoolContext;

        public UsersRepository(CodeCoolContext context)
        {
            _codecoolContext = context;
        }
        public async Task Add(User entity)
        {
            await _codecoolContext.Users.AddAsync(entity);
            await Save();
        }

        public async Task Delete(User entity)
        {
            _codecoolContext.Users.Remove(entity);
            await Save();
        }

        public async Task<List<User>> GetAll()
        {
            return await _codecoolContext.Users.Include(x => x.Roles).ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _codecoolContext.Users
                .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task Save()
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
