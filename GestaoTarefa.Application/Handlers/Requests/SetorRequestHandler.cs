using AutoMapper;
using GestaoTarefa.Application.Commands.Setor;
using GestaoTarefa.Application.Commands.Tarefa;
using GestaoTarefa.Application.Dtos;
using GestaoTarefa.Application.Handlers.Notifications;
using GestaoTarefa.Application.Validation;
using GestaoTarefa.Domain.Entities;
using GestaoTarefa.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Handlers.Requests
{
    public class SetorRequestHandler :
        IRequestHandler<SetorCreateCommand, Result>,
        IRequestHandler<SetorUpdateCommand, Result>,
        IRequestHandler<SetorDeleteCommand, Result>

    {
        //atributo
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SetorRequestHandler(IMediator mediator, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }      

        public async Task<Result> Handle(SetorCreateCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.SetorRepository.GetByName(request.Nome) == null)
            {               
                var setor = _mapper.Map<Setor>(request); 
                await _unitOfWork.SetorRepository.Add(setor);
                await _unitOfWork.SaveChanges();

                var setorDto = _mapper.Map<SetorDto>(setor);
                var setorNotification = new SetorNotification
                {
                    Setor = setorDto,
                    Action = NotificationAction.Criada
                };
                await _mediator.Publish(setorNotification, cancellationToken);

                return Result.Ok("Setor cadastrado com sucesso");
            }
            else
            {
                return Result.Fail("Setor já cadastrado");
            }
        }

        public async Task<Result> Handle(SetorUpdateCommand request, CancellationToken cancellationToken)
        {
            var setor = await _unitOfWork.SetorRepository.GetById(request.SetorId);

            if (setor != null)
            {
                var duplicate = await _unitOfWork.SetorRepository.GetAll(x => x.SetorId != request.SetorId && x.Nome == request.Nome);

                if (duplicate == null)
                {
                    _mapper.Map(request, setor);
                    await _unitOfWork.SetorRepository.Update(setor);
                    await _unitOfWork.SaveChanges();

                    var setorDto = _mapper.Map<SetorDto>(setor);
                    var setorNotification = new SetorNotification
                    {
                        Setor = setorDto,
                        Action = NotificationAction.Alterada
                    };
                    await _mediator.Publish(setorNotification, cancellationToken);

                    return Result.Ok("Setor atualizado com sucesso");
                }
                else
                {
                    return Result.Fail("Setor já cadastrado.");
                }
            }
            else
                return Result.Fail("Setor não encontrado.");
        }

        public async Task<Result> Handle(SetorDeleteCommand request, CancellationToken cancellationToken)
        {
            var setor = await _unitOfWork.SetorRepository.GetById(request.SetorId);

            if (setor != null)
            {
                await _unitOfWork.SetorRepository.Delete(setor);
                await _unitOfWork.SaveChanges();

                var setorDto = _mapper.Map<SetorDto>(setor);
                var setorNotification = new SetorNotification
                {
                    Setor = setorDto,
                    Action = NotificationAction.Excluida
                };
                await _mediator.Publish(setorNotification, cancellationToken);

                return Result.Ok("Setor excluído com sucesso");
            }
            else
            {
                return Result.Fail("Setor não encontrado");
            }
        }
    }
}
