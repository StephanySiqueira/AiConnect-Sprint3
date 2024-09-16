using AiConnect.DTOs;
using AiConnect.Models;
using AiConnect.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AiConnect.Repositories
{
    public class InteracaoRepository : IInteracaoRepository
    {
        private readonly OracleDbContext _context;

        public InteracaoRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InteracoesDTO>> GetAllInteracoesAsync()
        {
            return await _context.Interacoes
                .Include(i => i.Cliente)
                .Include(i => i.Lead)
                .Select(i => new InteracoesDTO
                {
                    Id = i.Id,
                    DataInteracao = i.DataInteracao,
                    Tipo = i.Tipo,
                    Descricao = i.Descricao,
                    ClienteId = i.ClienteId,
                    LeadId = i.LeadId
                })
                .ToListAsync();
        }

        public async Task<InteracoesDTO> GetInteracaoByIdAsync(int id)
        {
            return await _context.Interacoes
                .Include(i => i.Cliente)
                .Include(i => i.Lead)
                .Where(i => i.Id == id)
                .Select(i => new InteracoesDTO
                {
                    Id = i.Id,
                    DataInteracao = i.DataInteracao,
                    Tipo = i.Tipo,
                    Descricao = i.Descricao,
                    ClienteId = i.ClienteId,
                    LeadId = i.LeadId
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddInteracaoAsync(InteracoesDTO interacaoDTO)
        {
            var interacao = new Interacoes
            {
                DataInteracao = interacaoDTO.DataInteracao,
                Tipo = interacaoDTO.Tipo,
                Descricao = interacaoDTO.Descricao,
                ClienteId = interacaoDTO.ClienteId,
                LeadId = interacaoDTO.LeadId
            };

            _context.Interacoes.Add(interacao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInteracaoAsync(InteracoesDTO interacaoDTO)
        {
            var interacao = await _context.Interacoes.FindAsync(interacaoDTO.Id);
            if (interacao == null) throw new KeyNotFoundException();

            interacao.DataInteracao = interacaoDTO.DataInteracao;
            interacao.Tipo = interacaoDTO.Tipo;
            interacao.Descricao = interacaoDTO.Descricao;
            interacao.ClienteId = interacaoDTO.ClienteId;
            interacao.LeadId = interacaoDTO.LeadId;

            _context.Entry(interacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInteracaoAsync(int id)
        {
            var interacao = await _context.Interacoes.FindAsync(id);
            if (interacao == null) throw new KeyNotFoundException();

            _context.Interacoes.Remove(interacao);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeadDTO>> GetLeadsByClientIdAsync(int clientId)
        {
            return await _context.Leads
                .Where(l => l.ClienteId == clientId)
                .Select(l => new LeadDTO
                {
                    Id = l.Id,
                    Nome = l.Nome
                })
                .ToListAsync();
        }
    }
}
