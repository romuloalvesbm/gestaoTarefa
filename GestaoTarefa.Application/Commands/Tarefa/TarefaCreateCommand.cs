using GestaoTarefa.Application.Validation;
using GestaoTarefa.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Commands.Tarefa
{
    public class TarefaCreateCommand : IRequest<Result>
    {

        [Required(ErrorMessage = "Informe o id do setor.")]
        public Guid SetorId { get; set; }

        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o nome da tarefa.")]
        public required string Nome { get; set; }
       
        [Required(ErrorMessage = "Informe a data da tarefa.")]
        public DateTime Data { get; set; }

        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(250, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe a descrição da tarefa.")]
        public required string Descricao { get; set; }

        [Required(ErrorMessage = "A prioridade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A prioridade deve estar entre 1 e 3 (BAIXA=1, MEDIA=2, ALTA=3)")]     
        public Prioridade Prioridade { get; set; }
    }
}
