using System;

namespace ProspEco.Model.DTO.Response
{
    public class ConquistaResponse
    {
        public long IdConquista { get; set; }

        public DateTime DataConquista { get; set; }

        public string Descricao { get; set; }

        public string Titulo { get; set; }

        public long IdUsuario { get; set; }

        public DateTime DtCriacao { get; set; }

        public DateTime? DtModificacao { get; set; }
    }
}