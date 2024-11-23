using System;

namespace ProspEco.Model.DTO.Response
{
    public class NotificacaoResponse
    {
        public long IdNotificacao { get; set; }

        public DateTime DataHora { get; set; }

        public bool Lida { get; set; }

        public string Mensagem { get; set; }

        public long IdUsuario { get; set; }

        public DateTime DtCriacao { get; set; }

        public DateTime? DtModificacao { get; set; }
    }
}