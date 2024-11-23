using System;
using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTO.Request
{
    public class ConquistaRequest
    {
        [Required(ErrorMessage = "A data da conquista é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "A data da conquista deve ser uma data válida.")]
        public DateTime DataConquista { get; set; }

        [MaxLength(255, ErrorMessage = "A descrição deve conter no máximo 255 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O título deve conter no máximo 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public long UsuarioId { get; set; }
    }
}