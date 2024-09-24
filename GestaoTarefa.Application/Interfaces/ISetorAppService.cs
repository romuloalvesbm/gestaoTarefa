using GestaoTarefa.Application.Commands.Setor;
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
    public interface ISetorAppService
    {
        Task<Result> Create(SetorCreateCommand command);
        Task<Result> Update(SetorUpdateCommand command);
        Task<Result> Delete(SetorDeleteCommand command);

        Task<Result<ICollection<SetorDto>>> GetAll();
        Task<Result<SetorDto>> GetById(Guid id);
    }
}
