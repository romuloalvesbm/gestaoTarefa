using GestaoTarefa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Dtos
{
    public class SetorDto
    {
        public Guid SetorId { get; set; }
        public string Nome { get; set; } = string.Empty;               

    }
}
