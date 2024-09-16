using AiConnect.DTOs;

namespace AiConnect.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteDTO>> GetAllClientesAsync();
        Task<ClienteDTO> GetClienteByIdAsync(int id);
        Task AddClienteAsync(ClienteDTO clienteDTO);
        Task UpdateClienteAsync(ClienteDTO clienteDTO);
        Task DeleteClienteAsync(int id);
    }
}
