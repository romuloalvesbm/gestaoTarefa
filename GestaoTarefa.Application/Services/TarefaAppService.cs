using AutoMapper;
using GestaoTarefa.Application.Commands.Tarefa;
using GestaoTarefa.Application.Dtos;
using GestaoTarefa.Application.Interfaces;
using GestaoTarefa.Application.Validation;
using GestaoTarefa.Infra.Storage.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Services
{
    public class TarefaAppService : ITarefaAppService
    {
        //atributo
        private readonly TarefaPersistence _tarefaPersistence;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TarefaAppService(TarefaPersistence tarefaPersistence, IMediator mediator, IMapper mapper)
        {
            _tarefaPersistence = tarefaPersistence;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Result> Create(TarefaCreateCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<Result> Update(TarefaUpdateCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<Result> Delete(TarefaDeleteCommand command)
        {
            return await _mediator.Send(command);
        }             

        public async Task<Result<ICollection<TarefaDto>>> GetAll()
        {
            var result = await _tarefaPersistence.FindAll();
            return Result.Ok(_mapper.Map<ICollection<TarefaDto>>(result));
        }

        public async Task<Result<TarefaDto>> GetById(Guid id)
        {
            var result = await _tarefaPersistence.Find(id);

            if (result == null)
            {
                return Result.Fail<TarefaDto>("Tarefa não encontrada.");
            }

            return Result.Ok(_mapper.Map<TarefaDto>(result));
        }
    }
}
