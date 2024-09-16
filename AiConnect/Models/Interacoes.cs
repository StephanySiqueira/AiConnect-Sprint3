using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AiConnect.Models
{
    public enum TipoInteracao
    {
        Ligacao,
        Reuniao,
        Email,
        Visita
    }

    [Table("INTERACOES_AI_NOVO")]
    public class Interacoes
    {
        [Key]
        [Column("ID_INTERACAO_NOVO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data é obrigatória.")]
        [Column("DATA_INTERACAO")]
        public DateTime DataInteracao { get; set; }

        [Required(ErrorMessage = "O Tipo da interação é obrigatório.")]
        [Column("TIPO_INTERACAO", TypeName = "NVARCHAR2(2000)")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [Column("DESCRICAO_INTERACAO")]
        public string Descricao { get; set; }

        [ForeignKey("Cliente")]
        [Column("ID_CLIENTE")]
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        [ForeignKey("Lead")]
        [Column("ID_LEAD")]
        public int LeadId { get; set; }
        public Lead? Lead { get; set; }
    }
}
