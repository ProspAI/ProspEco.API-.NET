using System;
using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTOs
{
    public class MetaDTO
    {
        public long Id { get; set; }

        [Required]
        public bool Atingida { get; set; }

        [Required]
        public double ConsumoAlvo { get; set; }

        [Required]
        public DateTime DataFim { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public long UsuarioId { get; set; }
    }
}