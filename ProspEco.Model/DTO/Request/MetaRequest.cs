using System;
using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTO.Request
{
    public class MetaRequest
    {
        [Required(ErrorMessage = "O campo 'atingida' é obrigatório.")]
        public bool Atingida { get; set; }

        [Required(ErrorMessage = "O consumo-alvo é obrigatório.")]
        [Range(0.01, 9999999.99, ErrorMessage = "O consumo-alvo deve estar entre 0.01 e 9999999.99.")]
        public double ConsumoAlvo { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "A data de início deve ser uma data válida.")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "A data de fim deve ser uma data válida.")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public long UsuarioId { get; set; }
    }
}