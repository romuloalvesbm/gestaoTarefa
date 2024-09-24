using GestaoTarefa.Application.Commands.Tarefa;
using GestaoTarefa.Application.Dtos;
using GestaoTarefa.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Interfaces
{
    public interface ITarefaAppService
    {
        Task<Result> Create(TarefaCreateCommand command);
        Task<Result> Update(TarefaUpdateCommand command);
        Task<Result> Delete(TarefaDeleteCommand command);
        
        Task<Result<ICollection<TarefaDto>>> GetAll();
        Task<Result<TarefaDto>> GetById(Guid id);
    }
}
