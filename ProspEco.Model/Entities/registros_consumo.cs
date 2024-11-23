using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProspEco.Model.Entities
{
    [Table("registros_consumo")]
    public class RegistroConsumo
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("consumo")]
        public double Consumo { get; set; }

        [Required]
        [Column("data_hora")]
        public DateTime DataHora { get; set; }

        [Required]
        [Column("aparelho_id")]
        public long AparelhoId { get; set; }

        // Relacionamentos
        [ForeignKey("AparelhoId")]
        public Aparelho Aparelho { get; set; }
    }
}