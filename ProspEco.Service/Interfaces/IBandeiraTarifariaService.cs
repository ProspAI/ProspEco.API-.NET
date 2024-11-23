using ProspEco.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProspEco.Service.Interfaces
{
    public interface IBandeiraTarifariaService
    {
        Task<IEnumerable<BandeiraTarifariaDTO>> GetAllBandeirasTarifariasAsync();
        Task<BandeiraTarifariaDTO> GetBandeiraTarifariaByIdAsync(long id);
        Task<IEnumerable<BandeiraTarifariaDTO>> GetBandeirasByDataVigenciaAsync(DateTime dataVigencia);
        Task<BandeiraTarifariaDTO> GetLatestBandeiraTarifariaAsync();
        Task<BandeiraTarifariaDTO> CreateBandeiraTarifariaAsync(BandeiraTarifariaDTO bandeiraDTO);
        Task UpdateBandeiraTarifariaAsync(long id, BandeiraTarifariaDTO bandeiraDTO);
        Task DeleteBandeiraTarifariaAsync(long id);
    }
}