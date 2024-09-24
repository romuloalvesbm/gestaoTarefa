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
    public class SetorMap : IEntityTypeConfiguration<Setor>
    {
        public void Configure(EntityTypeBuilder<Setor> builder)
        {
            //nome da tabela
            builder.ToTable("SETOR");

            //chave primária
            builder.HasKey(t => t.SetorId);

            //mapeamento dos campos
            builder.Property(t => t.Nome)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
