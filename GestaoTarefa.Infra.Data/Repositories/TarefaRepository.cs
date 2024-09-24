using GestaoTarefa.Domain.Entities;
using GestaoTarefa.Domain.Interfaces.Repositories;
using GestaoTarefa.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Infra.Data.Repositories
{
    internal class TarefaRepository : BaseRepository<Tarefa>, ITarefaRepository
    {
        private readonly DataContext _context;

        public TarefaRepository(DataContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
