using Datas.Context;
using Microsoft.EntityFrameworkCore;


namespace Datas.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        public CodeCoolContext _codecoolContext;
        public DbSet<T> dbSet;

        public GenericRepository(CodeCoolContext context)
        {
            _codecoolContext = context;
            dbSet = _codecoolContext.Set<T>();
        }

        public virtual async Task Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task Save()
        {
            throw new NotImplementedException();
        }

        public virtual async Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
