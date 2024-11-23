using AutoMapper;
using ProspEco.Model.DTOs;
using ProspEco.Model.Entities;
using ProspEco.Repository.Interfaces;
using ProspEco.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Implementations
{
    public class BandeiraTarifariaService : IBandeiraTarifariaService
    {
        private readonly IBandeiraTarifariaRepository _bandeiraRepository;
        private readonly IMapper _mapper;

        public BandeiraTarifariaService(IBandeiraTarifariaRepository bandeiraRepository, IMapper mapper)
        {
            _bandeiraRepository = bandeiraRepository;
            _mapper = mapper;
        }

        public async Task<BandeiraTarifariaDTO> CreateBandeiraTarifariaAsync(BandeiraTarifariaDTO bandeiraDTO)
        {
            var bandeira = _mapper.Map<BandeiraTarifaria>(bandeiraDTO);
            await _bandeiraRepository.AddAsync(bandeira);
            return _mapper.Map<BandeiraTarifariaDTO>(bandeira);
        }

        public async Task DeleteBandeiraTarifariaAsync(long id)
        {
            await _bandeiraRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BandeiraTarifariaDTO>> GetAllBandeirasTarifariasAsync()
        {
            var bandeiras = await _bandeiraRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BandeiraTarifariaDTO>>(bandeiras);
        }

        public async Task<BandeiraTarifariaDTO> GetBandeiraTarifariaByIdAsync(long id)
        {
            var bandeira = await _bandeiraRepository.GetByIdAsync(id);
            return _mapper.Map<BandeiraTarifariaDTO>(bandeira);
        }

        public async Task<IEnumerable<BandeiraTarifariaDTO>> GetBandeirasByDataVigenciaAsync(DateTime dataVigencia)
        {
            var bandeiras = await _bandeiraRepository.GetByDataVigenciaAsync(dataVigencia);
            return _mapper.Map<IEnumerable<BandeiraTarifariaDTO>>(bandeiras);
        }

        public async Task<BandeiraTarifariaDTO> GetLatestBandeiraTarifariaAsync()
        {
            var bandeira = await _bandeiraRepository.GetLatestAsync();
            return _mapper.Map<BandeiraTarifariaDTO>(bandeira);
        }

        public async Task UpdateBandeiraTarifariaAsync(long id, BandeiraTarifariaDTO bandeiraDTO)
        {
            var bandeiraExistente = await _bandeiraRepository.GetByIdAsync(id);
            if (bandeiraExistente != null)
            {
                _mapper.Map(bandeiraDTO, bandeiraExistente);
                await _bandeiraRepository.UpdateAsync(bandeiraExistente);
            }
            else
            {
                // Opcional: lançar exceção ou retornar um resultado indicando que a bandeira não foi encontrada
                // throw new NotFoundException($"Bandeira tarifária com ID {id} não foi encontrada.");
            }
        }
    }
}
