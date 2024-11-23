using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("aparelhos")]
    public class Aparelho
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [MaxLength(255)]
        [Column("descricao")]
        public string Descricao { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nome")]
        public string Nome { get; set; }

        [Required]
        [Column("potencia")]
        public double Potencia { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("tipo")]
        public string Tipo { get; set; }

        [Required]
        [Column("usuario_id")]
        public long UsuarioId { get; set; }

        // Relacionamentos
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        public ICollection<RegistroConsumo> RegistrosConsumo { get; set; }
    }
}