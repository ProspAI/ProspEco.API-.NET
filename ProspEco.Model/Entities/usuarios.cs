using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("email")]
        public string Email { get; set; }

        [MaxLength(255)]
        [Column("nome")]
        public string Nome { get; set; }

        [Column("pontuacao_economia")]
        public double? PontuacaoEconomia { get; set; }

        [MaxLength(255)]
        [Column("role")]
        public string Role { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("senha")]
        public string Senha { get; set; }

        // Relacionamentos
        public ICollection<Aparelho> Aparelhos { get; set; }
        public ICollection<Conquista> Conquistas { get; set; }
        public ICollection<Meta> Metas { get; set; }
        public ICollection<Notificacao> Notificacoes { get; set; }
        public ICollection<Recomendacao> Recomendacoes { get; set; }
    }
}