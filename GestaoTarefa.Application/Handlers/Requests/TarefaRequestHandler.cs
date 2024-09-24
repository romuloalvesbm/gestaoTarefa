using AutoMapper;
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
    public class TarefaRequestHandler :
        IRequestHandler<TarefaCreateCommand, Result>,
        IRequestHandler<TarefaUpdateCommand, Result>,
        IRequestHandler<TarefaDeleteCommand, Result>
    {
        //atributo
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TarefaRequestHandler(IMediator mediator, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(TarefaCreateCommand request, CancellationToken cancellationToken)
        {
            var tarefa = _mapper.Map<Tarefa>(request);
            await _unitOfWork.TarefaRepository.Add(tarefa);
            await _unitOfWork.SaveChanges();

            var tarefaDto = _mapper.Map<TarefaDto>(tarefa);
            var tarefaNotification = new TarefaNotification
            {
                Tarefa = tarefaDto,
                Action = NotificationAction.Criada
            };
            await _mediator.Publish(tarefaNotification, cancellationToken);

            return Result.Ok("Tarefa cadastrada com sucesso");

        }

        public async Task<Result> Handle(TarefaUpdateCommand request, CancellationToken cancellationToken)
        {
            var tarefa = await _unitOfWork.TarefaRepository.GetById(request.TarefaId);

            if (tarefa != null)
            {
                _mapper.Map(request, tarefa);
                await _unitOfWork.TarefaRepository.Update(tarefa);
                await _unitOfWork.SaveChanges();

                var tarefaDto = _mapper.Map<TarefaDto>(tarefa);
                var tarefaNotification = new TarefaNotification
                {
                    Tarefa = tarefaDto,
                    Action = NotificationAction.Alterada
                };
                await _mediator.Publish(tarefaNotification, cancellationToken);

                return Result.Ok("Tarefa atualizado com sucesso");
            }
            else
                return Result.Fail("Tarefa não encontrada.");
        }

        public async Task<Result> Handle(TarefaDeleteCommand request, CancellationToken cancellationToken)
        {
            var tarefa = await _unitOfWork.TarefaRepository.GetById(request.TarefaId);

            if (tarefa != null)
            {
                await _unitOfWork.TarefaRepository.Delete(tarefa);
                await _unitOfWork.SaveChanges();

                var tarefaDto = _mapper.Map<TarefaDto>(tarefa);
                var tarefaNotification = new TarefaNotification
                {
                    Tarefa = tarefaDto,
                    Action = NotificationAction.Excluida
                };
                await _mediator.Publish(tarefaNotification, cancellationToken);

                return Result.Ok("Tarefa excluída com sucesso");
            }
            else
            {
                return Result.Fail("Tarefa não encontrada");
            }
        }
    }
}
