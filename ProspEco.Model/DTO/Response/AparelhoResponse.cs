using System;

namespace ProspEco.Model.DTO.Response
{
    public class AparelhoResponse
    {
        public long IdAparelho { get; set; }

        public string Descricao { get; set; }

        public string Nome { get; set; }

        public double Potencia { get; set; }

        public string Tipo { get; set; }

        public long IdUsuario { get; set; }

        public DateTime DtCriacao { get; set; }

        public DateTime? DtModificacao { get; set; }
    }
}