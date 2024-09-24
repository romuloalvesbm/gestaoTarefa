using GestaoTarefa.Domain.Entities;
using GestaoTarefa.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Infra.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TarefaMap());
            modelBuilder.ApplyConfiguration(new SetorMap());

            modelBuilder.Entity<Setor>(entity =>
            {
                entity.HasIndex(u => u.Nome).IsUnique();
            });

        }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Setor> Setores { get; set; }

    }
}
