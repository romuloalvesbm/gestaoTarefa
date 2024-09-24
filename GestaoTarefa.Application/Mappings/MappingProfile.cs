using AutoMapper;
using GestaoTarefa.Application.Commands.Setor;
using GestaoTarefa.Application.Commands.Tarefa;
using GestaoTarefa.Application.Dtos;
using GestaoTarefa.Application.Handlers.Notifications;
using GestaoTarefa.Domain.Entities;
using GestaoTarefa.Infra.Storage.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Setor

            CreateMap<SetorCreateCommand, Setor>()
                .ConvertUsing<SetorTypeConverter>();

            CreateMap<Setor, SetorDto>();

            CreateMap<SetorDto, SetorCollection>().ReverseMap();

            CreateMap<SetorUpdateCommand, Setor>()
                 .AfterMap((src, dest) => dest.Update(src.SetorId, src.Nome));

            #endregion

            #region Tarefa

            CreateMap<TarefaCreateCommand, Tarefa>()
                .ConvertUsing<TarefaTypeConverter>();

            CreateMap<Tarefa, TarefaDto>();

            CreateMap<TarefaDto, TarefaCollection>().ReverseMap();

            CreateMap<TarefaUpdateCommand, Tarefa>()
                 .AfterMap((src, dest) => dest.Update(src.TarefaId, src.SetorId, src.Nome, src.Data, src.Descricao, src.Prioridade));

            #endregion

        }

        public class SetorTypeConverter : ITypeConverter<SetorCreateCommand, Setor>
        {
            public Setor Convert(SetorCreateCommand source, Setor destination, ResolutionContext context)
            {
                return Setor.Create(Guid.NewGuid(), source.Nome);
            }
        }

        public class TarefaTypeConverter : ITypeConverter<TarefaCreateCommand, Tarefa>
        {
            public Tarefa Convert(TarefaCreateCommand source, Tarefa destination, ResolutionContext context)
            {
                return Tarefa.Create(Guid.NewGuid(), source.SetorId, source.Nome, source.Data, source.Descricao, source.Prioridade);
            }
        }
    }
}
