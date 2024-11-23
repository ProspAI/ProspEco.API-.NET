using ProspEco.Model.Entities;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using ProspEco.Repository;

namespace ProspEco.Service
{
    public class RegistroConsumoService : IRegistroConsumoService
    {
        private readonly IRepository<RegistroConsumo> _registroRepository;

        public RegistroConsumoService(IRepository<RegistroConsumo> registroRepository)
        {
            _registroRepository = registroRepository;
        }

        public async Task AddRegistroConsumo(RegistroConsumoRequest registroRequest)
        {
            var registro = new RegistroConsumo
            {
                VlConsumo = registroRequest.Consumo,
                DtHora = registroRequest.DataHora,
                IdAparelho = registroRequest.AparelhoId,
                DtCriacao = DateTime.UtcNow,
                DtModificacao = null
            };

            await _registroRepository.AddAsync(registro);
        }

        public async Task<IEnumerable<RegistroConsumoResponse>> GetAllRegistrosConsumo()
        {
            var registros = await _registroRepository.GetAllAsync();
            return registros.Select(r => new RegistroConsumoResponse
            {
                IdRegistroConsumo = r.IdRegistro,
                Consumo = r.VlConsumo,
                DataHora = r.DtHora,
                IdAparelho = r.IdAparelho,
                DtCriacao = r.DtCriacao,
                DtModificacao = r.DtModificacao
            });
        }

        public async Task<RegistroConsumoResponse> GetRegistroConsumoById(long id)
        {
            var registro = await _registroRepository.GetByIdAsync(id);
            if (registro == null)
            {
                throw new Exception("Registro de consumo não encontrado.");
            }

            return new RegistroConsumoResponse
            {
                IdRegistroConsumo = registro.IdRegistro,
                Consumo = registro.VlConsumo,
                DataHora = registro.DtHora,
                IdAparelho = registro.IdAparelho,
                DtCriacao = registro.DtCriacao,
                DtModificacao = registro.DtModificacao
            };
        }

        public async Task UpdateRegistroConsumo(long id, RegistroConsumoRequest registroRequest)
        {
            var registroExistente = await _registroRepository.GetByIdAsync(id);
            if (registroExistente == null)
            {
                throw new Exception("Registro de consumo não encontrado.");
            }

            registroExistente.VlConsumo = registroRequest.Consumo;
            registroExistente.DtHora = registroRequest.DataHora;
            registroExistente.IdAparelho = registroRequest.AparelhoId;
            registroExistente.DtModificacao = DateTime.UtcNow;

            await _registroRepository.UpdateAsync(id, registroExistente);
        }

        public async Task DeleteRegistroConsumo(long id)
        {
            var registroExistente = await _registroRepository.GetByIdAsync(id);
            if (registroExistente == null)
            {
                throw new Exception("Registro de consumo não encontrado.");
            }

            await _registroRepository.DeleteAsync(id);
        }
    }
}
