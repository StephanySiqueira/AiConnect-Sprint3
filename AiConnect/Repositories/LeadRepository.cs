using AiConnect.DTOs;
using AiConnect.Models;
using AiConnect.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AiConnect.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private readonly OracleDbContext _context;

        public LeadRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeadDTO>> GetAllLeadsAsync()
        {
            return await _context.Leads
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
        }

        public async Task<LeadDTO> GetLeadByIdAsync(int id)
        {
            return await _context.Leads
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
        }

        public async Task AddLeadAsync(LeadDTO leadDTO)
        {
            var lead = new Lead
            {
                Nome = leadDTO.Nome,
                Telefone = leadDTO.Telefone,
                Email = leadDTO.Email,
                Cargo = leadDTO.Cargo,
                Empresa = leadDTO.Empresa,
                ClienteId = leadDTO.ClienteId
            };

            _context.Leads.Add(lead);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLeadAsync(LeadDTO leadDTO)
        {
            var lead = await _context.Leads.FindAsync(leadDTO.Id);
            if (lead == null) throw new KeyNotFoundException();

            lead.Nome = leadDTO.Nome;
            lead.Telefone = leadDTO.Telefone;
            lead.Email = leadDTO.Email;
            lead.Cargo = leadDTO.Cargo;
            lead.Empresa = leadDTO.Empresa;
            lead.ClienteId = leadDTO.ClienteId;

            _context.Entry(lead).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLeadAsync(int id)
        {
            var lead = await _context.Leads.FindAsync(id);
            if (lead == null) throw new KeyNotFoundException();

            _context.Leads.Remove(lead);
            await _context.SaveChangesAsync();
        }
    }
}
