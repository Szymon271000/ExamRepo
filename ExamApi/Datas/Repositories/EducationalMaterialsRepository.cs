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
    public class EducationalMaterialsRepository : IBaseRepository<EducationalMaterial>
    {
        private readonly CodeCoolContext _codecoolContext;

        public EducationalMaterialsRepository(CodeCoolContext context)
        {
            _codecoolContext = context;
        }
        public async Task Add(EducationalMaterial entity)
        {
            await _codecoolContext.EducationalMaterials.AddAsync(entity);
            await Save();
        }

        public async Task Delete(EducationalMaterial entity)
        {
            _codecoolContext.EducationalMaterials.Remove(entity);
            await Save();
        }

        public async Task<List<EducationalMaterial>> GetAll()
        {
            return await _codecoolContext.EducationalMaterials.Include(x => x.author).Include(x=> x.materialType).ToListAsync();
        }

        public async Task<EducationalMaterial> GetById(int id)
        {
            return await _codecoolContext.EducationalMaterials.
                Include(x => x.author).
                Include(x=> x.materialType).
                FirstOrDefaultAsync(x => x.EducationalMaterialId == id);
        }

        public async Task Save()
        {
            await _codecoolContext.SaveChangesAsync();
        }

        public async Task Update(EducationalMaterial entity)
        {
            _codecoolContext.EducationalMaterials.Update(entity);
            await Save();
        }
    }
}
