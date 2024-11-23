using ProspEco.Model.Entities;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Repository;

namespace ProspEco.Service
{
    public class BandeiraTarifariaService : IBandeiraTarifariaService
    {
        private readonly IRepository<BandeiraTarifaria> _bandeiraTarifariaRepository;

        public BandeiraTarifariaService(IRepository<BandeiraTarifaria> bandeiraTarifariaRepository)
        {
            _bandeiraTarifariaRepository = bandeiraTarifariaRepository;
        }

        public async Task AddBandeiraTarifaria(BandeiraTarifariaRequest bandeiraRequest)
        {
            var bandeira = new BandeiraTarifaria
            {
                DtVigencia = bandeiraRequest.DataVigencia,
                DsTipoBandeira = bandeiraRequest.TipoBandeira,
                DtCriacao = DateTime.UtcNow,
                DtModificacao = null
            };

            await _bandeiraTarifariaRepository.AddAsync(bandeira);
        }

        public async Task<IEnumerable<BandeiraTarifariaResponse>> GetAllBandeirasTarifarias()
        {
            var bandeiras = await _bandeiraTarifariaRepository.GetAllAsync();
            return bandeiras.Select(bandeira => new BandeiraTarifariaResponse
            {
                IdBandeira = bandeira.IdBandeira,
                DataVigencia = bandeira.DtVigencia,
                TipoBandeira = bandeira.DsTipoBandeira,
                DtCriacao = bandeira.DtCriacao,
                DtModificacao = bandeira.DtModificacao
            });
        }

        public async Task<BandeiraTarifariaResponse> GetBandeiraTarifariaById(long id)
        {
            var bandeira = await _bandeiraTarifariaRepository.GetByIdAsync(id);
            if (bandeira == null)
            {
                throw new Exception("Bandeira tarifária não encontrada.");
            }

            return new BandeiraTarifariaResponse
            {
                IdBandeira = bandeira.IdBandeira,
                DataVigencia = bandeira.DtVigencia,
                TipoBandeira = bandeira.DsTipoBandeira,
                DtCriacao = bandeira.DtCriacao,
                DtModificacao = bandeira.DtModificacao
            };
        }

        public async Task UpdateBandeiraTarifaria(long id, BandeiraTarifariaRequest bandeiraRequest)
        {
            var bandeiraExistente = await _bandeiraTarifariaRepository.GetByIdAsync(id);
            if (bandeiraExistente == null)
            {
                throw new Exception("Bandeira tarifária não encontrada.");
            }

            bandeiraExistente.DtVigencia = bandeiraRequest.DataVigencia;
            bandeiraExistente.DsTipoBandeira = bandeiraRequest.TipoBandeira;
            bandeiraExistente.DtModificacao = DateTime.UtcNow;

            await _bandeiraTarifariaRepository.UpdateAsync(id, bandeiraExistente);
        }

        public async Task DeleteBandeiraTarifaria(long id)
        {
            var bandeiraExistente = await _bandeiraTarifariaRepository.GetByIdAsync(id);
            if (bandeiraExistente == null)
            {
                throw new Exception("Bandeira tarifária não encontrada.");
            }

            await _bandeiraTarifariaRepository.DeleteAsync(id);
        }
    }
}
