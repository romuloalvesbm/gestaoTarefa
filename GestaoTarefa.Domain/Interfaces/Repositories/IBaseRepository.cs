using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> : IAsyncDisposable
       where T : class
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task DeleteAll(List<T> list);

        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> where);

        Task<T?> GetById(Guid id);
    }
}
