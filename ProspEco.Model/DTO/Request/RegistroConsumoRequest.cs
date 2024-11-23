using System;
using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTO.Request
{
    public class RegistroConsumoRequest
    {
        [Required(ErrorMessage = "O consumo é obrigatório.")]
        [Range(0.01, 9999999.99, ErrorMessage = "O consumo deve ser maior que 0.")]
        public double Consumo { get; set; }

        [Required(ErrorMessage = "A data e hora são obrigatórias.")]
        [DataType(DataType.DateTime, ErrorMessage = "A data e hora devem ser válidas.")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "O ID do aparelho é obrigatório.")]
        public long AparelhoId { get; set; }
    }
}