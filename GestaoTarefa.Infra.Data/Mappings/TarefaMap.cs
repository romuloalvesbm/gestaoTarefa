using GestaoTarefa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Infra.Data.Mappings
{
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            //nome da tabela
            builder.ToTable("TAREFA");

            //chave primária
            builder.HasKey(t => t.TarefaId);

            //mapeamento dos campos
            builder.Property(t => t.Nome)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(t => t.Data)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(t => t.Descricao)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(t => t.Prioridade)
                   .IsRequired();
        }
    }
}
