using GestaoTarefa.Application.Commands.Setor;
using GestaoTarefa.Application.Commands.Tarefa;
using GestaoTarefa.Application.Dtos;
using GestaoTarefa.Application.Interfaces;
using GestaoTarefa.Application.Services;
using GestaoTarefa.Application.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoTarefa.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        //atributo
        private readonly ITarefaAppService _tarefaAppService;

        //construtor para injeção de dependência
        public TarefasController(ITarefaAppService tarefaAppService)
        {
            _tarefaAppService = tarefaAppService;
        }

        /// <summary>
        /// Serviço para cadastro de tarefas.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(TarefaCreateCommand command)
        { 
            try
            {
                var result = await _tarefaAppService.Create(command);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para atualiação de tarefas.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(TarefaUpdateCommand command)
        {
            try
            {
                var result = await _tarefaAppService.Update(command);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para exclusão / inativação de tarefas.
        /// </summary>      
        [HttpDelete]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] TarefaDeleteCommand command)
        {
            try
            {
                var result = await _tarefaAppService.Delete(command);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para consulta de tarefas.
        /// </summary>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(Result<ICollection<TarefaDto>>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Result<ICollection<TarefaDto>>>> GetAll()
        {
            try
            {
                var result = await _tarefaAppService.GetAll();
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para consulta de tarefa por id.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Result<TarefaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Result<TarefaDto>>> GetById(Guid id)
        {
            try
            {
                var result = await _tarefaAppService.GetById(id);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }
    }
}
