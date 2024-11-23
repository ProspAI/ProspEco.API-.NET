using System;

namespace ProspEco.Model.DTO.Response
{
    public class BandeiraTarifariaResponse
    {
        public long IdBandeira { get; set; }

        public DateTime DataVigencia { get; set; }

        public string TipoBandeira { get; set; }

        public DateTime DtCriacao { get; set; }

        public DateTime? DtModificacao { get; set; }
    }
}