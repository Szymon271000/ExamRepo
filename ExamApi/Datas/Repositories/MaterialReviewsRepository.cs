using Datas.Context;
using Datas.Models;
using Microsoft.EntityFrameworkCore;


namespace Datas.Repositories
{
    public class MaterialReviewsRepository : IBaseRepository<MaterialReview>
    {
        private readonly CodeCoolContext _codecoolContext;

        public MaterialReviewsRepository(CodeCoolContext context)
        {
            _codecoolContext = context;
        }
        public async Task Add(MaterialReview entity)
        {
            await _codecoolContext.MaterialReviews.AddAsync(entity);
            await Save();
        }

        public async Task Delete(MaterialReview entity)
        {
            _codecoolContext.MaterialReviews.Remove(entity);
            await Save();
        }

        public async Task<List<MaterialReview>> GetAll()
        {
            return await _codecoolContext.MaterialReviews.Include(x => x.educationalMaterial).ToListAsync();
        }

        public async Task<MaterialReview> GetById(int id)
        {
            return await _codecoolContext.MaterialReviews.
                Include(x => x.educationalMaterial)
                .FirstOrDefaultAsync(x => x.MaterialReviewId == id);
        }

        public async Task Save()
        {
            await _codecoolContext.SaveChangesAsync();
        }

        public async Task Update(MaterialReview entity)
        {
            _codecoolContext.MaterialReviews.Update(entity);
            await Save();
        }
    }
}
