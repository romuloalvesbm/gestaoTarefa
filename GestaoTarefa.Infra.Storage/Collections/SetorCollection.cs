using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Infra.Storage.Collections
{
    public class SetorCollection
    {
        [BsonId]
        public Guid SetorId { get; set; }
        public string Nome { get; set; } = string.Empty;

        #region Relacionamento

        public List<Guid> TarefasIds { get; set; } = [];

        #endregion
    }
}
