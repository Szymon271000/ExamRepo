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
    public class RolesRepository : IBaseRepository<Role>
    {
        private readonly CodeCoolContext _codecoolContext;

        public RolesRepository(CodeCoolContext context)
        {
            _codecoolContext = context;
        }
        public async Task Add(Role entity)
        {
            await _codecoolContext.Roles.AddAsync(entity);
            await Save();
        }

        public async Task Delete(Role entity)
        {
            _codecoolContext.Roles.Remove(entity);
            await Save();
        }

        public async Task<List<Role>> GetAll()
        {
            return await _codecoolContext.Roles.Include(x => x.Users).ToListAsync();
        }

        public async Task<Role> GetById(int id)
        {
            return await _codecoolContext.Roles.
                Include(x => x.Users).
                FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save()
        {
            await _codecoolContext.SaveChangesAsync();
        }

        public async Task Update(Role entity)
        {
            _codecoolContext.Roles.Update(entity);
            await Save();
        }
    }
}
