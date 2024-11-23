using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("prospecco_usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario", TypeName = "number(11)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdUsuario { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("ds_email", TypeName = "varchar(255)")]
        public string DsEmail { get; set; }

        [MaxLength(255)]
        [Column("ds_nome", TypeName = "varchar(255)")]
        public string DsNome { get; set; }

        [Column("vl_pontuacao_economia", TypeName = "number(11,2)")]
        public double? VlPontuacaoEconomia { get; set; }

        [MaxLength(255)]
        [Column("ds_role", TypeName = "varchar(255)")]
        public string DsRole { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("ds_senha", TypeName = "varchar(255)")]
        public string DsSenha { get; set; }

        // Relacionamentos
        public ICollection<Aparelho> Aparelhos { get; set; }
        public ICollection<Conquista> Conquistas { get; set; }
        public ICollection<Meta> Metas { get; set; }
        public ICollection<Notificacao> Notificacoes { get; set; }
        public ICollection<Recomendacao> Recomendacoes { get; set; }

        [Column("dt_criacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime DtCriacao { get; set; }

        [Column("dt_modificacao", TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime? DtModificacao { get; set; }
    }
}