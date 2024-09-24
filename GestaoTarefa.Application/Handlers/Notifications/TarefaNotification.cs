using GestaoTarefa.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Handlers.Notifications
{
    public class TarefaNotification : INotification
    {
        public TarefaDto? Tarefa { get; set; }
        public NotificationAction Action { get; set; }
    }

    public enum NotificationAction
    {
        Criada = 1,
        Alterada = 2,
        Excluida = 3
    }
}
