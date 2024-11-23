using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("prospecco_registros_consumo")]
    public class RegistroConsumo
    {
        [Key]
        [Column("id_registro", TypeName = "number(11)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdRegistro { get; set; }

        [Required]
        [Column("vl_consumo", TypeName = "number(11,2)")]
        public double VlConsumo { get; set; }

        [Required]
        [Column("dt_hora", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime DtHora { get; set; }

        [Required]
        [Column("id_aparelho", TypeName = "number(11)")]
        public long IdAparelho { get; set; }

        // Relacionamento com Aparelho
        [ForeignKey("IdAparelho")]
        public Aparelho Aparelho { get; set; }

        [Column("dt_criacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime DtCriacao { get; set; }

        [Column("dt_modificacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime? DtModificacao { get; set; }
    }
}