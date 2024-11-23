using ProspEco.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Interfaces
{
    public interface IRegistroConsumoService
    {
        Task<IEnumerable<RegistroConsumoDTO>> GetAllRegistrosConsumoAsync();
        Task<RegistroConsumoDTO> GetRegistroConsumoByIdAsync(long id);
        Task<IEnumerable<RegistroConsumoDTO>> GetRegistrosConsumoByAparelhoIdAsync(long aparelhoId);
        Task<IEnumerable<RegistroConsumoDTO>> GetRegistrosConsumoByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<RegistroConsumoDTO> CreateRegistroConsumoAsync(RegistroConsumoDTO registroConsumoDTO);
        Task UpdateRegistroConsumoAsync(long id, RegistroConsumoDTO registroConsumoDTO);
        Task DeleteRegistroConsumoAsync(long id);
    }
}