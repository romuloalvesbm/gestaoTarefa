using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Domain.Entities
{
    public class Setor
    {
        public Guid SetorId { get; private set; }
        public string Nome { get; private set; } = string.Empty;

        #region Relacionamento

        public ICollection<Tarefa> Tarefas { get; set; } = [];

        #endregion

        public Setor()
        {
            
        }

        private Setor(Guid setorId, string nome)
        {
            SetorId = setorId;
            Nome = nome;

            Validate();
        }

        public static Setor Create(Guid setorId, string nome) 
        {
            return new Setor(setorId, nome);
        }

        public void Update(Guid setorId, string nome)
        {
            SetorId = setorId;
            Nome = nome;
            
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                throw new Exception("Nome deve ser preenchido.");
            }
        }

    }
}
