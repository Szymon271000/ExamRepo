using Datas.Context;
using Datas.Models;
using Microsoft.EntityFrameworkCore;


namespace Datas.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(CodeCoolContext context) : base(context)
        {
        }

        public override async Task Add(Role entity)
        {
            await _codecoolContext.Roles.AddAsync(entity);
            await Save();
        }

        public override async Task Delete(Role entity)
        {
            _codecoolContext.Roles.Remove(entity);
            await Save();
        }

        public override async Task<List<Role>> GetAll()
        {
            return await _codecoolContext.Roles.Include(x => x.Users).ToListAsync();
        }

        public override async Task<Role> GetById(int id)
        {
            return await _codecoolContext.Roles.
                Include(x => x.Users).
                FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task Save()
        {
            await _codecoolContext.SaveChangesAsync();
        }

        public override async Task Update(Role entity)
        {
            _codecoolContext.Roles.Update(entity);
            await Save();
        }
    }
}
