using Azure.Core;
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
    public class SetorController : ControllerBase
    {
        private readonly ISetorAppService _setorAppService;

        public SetorController(ISetorAppService setorAppService)
        {
            _setorAppService = setorAppService;
        }

        /// <summary>
        /// Serviço para cadastro de setor.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SetorCreateCommand command)
        {
            try
            {
                var result = await _setorAppService.Create(command);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para atualiação de setor.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(SetorUpdateCommand command)
        {
            try
            {
                var result = await _setorAppService.Update(command);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para exclusão / inativação de setor.
        /// </summary>      
        [HttpDelete]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] SetorDeleteCommand command)
        {
            try
            {
                var result = await _setorAppService.Delete(command);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para consulta de setores.
        /// </summary>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(Result<ICollection<SetorDto>>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Result<ICollection<SetorDto>>>> GetAll()
        {
            try
            {
                var result = await _setorAppService.GetAll();
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para consulta de setor por id.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Result<SetorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Result<SetorDto>>> GetById(Guid id)
        {
            try
            {
                var result = await _setorAppService.GetById(id);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }
    }
}

