using GestaoTarefa.Application.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Commands.Tarefa
{
    public class TarefaDeleteCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Informe o id da tarefa.")]
        public Guid TarefaId { get; set; }
    }
}
