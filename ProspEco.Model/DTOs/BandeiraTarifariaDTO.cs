using System;
using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTOs
{
    public class BandeiraTarifariaDTO
    {
        public long Id { get; set; }

        [Required]
        public DateTime DataVigencia { get; set; }

        [Required]
        [MaxLength(20)]
        public string TipoBandeira { get; set; }
    }
}