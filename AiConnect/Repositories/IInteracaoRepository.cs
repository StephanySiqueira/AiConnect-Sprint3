using AiConnect.DTOs;

namespace AiConnect.Repositories
{
    public interface IInteracaoRepository
    {
        Task<IEnumerable<InteracoesDTO>> GetAllInteracoesAsync();
        Task<InteracoesDTO> GetInteracaoByIdAsync(int id);
        Task AddInteracaoAsync(InteracoesDTO interacaoDTO);
        Task UpdateInteracaoAsync(InteracoesDTO interacaoDTO);
        Task DeleteInteracaoAsync(int id);
        Task<IEnumerable<LeadDTO>> GetLeadsByClientIdAsync(int clientId);

    }
}
