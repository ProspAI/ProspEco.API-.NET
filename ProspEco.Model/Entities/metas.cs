using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("metas")]
    public class Meta
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("atingida")]
        public bool Atingida { get; set; }

        [Required]
        [Column("consumo_alvo")]
        public double ConsumoAlvo { get; set; }

        [Required]
        [Column("data_fim")]
        public DateTime DataFim { get; set; }

        [Required]
        [Column("data_inicio")]
        public DateTime DataInicio { get; set; }

        [Required]
        [Column("usuario_id")]
        public long UsuarioId { get; set; }

        // Relacionamentos
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
    }
}