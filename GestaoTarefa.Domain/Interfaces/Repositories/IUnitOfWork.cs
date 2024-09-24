using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
        Task SaveChanges();

        ITarefaRepository TarefaRepository { get; }
        ISetorRepository SetorRepository { get; }

    }
}
