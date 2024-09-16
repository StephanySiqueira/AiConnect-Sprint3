using AiConnect.DTOs;

namespace AiConnect.Repositories
{
    public interface ILeadRepository
    {
        Task<IEnumerable<LeadDTO>> GetAllLeadsAsync();
        Task<LeadDTO> GetLeadByIdAsync(int id);
        Task AddLeadAsync(LeadDTO leadDTO);
        Task UpdateLeadAsync(LeadDTO leadDTO);
        Task DeleteLeadAsync(int id);
    }
}
