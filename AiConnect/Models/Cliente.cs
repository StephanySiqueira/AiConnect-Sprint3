using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiConnect.Models
{
    [Table("CLIENTE_AI_NOVO")]
    public class Cliente
    {
        [Key]
        [Column("ID_CLIENTE")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Column("NOME_CLIENTE")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Column("TELEFONE_CLIENTE", TypeName = "varchar(20)")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [Column("EMAIL_CLIENTE", TypeName = "varchar(255)")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [Column("DATA_NASCIMENTO")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [Column("ENDERECO_CLIENTE")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "A empresa é obrigatória.")]
        [Column("EMPRESA_CLIENTE")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "O segmento é obrigatório.")]
        [Column("SEGMENTO_MERCADO")]
        public string SegmentoMercado { get; set; }

        [Required(ErrorMessage = "O interesse é obrigatório.")]
        [Column("INTERESSES_CLIENTE")]
        public string Interesses { get; set; }
    }
}
