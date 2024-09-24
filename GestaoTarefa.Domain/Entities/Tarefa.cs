using GestaoTarefa.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Domain.Entities
{
    public class Tarefa
    {
        public Guid TarefaId { get; private set; }
        public Guid SetorId { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public DateTime Data { get; private set; }
        public string Descricao { get; private set; } = string.Empty;
        public Prioridade Prioridade { get; private set; }

        #region Relacionamento

        public Setor? Setor { get; private set; }

        #endregion

        public Tarefa()
        {
            
        }

        private Tarefa(Guid tarefaId, Guid setorId, string nome, DateTime data, string descricao, Prioridade prioridade)
        {
            TarefaId = tarefaId;
            SetorId = setorId;
            Nome = nome;
            Data = data;
            Descricao = descricao;
            Prioridade = prioridade;

            Validate();
        }

        public static Tarefa Create(Guid tarefaId, Guid setorId, string nome, DateTime data, string descricao, Prioridade prioridade) 
        {
            return new Tarefa(tarefaId, setorId, nome, data, descricao, prioridade);
        }

        public void Update(Guid tarefaId, Guid setorId, string nome, DateTime data, string descricao, Prioridade prioridade)
        {
            TarefaId = tarefaId;
            SetorId = setorId;
            Nome = nome;
            Data = data;
            Descricao = descricao;
            Prioridade = prioridade;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                throw new Exception("Nome deve ser preenchido.");
            }

            if (string.IsNullOrEmpty(Descricao))
            {
                throw new Exception("Descrição deve ser preenchida.");
            }
        }
    }
}
