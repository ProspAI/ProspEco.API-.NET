using System;
using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTO.Request
{
    public class NotificacaoRequest
    {
        [Required(ErrorMessage = "A data e hora são obrigatórias.")]
        [DataType(DataType.DateTime, ErrorMessage = "A data e hora devem ser válidas.")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "O status de leitura é obrigatório.")]
        public bool Lida { get; set; }

        [MaxLength(255, ErrorMessage = "A mensagem deve conter no máximo 255 caracteres.")]
        public string Mensagem { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public long UsuarioId { get; set; }
    }
}