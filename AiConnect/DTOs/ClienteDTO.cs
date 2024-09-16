namespace AiConnect.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }
        public string Empresa { get; set; }
        public string SegmentoMercado { get; set; }
        public string Interesses { get; set; }
    }
}

