using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("conquistas")]
    public class Conquista
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("data_conquista")]
        public DateTime DataConquista { get; set; }

        [MaxLength(255)]
        [Column("descricao")]
        public string Descricao { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("titulo")]
        public string Titulo { get; set; }

        [Required]
        [Column("usuario_id")]
        public long UsuarioId { get; set; }

        // Relacionamentos
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
    }
}