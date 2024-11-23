using AutoMapper;
using ProspEco.Model.DTOs;
using ProspEco.Model.Entities;

namespace ProspEco.API.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapear de Entidade para DTO e vice-versa

            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Aparelho, AparelhoDTO>().ReverseMap();
            CreateMap<BandeiraTarifaria, BandeiraTarifariaDTO>().ReverseMap();
            CreateMap<Conquista, ConquistaDTO>().ReverseMap();
            CreateMap<Meta, MetaDTO>().ReverseMap();
            CreateMap<Notificacao, NotificacaoDTO>().ReverseMap();
            CreateMap<Recomendacao, RecomendacaoDTO>().ReverseMap();
            CreateMap<RegistroConsumo, RegistroConsumoDTO>().ReverseMap();
        }
    }
}