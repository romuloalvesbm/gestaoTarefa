using GestaoTarefa.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Handlers.Notifications
{
    public class SetorNotification : INotification
    {
        public SetorDto? Setor { get; set; }
        public NotificationAction Action { get; set; }
    }   
}