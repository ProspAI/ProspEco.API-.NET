using System;
using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTO.Request
{
    public class BandeiraTarifariaRequest
    {
        [Required(ErrorMessage = "A data de vigência é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "A data de vigência deve ser uma data válida.")]
        public DateTime DataVigencia { get; set; }

        [Required(ErrorMessage = "O tipo de bandeira é obrigatório.")]
        [MaxLength(20, ErrorMessage = "O tipo de bandeira deve conter no máximo 20 caracteres.")]
        public string TipoBandeira { get; set; }
    }
}