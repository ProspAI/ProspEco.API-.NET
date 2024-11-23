using System;
using System.ComponentModel.DataAnnotations;

namespace ProspEco.Model.DTOs
{
    public class RecomendacaoDTO
    {
        public long Id { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        public string Mensagem { get; set; }

        [Required]
        public long UsuarioId { get; set; }
    }
}