using GestaoTarefa.Domain.Interfaces.Repositories;
using GestaoTarefa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Infra.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        private readonly DataContext _context;

        protected BaseRepository(DataContext context)
        {
            _context = context;
        }

        public async virtual Task Add(T entity)
        {
            await _context.AddAsync(entity);
        }

        public virtual Task Update(T entity)
        {
            _context.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task Delete(T entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAll(List<T> list)
        {
            _context.RemoveRange(list);
            return Task.CompletedTask;
        }

        public async virtual Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync() ?? [];
        }

        public async virtual Task<List<T>> GetAll(Expression<Func<T, bool>> where)
        {
            return await _context.Set<T>().Where(where).ToListAsync() ?? [];
        }

        public async virtual Task<T?> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}

