using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("bandeiras_tarifarias")]
    public class BandeiraTarifaria
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("data_vigencia")]
        public DateTime DataVigencia { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("tipo_bandeira")]
        public string TipoBandeira { get; set; }
    }
}