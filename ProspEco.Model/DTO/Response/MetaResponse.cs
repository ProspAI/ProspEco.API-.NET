using System;

namespace ProspEco.Model.DTO.Response
{
    public class MetaResponse
    {
        public long IdMeta { get; set; }

        public bool Atingida { get; set; }

        public double ConsumoAlvo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public long IdUsuario { get; set; }

        public DateTime DtCriacao { get; set; }

        public DateTime? DtModificacao { get; set; }
    }
}