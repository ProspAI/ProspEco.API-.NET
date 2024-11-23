using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTO.Request
{
    public class UsuarioRequest
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail deve ser válido.")]
        [MaxLength(255, ErrorMessage = "O e-mail deve conter no máximo 255 caracteres.")]
        public string Email { get; set; }

        [MaxLength(255, ErrorMessage = "O nome deve conter no máximo 255 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres.")]
        public string Senha { get; set; }

        [MaxLength(255, ErrorMessage = "O papel (role) deve conter no máximo 255 caracteres.")]
        public string Role { get; set; }
    }
}