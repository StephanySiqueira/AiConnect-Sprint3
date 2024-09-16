namespace AiConnect.DTOs
{
    public class LeadDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
        public int ClienteId { get; set; }
    }
}

