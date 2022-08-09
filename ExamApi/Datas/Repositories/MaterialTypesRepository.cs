using Datas.Context;
using Datas.Models;
using Microsoft.EntityFrameworkCore;


namespace Datas.Repositories
{
    public class MaterialTypesRepository : IBaseRepository<MaterialType>
    {
        private readonly CodeCoolContext _codecoolContext;

        public MaterialTypesRepository(CodeCoolContext context)
        {
            _codecoolContext = context;
        }
        public async Task Add(MaterialType entity)
        {
            await _codecoolContext.MaterialTypes.AddAsync(entity);
            await Save();
        }

        public async Task Delete(MaterialType entity)
        {
            _codecoolContext.MaterialTypes.Remove(entity);
            await Save();
        }

        public async Task<List<MaterialType>> GetAll()
        {
            return await _codecoolContext.MaterialTypes.Include(x=> x.educationalMaterials).ToListAsync();
        }

        public async Task<MaterialType> GetById(int id)
        {
            return await _codecoolContext.MaterialTypes.
                Include(x=> x.educationalMaterials).
                FirstOrDefaultAsync(x => x.MaterialTypeId == id);
        }

        public async Task Save()
        {
            await _codecoolContext.SaveChangesAsync();
        }

        public async Task Update(MaterialType entity)
        {
            _codecoolContext.MaterialTypes.Update(entity);
            await Save();
        }
    }
}
