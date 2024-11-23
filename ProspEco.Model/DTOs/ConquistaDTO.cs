using System;
using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTOs
{
    public class ConquistaDTO
    {
        public long Id { get; set; }

        [Required]
        public DateTime DataConquista { get; set; }

        [MaxLength(255)]
        public string Descricao { get; set; }

        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; }

        [Required]
        public long UsuarioId { get; set; }
    }
}