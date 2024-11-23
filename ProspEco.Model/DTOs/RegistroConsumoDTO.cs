using System;
using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTOs
{
    public class RegistroConsumoDTO
    {
        public long Id { get; set; }

        [Required]
        public double Consumo { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        public long AparelhoId { get; set; }
    }
}