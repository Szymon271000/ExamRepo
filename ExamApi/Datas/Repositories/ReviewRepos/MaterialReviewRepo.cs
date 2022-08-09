using Datas.Context;
using Datas.Models;
using Microsoft.EntityFrameworkCore;


namespace Datas.Repositories
{
    public class MaterialReviewRepo : GenericRepository<MaterialReview>, IMaterialReviewRepository
    {
        public MaterialReviewRepo(CodeCoolContext context) : base(context)
        {
        }
        public override async Task Add(MaterialReview entity)
        {
            await _codecoolContext.MaterialReviews.AddAsync(entity);
            await Save();
        }

        public override async Task Delete(MaterialReview entity)
        {
            _codecoolContext.MaterialReviews.Remove(entity);
            await Save();
        }

        public override async Task<List<MaterialReview>> GetAll()
        {
            return await _codecoolContext.MaterialReviews.Include(x => x.educationalMaterial).ToListAsync();
        }

        public override async Task<MaterialReview> GetById(int id)
        {
            return await _codecoolContext.MaterialReviews.
                Include(x => x.educationalMaterial)
                .FirstOrDefaultAsync(x => x.MaterialReviewId == id);
        }

        public override async Task Save()
        {
            await _codecoolContext.SaveChangesAsync();
        }

        public override async Task Update(MaterialReview entity)
        {
            _codecoolContext.MaterialReviews.Update(entity);
            await Save();
        }
    }
}
