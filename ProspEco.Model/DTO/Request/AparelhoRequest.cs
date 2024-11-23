using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTO.Request
{
    public class AparelhoRequest
    {
        public long Id { get; set; }

        [MaxLength(255, ErrorMessage = "A descrição deve conter no máximo 255 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O nome do aparelho é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome do aparelho deve conter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A potência do aparelho é obrigatória.")]
        [Range(0.01, 999999.99, ErrorMessage = "A potência deve estar entre 0.01 e 999999.99.")]
        public double Potencia { get; set; }

        [Required(ErrorMessage = "O tipo do aparelho é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O tipo do aparelho deve conter no máximo 50 caracteres.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public long UsuarioId { get; set; }
    }
}