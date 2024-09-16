using AiConnect.Models;
using Microsoft.EntityFrameworkCore;
namespace AiConnect.Persistence
{
    public class OracleDbContext : DbContext
    {

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Interacoes> Interacoes { get; set; }

        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options)
        {
        }
    }
}