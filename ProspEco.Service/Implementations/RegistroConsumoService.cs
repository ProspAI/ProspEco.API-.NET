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
    public class RegistroConsumoService : IRegistroConsumoService
    {
        private readonly IRegistroConsumoRepository _registroConsumoRepository;
        private readonly IMapper _mapper;

        public RegistroConsumoService(IRegistroConsumoRepository registroConsumoRepository, IMapper mapper)
        {
            _registroConsumoRepository = registroConsumoRepository;
            _mapper = mapper;
        }

        public async Task<RegistroConsumoDTO> CreateRegistroConsumoAsync(RegistroConsumoDTO registroConsumoDTO)
        {
            var registroConsumo = _mapper.Map<RegistroConsumo>(registroConsumoDTO);
            await _registroConsumoRepository.AddAsync(registroConsumo);
            return _mapper.Map<RegistroConsumoDTO>(registroConsumo);
        }

        public async Task DeleteRegistroConsumoAsync(long id)
        {
            await _registroConsumoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RegistroConsumoDTO>> GetAllRegistrosConsumoAsync()
        {
            var registros = await _registroConsumoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RegistroConsumoDTO>>(registros);
        }

        public async Task<RegistroConsumoDTO> GetRegistroConsumoByIdAsync(long id)
        {
            var registro = await _registroConsumoRepository.GetByIdAsync(id);
            return _mapper.Map<RegistroConsumoDTO>(registro);
        }

        public async Task<IEnumerable<RegistroConsumoDTO>> GetRegistrosConsumoByAparelhoIdAsync(long aparelhoId)
        {
            var registros = await _registroConsumoRepository.GetByAparelhoIdAsync(aparelhoId);
            return _mapper.Map<IEnumerable<RegistroConsumoDTO>>(registros);
        }

        public async Task<IEnumerable<RegistroConsumoDTO>> GetRegistrosConsumoByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var registros = await _registroConsumoRepository.GetByDateRangeAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<RegistroConsumoDTO>>(registros);
        }

        public async Task UpdateRegistroConsumoAsync(long id, RegistroConsumoDTO registroConsumoDTO)
        {
            var registroExistente = await _registroConsumoRepository.GetByIdAsync(id);
            if (registroExistente != null)
            {
                _mapper.Map(registroConsumoDTO, registroExistente);
                await _registroConsumoRepository.UpdateAsync(registroExistente);
            }
            else
            {
                // Opcional: lançar exceção ou retornar um resultado indicando que o registro não foi encontrado
                // throw new NotFoundException($"Registro de consumo com ID {id} não foi encontrado.");
            }
        }
    }
}
