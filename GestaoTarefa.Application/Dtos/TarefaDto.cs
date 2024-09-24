using GestaoTarefa.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Dtos
{
    public class TarefaDto
    {
        public Guid TarefaId { get; set; }
        public Guid SetorId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Prioridade Prioridade { get; set; }
       
    }
}
