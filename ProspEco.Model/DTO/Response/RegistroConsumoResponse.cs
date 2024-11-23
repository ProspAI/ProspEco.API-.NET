using System;

namespace ProspEco.Model.DTO.Response
{
    public class RegistroConsumoResponse
    {
        public long IdRegistroConsumo { get; set; }

        public double Consumo { get; set; }

        public DateTime DataHora { get; set; }

        public long IdAparelho { get; set; }

        public DateTime DtCriacao { get; set; }

        public DateTime? DtModificacao { get; set; }
    }
}