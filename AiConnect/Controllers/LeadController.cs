using AiConnect.DTOs;
using AiConnect.Models;
using AiConnect.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AiConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadController : ControllerBase
    {
        private readonly OracleDbContext _contexto;

        public LeadController(OracleDbContext contexto)
        {
            _contexto = contexto;
        }

        // GET: api/leads
        [HttpGet]
        public async Task<IActionResult> GetLeads()
        {
            try
            {
                var leads = await _contexto.Leads
                                           .Include(l => l.Cliente)
                                           .Select(l => new LeadDTO
                                           {
                                               Id = l.Id,
                                               Nome = l.Nome,
                                               Telefone = l.Telefone,
                                               Email = l.Email,
                                               Cargo = l.Cargo,
                                               Empresa = l.Empresa,
                                               ClienteId = l.ClienteId
                                           })
                                           .ToListAsync();
                return Ok(leads);
            }
            catch
            {
                // Retorna um erro 500 com a mensagem personalizada
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
            }
        }

        // GET: api/leads/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLead(int id)
        {
            try
            {
                var lead = await _contexto.Leads
                                          .Where(l => l.Id == id)
                                          .Select(l => new LeadDTO
                                          {
                                              Id = l.Id,
                                              Nome = l.Nome,
                                              Telefone = l.Telefone,
                                              Email = l.Email,
                                              Cargo = l.Cargo,
                                              Empresa = l.Empresa,
                                              ClienteId = l.ClienteId
                                          })
                                          .FirstOrDefaultAsync();

                if (lead == null)
                {
                    return NotFound(new ErrorResponse { Message = "Lead não encontrado." });
                }

                return Ok(lead);
            }
            catch
            {
                // Retorna um erro 500 com a mensagem personalizada
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
            }
        }

        // POST: api/leads
        [HttpPost]
        public async Task<IActionResult> CreateLead([FromBody] LeadDTO leadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var lead = new Lead
                {
                    Nome = leadDto.Nome,
                    Telefone = leadDto.Telefone,
                    Email = leadDto.Email,
                    Cargo = leadDto.Cargo,
                    Empresa = leadDto.Empresa,
                    ClienteId = leadDto.ClienteId
                };

                _contexto.Leads.Add(lead);
                await _contexto.SaveChangesAsync();

                return CreatedAtAction(nameof(GetLead), new { id = lead.Id }, leadDto);
            }
            catch
            {
                // Retorna um erro 500 com a mensagem personalizada
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse { Message = "Cliente não encontrado na base. Informe ClienteId válido!" });
            }
        }

        // PUT: api/leads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLead(int id, [FromBody] LeadDTO leadDto)
        {
            if (id != leadDto.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var lead = await _contexto.Leads.FindAsync(id);
                if (lead == null)
                {
                    return NotFound(new ErrorResponse { Message = "Lead não encontrado." });
                }

                lead.Nome = leadDto.Nome;
                lead.Telefone = leadDto.Telefone;
                lead.Email = leadDto.Email;
                lead.Cargo = leadDto.Cargo;
                lead.Empresa = leadDto.Empresa;
                lead.ClienteId = leadDto.ClienteId;

                _contexto.Update(lead);
                await _contexto.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeadExists(id))
                {
                    return NotFound(new ErrorResponse { Message = "Lead não encontrado." });
                }
                else
                {
                    // Retorna um erro 500 com a mensagem personalizada
                    return StatusCode(StatusCodes.Status500InternalServerError,
                                      new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
                }
            }
            catch
            {
                // Retorna um erro 500 com a mensagem personalizada
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
            }
        }

        // DELETE: api/leads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLead(int id)
        {
            try
            {
                var lead = await _contexto.Leads.FindAsync(id);
                if (lead == null)
                {
                    return NotFound(new ErrorResponse { Message = "Lead não encontrado." });
                }

                _contexto.Leads.Remove(lead);
                await _contexto.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                // Retorna um erro 500 com a mensagem personalizada
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse { Message = "ERRO DE REGISTRO DE ID - CHAVE MÃE VIOLADA" });
            }
        }

        private bool LeadExists(int id)
        {
            return _contexto.Leads.Any(e => e.Id == id);
        }
    }
}
