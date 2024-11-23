using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("prospecco_aparelhos")]
    public class Aparelho
    {
        [Key]
        [Column("id_aparelho")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdAparelho { get; set; }

        [Column("ds_descricao", TypeName = "varchar(255)")]
        public string DsDescricao { get; set; }

        [Required]
        [Column("ds_nome", TypeName = "varchar(100)")]
        public string DsNome { get; set; }

        [Required]
        [Column("vl_potencia", TypeName = "number(11,2)")]
        public double VlPotencia { get; set; }

        [Required]
        [Column("ds_tipo", TypeName = "varchar(50)")]
        public string DsTipo { get; set; }

        [Required]
        [Column("id_usuario", TypeName = "number(11)")]
        public long IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        public ICollection<RegistroConsumo> RegistrosConsumo { get; set; }

        [Column("dt_criacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime DtCriacao { get; set; }

        [Column("dt_modificacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime? DtModificacao { get; set; }
    }
}