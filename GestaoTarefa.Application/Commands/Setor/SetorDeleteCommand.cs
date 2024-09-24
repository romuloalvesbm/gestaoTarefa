using GestaoTarefa.Application.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Commands.Setor
{
    public class SetorDeleteCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Informe o id do setor.")]
        public Guid SetorId { get; set; }
    }
}
