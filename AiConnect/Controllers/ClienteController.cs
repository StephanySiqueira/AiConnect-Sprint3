using AiConnect.DTOs;
using AiConnect.Models;
using AiConnect.Repositories;
using AiConnect.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteRepository _clienteRepository;
    private readonly AppConfigurationManager _configurationManager;

    public ClienteController(IClienteRepository clienteRepository, AppConfigurationManager configurationManager)
    {
        _clienteRepository = clienteRepository;
        _configurationManager = configurationManager;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClienteDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetClientes()
    {
        try
        {
            var clientes = await _clienteRepository.GetAllClientesAsync();
            return Ok(clientes);
        }
        catch (Exception ex)
        {
            // Adicione tratamento de exceção se necessário
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetCliente(int id)
    {
        try
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound(new ErrorResponse { Message = "Cliente não encontrado." });
            }

            return Ok(cliente);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClienteDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> PostCliente(ClienteDTO clienteDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _clienteRepository.AddClienteAsync(clienteDTO);
            return CreatedAtAction(nameof(GetCliente), new { id = clienteDTO.Id }, clienteDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> PutCliente(int id, ClienteDTO clienteDTO)
    {
        if (id != clienteDTO.Id)
        {
            return BadRequest(new ErrorResponse { Message = "ID do cliente não encontrado." });
        }

        try
        {
            await _clienteRepository.UpdateClienteAsync(clienteDTO);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ErrorResponse { Message = "Cliente não encontrado." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        try
        {
            await _clienteRepository.DeleteClienteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ErrorResponse { Message = "Cliente não encontrado." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
        }
    }

    [HttpGet("configuracao")]
    public IActionResult GetConfiguracao()
    {
        
        var connectionString = _configurationManager.ConnectionString;
        var maxFileSize = _configurationManager.MaxUploadFileSize;

        return Ok(new { connectionString, maxFileSize });
    }
}
