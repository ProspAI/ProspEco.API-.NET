using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("prospecco_conquistas")]
    public class Conquista
    {
        [Key]
        [Column("id_conquista", TypeName = "number(11)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdConquista { get; set; }

        [Required]
        [Column("dt_conquista", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime DtConquista { get; set; }

        [Column("ds_descricao", TypeName = "varchar(255)")]
        public string DsDescricao { get; set; }

        [Required]
        [Column("ds_titulo", TypeName = "varchar(100)")]
        public string DsTitulo { get; set; }

        [Required]
        [Column("id_usuario", TypeName = "number(11)")]
        public long IdUsuario { get; set; }

        // Relacionamento com Usuário
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        [Column("dt_criacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime DtCriacao { get; set; }

        [Column("dt_modificacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime? DtModificacao { get; set; }
    }
}
