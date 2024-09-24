using AutoMapper;
using GestaoTarefa.Infra.Storage.Collections;
using GestaoTarefa.Infra.Storage.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Handlers.Notifications
{
    public class TarefaNotificationHandler : INotificationHandler<TarefaNotification>
    {
        //atributo
        private readonly TarefaPersistence _tarefaPersistence;
        private readonly IMapper _mapper;

        public TarefaNotificationHandler(TarefaPersistence tarefaPersistence, IMapper mapper)
        {
            _tarefaPersistence = tarefaPersistence;
            _mapper = mapper;
        }

        public async Task Handle(TarefaNotification notification, CancellationToken cancellationToken)
        {
            switch (notification.Action)
            {
                case NotificationAction.Criada:
                    await _tarefaPersistence.Insert(_mapper.Map<TarefaCollection>(notification.Tarefa));
                    break;

                case NotificationAction.Alterada:
                    await _tarefaPersistence.Update(_mapper.Map<TarefaCollection>(notification.Tarefa));
                    break;

                case NotificationAction.Excluida:
                    await _tarefaPersistence.Delete(_mapper.Map<TarefaCollection>(notification.Tarefa));
                    break;
            }

            await Task.CompletedTask;
        }
    }
}
