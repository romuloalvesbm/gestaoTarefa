using GestaoTarefa.Domain.Interfaces.Repositories;
using GestaoTarefa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //atributos
        private readonly DataContext _context;
        private IDbContextTransaction? transaction;

        public UnitOfWork(DataContext dbContext)
        {
            _context = dbContext;
        }

        public ITarefaRepository TarefaRepository => new TarefaRepository(_context);

        public ISetorRepository SetorRepository => new SetorRepository(_context);

        public async Task BeginTransaction()
        {
            transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            if (transaction != null)
                await transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            if (transaction != null)
                await transaction.RollbackAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            // Descarte a transação se estiver ativa
            if (transaction != null)
            {
                await transaction.DisposeAsync();
                transaction = null;
            }

            // Descarte o DbContext de forma assíncrona
            if (_context != null)
            {
                await _context.DisposeAsync();
            }

            // Suprime a finalização
            GC.SuppressFinalize(this);
        }
    }
}
