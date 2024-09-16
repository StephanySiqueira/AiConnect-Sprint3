using AiConnect.DTOs;
using AiConnect.Models;
using AiConnect.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AiConnect.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly OracleDbContext _context;

        public ClienteRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllClientesAsync()
        {
            return await _context.Clientes
                .Select(c => new ClienteDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Telefone = c.Telefone,
                    Email = c.Email,
                    DataNascimento = c.DataNascimento,
                    Endereco = c.Endereco,
                    Empresa = c.Empresa,
                    SegmentoMercado = c.SegmentoMercado,
                    Interesses = c.Interesses
                })
                .ToListAsync();
        }

        public async Task<ClienteDTO> GetClienteByIdAsync(int id)
        {
            return await _context.Clientes
                .Where(c => c.Id == id)
                .Select(c => new ClienteDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Telefone = c.Telefone,
                    Email = c.Email,
                    DataNascimento = c.DataNascimento,
                    Endereco = c.Endereco,
                    Empresa = c.Empresa,
                    SegmentoMercado = c.SegmentoMercado,
                    Interesses = c.Interesses
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddClienteAsync(ClienteDTO clienteDTO)
        {
            var cliente = new Cliente
            {
                Nome = clienteDTO.Nome,
                Telefone = clienteDTO.Telefone,
                Email = clienteDTO.Email,
                DataNascimento = clienteDTO.DataNascimento,
                Endereco = clienteDTO.Endereco,
                Empresa = clienteDTO.Empresa,
                SegmentoMercado = clienteDTO.SegmentoMercado,
                Interesses = clienteDTO.Interesses
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClienteAsync(ClienteDTO clienteDTO)
        {
            var cliente = await _context.Clientes.FindAsync(clienteDTO.Id);
            if (cliente == null) throw new KeyNotFoundException();

            cliente.Nome = clienteDTO.Nome;
            cliente.Telefone = clienteDTO.Telefone;
            cliente.Email = clienteDTO.Email;
            cliente.DataNascimento = clienteDTO.DataNascimento;
            cliente.Endereco = clienteDTO.Endereco;
            cliente.Empresa = clienteDTO.Empresa;
            cliente.SegmentoMercado = clienteDTO.SegmentoMercado;
            cliente.Interesses = clienteDTO.Interesses;

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) throw new KeyNotFoundException();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
