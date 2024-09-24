using AutoMapper;
using Bogus;
using GestaoTarefa.Application.Commands.Tarefa;
using GestaoTarefa.Application.Dtos;
using GestaoTarefa.Application.Handlers.Notifications;
using GestaoTarefa.Application.Handlers.Requests;
using GestaoTarefa.Application.Validation;
using GestaoTarefa.Domain.Entities;
using GestaoTarefa.Domain.Enum;
using GestaoTarefa.Domain.Interfaces.Repositories;
using MediatR;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Tests.Application
{
    public class TarefaRequestHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TarefaRequestHandler _tarefaRequestHandler;
        private readonly TarefaCreateCommand _tarefaCreateCommandMock;
        //private readonly TarefaUpdateCommand _tarefaUpdateCommandMock;
        //private readonly TarefaDeleteCommand _tarefaDeleteCommandMock;
        private readonly Faker _faker = new();
        private readonly Tarefa _tarefa;
        private readonly TarefaDto _tarefaDto;
        private readonly TarefaNotification _tarefaNotification;

        public TarefaRequestHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _mediatorMock = new Mock<IMediator>();

            _tarefaRequestHandler = new TarefaRequestHandler(_mediatorMock.Object, _mapperMock.Object, _unitOfWorkMock.Object);

            _tarefaCreateCommandMock = new TarefaCreateCommand
            {
                Nome = _faker.Lorem.Sentence(3, 5)[..20].Trim(),
                Descricao = _faker.Lorem.Sentence(10)[..50].Trim(),
                Data = DateTime.Now,
                SetorId = _faker.Random.Guid(),
                Prioridade = Prioridade.Baixa
            };

            _tarefa = Tarefa.Create(Guid.NewGuid(), _tarefaCreateCommandMock.SetorId, _tarefaCreateCommandMock.Nome, _tarefaCreateCommandMock.Data,
                                                    _tarefaCreateCommandMock.Descricao, _tarefaCreateCommandMock.Prioridade);
            _tarefaDto = new TarefaDto
            {
                TarefaId = _tarefa.TarefaId,
                SetorId = _tarefa.SetorId,
                Nome = _tarefa.Nome,
                Data = _tarefa.Data,
                Descricao = _tarefa.Descricao,
                Prioridade = _tarefa.Prioridade
            };

            _tarefaNotification = new TarefaNotification
            {
                Tarefa = _tarefaDto,
                Action = NotificationAction.Criada,
            };

        }

        [Fact]
        public async Task ReturnoOkQuandoTarefaECriadaComSucesso()
        {
            _mapperMock.Setup(m => m.Map<Tarefa>(_tarefaCreateCommandMock)).Returns(_tarefa);
            _mapperMock.Setup(m => m.Map<TarefaDto>(_tarefa)).Returns(_tarefaDto);

            _unitOfWorkMock.Setup(u => u.TarefaRepository.Add(_tarefa));
            _unitOfWorkMock.Setup(u => u.SaveChanges());
            
            _mediatorMock.Setup(m => m.Publish(It.Is<TarefaNotification>(n => n.Tarefa == _tarefaNotification.Tarefa && n.Action == _tarefaNotification.Action),
                                               It.IsAny<CancellationToken>()));

            var result = await _tarefaRequestHandler.Handle(_tarefaCreateCommandMock, CancellationToken.None);

            Assert.Equal("Tarefa cadastrada com sucesso", result.Message);
          
            _unitOfWorkMock.Verify(u => u.TarefaRepository.Add(It.IsAny<Tarefa>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChanges(), Times.Once);
          
            _mediatorMock.Verify(m => m.Publish(It.IsAny<TarefaNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task RetornoErroQuandoFalhaAoAdicionarTarefa()
        {            
            _mapperMock.Setup(m => m.Map<Tarefa>(_tarefaCreateCommandMock)).Returns(_tarefa);
            _mapperMock.Setup(m => m.Map<TarefaDto>(_tarefa)).Returns(_tarefaDto);
            
            _unitOfWorkMock.Setup(u => u.TarefaRepository.Add(_tarefa)).ThrowsAsync(new Exception("Erro ao incluir tarefa"));           
           
            var result = await Assert.ThrowsAsync<Exception>(() => _tarefaRequestHandler.Handle(_tarefaCreateCommandMock, CancellationToken.None));
                       
            Assert.Equal("Erro ao incluir tarefa", result.Message);                  

            _unitOfWorkMock.Verify(u => u.TarefaRepository.Add(It.IsAny<Tarefa>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChanges(), Times.Never);

            _mediatorMock.Verify(m => m.Publish(It.IsAny<TarefaNotification>(), It.IsAny<CancellationToken>()), Times.Never);           
        }
    }
}
