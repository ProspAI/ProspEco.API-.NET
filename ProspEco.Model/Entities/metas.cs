using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("prospecco_metas")]
    public class Meta
    {
        [Key]
        [Column("id_meta", TypeName = "number(11)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdMeta { get; set; }

        [Required]
        [Column("fl_atingida", TypeName = "char(1)")]
        public bool FlAtingida { get; set; }

        [Required]
        [Column("vl_consumo_alvo", TypeName = "number(11,2)")]
        public double VlConsumoAlvo { get; set; }

        [Required]
        [Column("dt_fim", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime DtFim { get; set; }

        [Required]
        [Column("dt_inicio", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime DtInicio { get; set; }

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