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
    public class SetorNotificationHandler : INotificationHandler<SetorNotification>
    {
        //atributo
        private readonly SetorPersistence _setorPersistence;
        private readonly IMapper _mapper;

        public SetorNotificationHandler(SetorPersistence setorPersistence, IMapper mapper)
        {
            _setorPersistence = setorPersistence;
            _mapper = mapper;
        }

        public async Task Handle(SetorNotification notification, CancellationToken cancellationToken)
        {
            switch (notification.Action)
            {
                case NotificationAction.Criada:
                    var teste = _mapper.Map<SetorCollection>(notification.Setor);
                    await _setorPersistence.Insert(teste);
                    break;

                case NotificationAction.Alterada:
                    await _setorPersistence.Update(_mapper.Map<SetorCollection>(notification.Setor));
                    break;

                case NotificationAction.Excluida:
                    await _setorPersistence.Delete(_mapper.Map<SetorCollection>(notification.Setor));
                    break;
            }

            await Task.CompletedTask;
        }
    }
}
