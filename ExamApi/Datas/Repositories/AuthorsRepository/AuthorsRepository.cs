using Datas.Context;
using Datas.Models;
using Microsoft.EntityFrameworkCore;


namespace Datas.Repositories
{
    public class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
    {
        public AuthorsRepository(CodeCoolContext context) : base(context)
        {
        }

        public override async Task Add(Author entity)
        {
            await _codecoolContext.Authors.AddAsync(entity);
            await Save();
        }

        public override async Task Delete(Author entity)
        {
            _codecoolContext.Authors.Remove(entity);
            await Save();
        }

        public override async Task<List<Author>> GetAll()
        {
            return await _codecoolContext.Authors
                .Include(x => x.EducationalMaterials)
                .ToListAsync();
        }

        public override async Task<Author> GetById(int id)
        {
            return await _codecoolContext.Authors.
                Include(x => x.EducationalMaterials)
                .FirstOrDefaultAsync(x => x.AuthorId == id);
        }

        public override async Task Save()
        {
            await _codecoolContext.SaveChangesAsync();
        }

        public override async Task Update(Author entity)
        {
            _codecoolContext.Authors.Update(entity);
            await Save();
        }
    }
}
