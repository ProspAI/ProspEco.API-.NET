using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("recomendacoes")]
    public class Recomendacao
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("data_hora")]
        public DateTime DataHora { get; set; }

        [Column("mensagem")]
        public string Mensagem { get; set; }

        [Required]
        [Column("usuario_id")]
        public long UsuarioId { get; set; }

        // Relacionamentos
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
    }
}