using AutoMapper;
using GestaoTarefa.Application.Commands.Setor;
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
    public class SetorAppService : ISetorAppService
    {
        //atributo
        private readonly SetorPersistence _setorPersistence;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SetorAppService(SetorPersistence setorPersistence, IMediator mediator, IMapper mapper)
        {
            _setorPersistence = setorPersistence;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Result> Create(SetorCreateCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<Result> Update(SetorUpdateCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<Result> Delete(SetorDeleteCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<Result<ICollection<SetorDto>>> GetAll()
        {
            var result = await _setorPersistence.FindAll();
            return Result.Ok(_mapper.Map<ICollection<SetorDto>>(result));
        }

        public async Task<Result<SetorDto>> GetById(Guid id)
        {
            var result = await _setorPersistence.Find(id);

            if (result == null)
            {
                return Result.Fail<SetorDto>("Setor não encontrado.");
            }

            return Result.Ok(_mapper.Map<SetorDto>(result));
        }        
    }
}
