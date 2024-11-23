using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTOs
{
    public class AparelhoDTO
    {
        public long Id { get; set; }

        [MaxLength(255)]
        public string Descricao { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        public double Potencia { get; set; }

        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; }

        [Required]
        public long UsuarioId { get; set; }
    }
}