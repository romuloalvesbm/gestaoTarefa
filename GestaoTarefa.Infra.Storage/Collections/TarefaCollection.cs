using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Infra.Storage.Collections
{
    public class TarefaCollection
    {
        [BsonId]
        public Guid TarefaId { get; set; }
        public Guid SetorId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Prioridade { get; set; }        
    }
}
