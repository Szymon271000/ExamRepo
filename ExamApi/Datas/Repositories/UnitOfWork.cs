using Datas.Context;
using Datas.Repositories.Interfaces;

namespace Datas.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CodeCoolContext _context;

        public IAuthorsRepository AuthorsRepository { get; set; }

        public IEducationalMaterialRepository EducationalMaterialRepository { get; set; }

        public IMaterialReviewRepository MaterialReviewRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public IRoleRepository RoleRepository { get; set; }

        public IMaterialTypeRepository MaterialTypeRepository { get; set; }

        public UnitOfWork(CodeCoolContext context)
        {
            _context = context;
            AuthorsRepository = new AuthorsRepository(context);
            EducationalMaterialRepository = new EducationalMaterialRepository(context);
            MaterialReviewRepository = new MaterialReviewRepo(context);
            UserRepository = new UserRepository(context);
            RoleRepository = new RoleRepository(context);
            MaterialTypeRepository = new MaterialTypeRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
