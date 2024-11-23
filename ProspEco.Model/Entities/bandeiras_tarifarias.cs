using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("prospecco_bandeiras_tarifarias")]
    public class BandeiraTarifaria
    {
        [Key]
        [Column("id_bandeira", TypeName = "number(11)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdBandeira { get; set; }

        [Required]
        [Column("dt_vigencia", TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime DtVigencia { get; set; }

        [Required]
        [Column("ds_tipo_bandeira", TypeName = "varchar(20)")]
        public string DsTipoBandeira { get; set; }

        [Column("dt_criacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime DtCriacao { get; set; }

        [Column("dt_modificacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime? DtModificacao { get; set; }
    }
}