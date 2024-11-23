using System;

namespace ProspEco.Model.DTO.Response
{
    public class RecomendacaoResponse
    {
        public long IdRecomendacao { get; set; }

        public DateTime DataHora { get; set; }

        public string Mensagem { get; set; }

        public long IdUsuario { get; set; }

        public DateTime DtCriacao { get; set; }

        public DateTime? DtModificacao { get; set; }
    }
}