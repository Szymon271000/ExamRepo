using Datas.Context;
using Datas.Models;
using Microsoft.EntityFrameworkCore;


namespace Datas.Repositories.Interfaces
{
    public class MaterialTypeRepository : GenericRepository<MaterialType>, IMaterialTypeRepository
    {
        public MaterialTypeRepository(CodeCoolContext context) : base(context)
        {
        }
        public override async Task Add(MaterialType entity)
        {
            await _codecoolContext.MaterialTypes.AddAsync(entity);
            await Save();
        }

        public override async Task Delete(MaterialType entity)
        {
            _codecoolContext.MaterialTypes.Remove(entity);
            await Save();
        }

        public override async Task<List<MaterialType>> GetAll()
        {
            return await _codecoolContext.MaterialTypes.Include(x => x.educationalMaterials).ToListAsync();
        }

        public override async Task<MaterialType> GetById(int id)
        {
            return await _codecoolContext.MaterialTypes.
                Include(x => x.educationalMaterials).
                FirstOrDefaultAsync(x => x.MaterialTypeId == id);
        }

        public override async Task Save()
        {
            await _codecoolContext.SaveChangesAsync();
        }

        public override async Task Update(MaterialType entity)
        {
            _codecoolContext.MaterialTypes.Update(entity);
            await Save();
        }
    }
}
