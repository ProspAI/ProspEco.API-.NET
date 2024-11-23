namespace ProspEco.Model.DTO.Response
{
    public class UsuarioResponse
    {
        public long IdUsuario { get; set; }

        public string Email { get; set; }

        public string Nome { get; set; }

        public double? PontuacaoEconomia { get; set; }

        public string Role { get; set; }

        public DateTime DtCriacao { get; set; }

        public DateTime? DtModificacao { get; set; }
    }
}