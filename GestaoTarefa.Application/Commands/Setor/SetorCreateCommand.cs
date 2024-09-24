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
    public class SetorCreateCommand : IRequest<Result>
    {

        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o nome do setor.")]
        public required string Nome { get; set; }
    }
}
