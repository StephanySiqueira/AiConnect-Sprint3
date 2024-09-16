
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiConnect.Models
{
    [Table("LEAD_AI_NOVO")]
    public class Lead
    {
        [Key]
        [Column("ID_LEAD")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Column("NOME_LEAD")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Column("TELEFONE_LEAD", TypeName = "varchar(20)")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [Column("EMAIL_LEAD", TypeName = "varchar(255)")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "O cargo é obrigatório.")]
        [Column("CARGO_LEAD")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "A empresa é obrigatória.")]
        [Column("EMPRESA_LEAD")]
        public string Empresa { get; set; }

        [ForeignKey("Cliente")]
        [Column("ID_CLIENTE")]
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
