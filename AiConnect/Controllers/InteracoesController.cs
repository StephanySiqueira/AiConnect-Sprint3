using AiConnect.DTOs;
using AiConnect.Models;
using AiConnect.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InteracoesController : ControllerBase
    {
        private readonly IInteracaoRepository _interacaoRepository;

        public InteracoesController(IInteracaoRepository interacaoRepository)
        {
            _interacaoRepository = interacaoRepository;
        }

        // GET: api/Interacoes
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InteracoesDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetInteracoes()
        {
            try
            {
                var interacoes = await _interacaoRepository.GetAllInteracoesAsync();
                return Ok(interacoes);
            }
            catch (Exception)
            {
                // Adicione tratamento de exceção se necessário
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
            }
        }

        // GET: api/Interacoes/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InteracoesDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetInteracao(int id)
        {
            try
            {
                var interacao = await _interacaoRepository.GetInteracaoByIdAsync(id);
                if (interacao == null)
                {
                    return NotFound(new ErrorResponse { Message = "Interação não encontrada." });
                }

                return Ok(interacao);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
            }
        }

        // POST: api/Interacoes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InteracoesDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> PostInteracao([FromBody] InteracoesDTO interacaoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse { Message = "Dados inválidos fornecidos." });
            }

            try
            {
                await _interacaoRepository.AddInteracaoAsync(interacaoDto);
                return CreatedAtAction(nameof(GetInteracao), new { id = interacaoDto.Id }, interacaoDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Cliente ou Lead não encontrado! Forneça dados existentes na base" });
            }
        }

        // PUT: api/Interacoes/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> PutInteracao(int id, [FromBody] InteracoesDTO interacaoDto)
        {
            if (id != interacaoDto.Id || !ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse { Message = "ID da interação não corresponde ou dados inválidos fornecidos." });
            }

            try
            {
                await _interacaoRepository.UpdateInteracaoAsync(interacaoDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ErrorResponse { Message = "Interação não encontrada." });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
            }
        }

        // DELETE: api/Interacoes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteInteracao(int id)
        {
            try
            {
                await _interacaoRepository.DeleteInteracaoAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ErrorResponse { Message = "Interação não encontrada." });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
            }
        }

        // GET: api/Interacoes/leads/{clientId}
        [HttpGet("leads/{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LeadDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetLeadsByClientId(int clientId)
        {
            try
            {
                var leads = await _interacaoRepository.GetLeadsByClientIdAsync(clientId);
                if (leads == null)
                {
                    return NotFound(new ErrorResponse { Message = "Leads não encontrados para o cliente especificado." });
                }

                return Ok(leads);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
            }
        }
    }
}
