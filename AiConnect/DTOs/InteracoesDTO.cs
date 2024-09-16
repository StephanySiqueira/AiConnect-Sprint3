namespace AiConnect.DTOs
{
    public class InteracoesDTO
    {
        public int Id { get; set; }
        public DateTime DataInteracao { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public int ClienteId { get; set; }  // Referência ao cliente
        public int LeadId { get; set; }     // Referência ao lead
    }
}

