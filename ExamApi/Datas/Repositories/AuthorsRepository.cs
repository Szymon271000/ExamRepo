using Datas.Context;
using Datas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Repositories.Interfaces
{
    public class AuthorsRepository : IBaseRepository<Author>
    {
        private readonly CodeCoolContext _codecoolContext;

        public AuthorsRepository(CodeCoolContext context)
        {
            _codecoolContext = context;
        }
        public async Task Add(Author entity)
        {
            await _codecoolContext.Authors.AddAsync(entity);
            await Save();
        }

        public async Task Delete(Author entity)
        {
            _codecoolContext.Authors.Remove(entity);
            await Save();
        }

        public async Task<List<Author>> GetAll()
        {
            return await _codecoolContext.Authors
                .Include(x => x.EducationalMaterials)
                .ThenInclude(x => x.Reviews).
                ThenInclude(x => x.MaterialReviewDigit)
                .ToListAsync();
        }

        public async Task<Author> GetById(int id)
        {
            return await _codecoolContext.Authors.
                Include(x => x.EducationalMaterials)
                .ThenInclude(x=> x.Reviews).
                ThenInclude(x=> x.MaterialReviewDigit)
                .FirstOrDefaultAsync(x => x.AuthorId == id);
        }

        public async Task Save()
        {
            await _codecoolContext.SaveChangesAsync();
        }

        public async Task Update(Author entity)
        {
            _codecoolContext.Authors.Update(entity);
            await Save();
        }
    }
}
