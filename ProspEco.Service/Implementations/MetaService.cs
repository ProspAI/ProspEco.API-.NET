using AutoMapper;
using ProspEco.Model.DTOs;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using ProspEco.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Implementations
{
    public class MetaService : IMetaService
    {
        private readonly IMetaRepository _metaRepository;
        private readonly IMapper _mapper;

        public MetaService(IMetaRepository metaRepository, IMapper mapper)
        {
            _metaRepository = metaRepository;
            _mapper = mapper;
        }

        public async Task<MetaDTO> CreateMetaAsync(MetaDTO metaDTO)
        {
            var meta = _mapper.Map<Meta>(metaDTO);
            await _metaRepository.AddAsync(meta);
            return _mapper.Map<MetaDTO>(meta);
        }

        public async Task DeleteMetaAsync(long id)
        {
            await _metaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MetaDTO>> GetAllMetasAsync()
        {
            var metas = await _metaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MetaDTO>>(metas);
        }

        public async Task<MetaDTO> GetActiveMetaAsync(long usuarioId)
        {
            var meta = await _metaRepository.GetActiveMetaAsync(usuarioId);
            return _mapper.Map<MetaDTO>(meta);
        }

        public async Task<MetaDTO> GetMetaByIdAsync(long id)
        {
            var meta = await _metaRepository.GetByIdAsync(id);
            return _mapper.Map<MetaDTO>(meta);
        }

        public async Task<IEnumerable<MetaDTO>> GetMetasByUsuarioIdAsync(long usuarioId)
        {
            var metas = await _metaRepository.GetByUsuarioIdAsync(usuarioId);
            return _mapper.Map<IEnumerable<MetaDTO>>(metas);
        }

        public async Task UpdateMetaAsync(long id, MetaDTO metaDTO)
        {
            var metaExistente = await _metaRepository.GetByIdAsync(id);
            if (metaExistente != null)
            {
                _mapper.Map(metaDTO, metaExistente);
                await _metaRepository.UpdateAsync(metaExistente);
            }
            else
            {
                // Opcional: lançar exceção ou retornar um resultado indicando que a meta não foi encontrada
                // throw new NotFoundException($"Meta com ID {id} não foi encontrada.");
            }
        }
    }
}
