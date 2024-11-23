using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("prospecco_notificacoes")]
    public class Notificacao
    {
        [Key]
        [Column("id_notificacao", TypeName = "number(11)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdNotificacao { get; set; }

        [Required]
        [Column("dt_hora", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime DtHora { get; set; }

        [Required]
        [Column("fl_lida", TypeName = "char(1)")]
        public bool FlLida { get; set; }

        [Column("ds_mensagem", TypeName = "varchar(255)")]
        public string DsMensagem { get; set; }

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