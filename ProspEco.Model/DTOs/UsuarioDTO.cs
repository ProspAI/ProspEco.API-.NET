using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTOs
{
    public class UsuarioDTO
    {
        public long Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Nome { get; set; }

        public double? PontuacaoEconomia { get; set; }

        [MaxLength(255)]
        public string Role { get; set; }

        [Required]
        [MinLength(6)]
        public string Senha { get; set; } // Apenas para criação e atualização
    }
}