

namespace Datas.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthorsRepository AuthorsRepository { get; }
        IEducationalMaterialRepository EducationalMaterialRepository { get; }
        IMaterialReviewRepository MaterialReviewRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IMaterialTypeRepository MaterialTypeRepository { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
