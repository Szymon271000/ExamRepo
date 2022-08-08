using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<T> GetById(int id);
        public Task Add(T entity);
        public Task Update(T entity);
        public Task Delete(T entity);
        public Task<List<T>> GetAll();

        public Task Save();
    }
}
