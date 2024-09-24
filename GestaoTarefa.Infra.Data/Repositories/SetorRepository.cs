using GestaoTarefa.Domain.Entities;
using GestaoTarefa.Domain.Interfaces.Repositories;
using GestaoTarefa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Infra.Data.Repositories
{
    public class SetorRepository : BaseRepository<Setor>, ISetorRepository
    {
        private readonly DataContext _context;

        public SetorRepository(DataContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Setor?> GetByName(string name)
        {
           return await _context.Setores.FirstOrDefaultAsync(x => x.Nome == name);
        }
    }
}
