using Moq;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.Entities;
using ProspEco.Repository;
using ProspEco.Service;

namespace ProspEco.Test
{
    public class AparelhoServiceTests
    {
        private readonly Mock<IRepository<Aparelho>> _aparelhoRepositoryMock;
        private readonly AparelhoService _aparelhoService;

        public AparelhoServiceTests()
        {
            _aparelhoRepositoryMock = new Mock<IRepository<Aparelho>>();
            _aparelhoService = new AparelhoService(_aparelhoRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAparelhoById_ReturnsAparelhoResponse_WhenAparelhoExists()
        {
            // Arrange
            var aparelhoId = 1L;
            var aparelho = new Aparelho
            {
                IdAparelho = aparelhoId,
                DsNome = "Lâmpada",
                DsTipo = "Iluminação",
                VlPotencia = 60,
                IdUsuario = 1,
                DtCriacao = DateTime.UtcNow
            };

            _aparelhoRepositoryMock.Setup(repo => repo.GetByIdAsync(aparelhoId)).ReturnsAsync(aparelho);

            // Act
            var result = await _aparelhoService.GetAparelhoById(aparelhoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(aparelhoId, result.IdAparelho);
            Assert.Equal("Lâmpada", result.Nome);
        }

        [Fact]
        public async Task GetAparelhoById_ThrowsException_WhenAparelhoDoesNotExist()
        {
            // Arrange
            var aparelhoId = 1L;
            _aparelhoRepositoryMock.Setup(repo => repo.GetByIdAsync(aparelhoId)).ReturnsAsync((Aparelho)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _aparelhoService.GetAparelhoById(aparelhoId));
            Assert.Equal("Aparelho não encontrado.", exception.Message);
        }

        [Fact]
        public async Task AddAparelho_CreatesAparelhoSuccessfully()
        {
            // Arrange
            var aparelhoRequest = new AparelhoRequest
            {
                Nome = "Ventilador",
                Tipo = "Climatização",
                Potencia = 75,
                UsuarioId = 1
            };

            _aparelhoRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Aparelho>())).Returns(Task.CompletedTask);

            // Act
            await _aparelhoService.AddAparelho(aparelhoRequest);

            // Assert
            _aparelhoRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Aparelho>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAparelho_UpdatesExistingAparelhoSuccessfully()
        {
            // Arrange
            var aparelhoId = 1L;
            var aparelhoRequest = new AparelhoRequest
            {
                Nome = "Ar Condicionado",
                Tipo = "Climatização",
                Potencia = 120,
                UsuarioId = 1
            };

            var aparelhoExistente = new Aparelho
            {
                IdAparelho = aparelhoId,
                DsNome = "Ventilador",
                DsTipo = "Climatização",
                VlPotencia = 75,
                IdUsuario = 1,
                DtCriacao = DateTime.UtcNow
            };

            _aparelhoRepositoryMock.Setup(repo => repo.GetByIdAsync(aparelhoId)).ReturnsAsync(aparelhoExistente);

            // Act
            await _aparelhoService.UpdateAparelho(aparelhoId, aparelhoRequest);

            // Assert
            _aparelhoRepositoryMock.Verify(repo => repo.UpdateAsync(aparelhoId, It.IsAny<Aparelho>()), Times.Once);
            Assert.Equal("Ar Condicionado", aparelhoExistente.DsNome);
            Assert.Equal("Climatização", aparelhoExistente.DsTipo);
            Assert.Equal(120, aparelhoExistente.VlPotencia);
        }

        [Fact]
        public async Task UpdateAparelho_ThrowsException_WhenAparelhoDoesNotExist()
        {
            // Arrange
            var aparelhoId = 1L;
            var aparelhoRequest = new AparelhoRequest
            {
                Nome = "Ar Condicionado",
                Tipo = "Climatização",
                Potencia = 120,
                UsuarioId = 1
            };

            _aparelhoRepositoryMock.Setup(repo => repo.GetByIdAsync(aparelhoId)).ReturnsAsync((Aparelho)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _aparelhoService.UpdateAparelho(aparelhoId, aparelhoRequest));
            Assert.Equal("Aparelho não encontrado.", exception.Message);
        }

        [Fact]
        public async Task DeleteAparelho_CallsDeleteAsync()
        {
            // Arrange
            var aparelhoId = 1L;
            var aparelhoExistente = new Aparelho
            {
                IdAparelho = aparelhoId,
                DsNome = "Ventilador"
            };

            _aparelhoRepositoryMock.Setup(repo => repo.GetByIdAsync(aparelhoId)).ReturnsAsync(aparelhoExistente);
            _aparelhoRepositoryMock.Setup(repo => repo.DeleteAsync(aparelhoId)).Returns(Task.CompletedTask);

            // Act
            await _aparelhoService.DeleteAparelho(aparelhoId);

            // Assert
            _aparelhoRepositoryMock.Verify(repo => repo.DeleteAsync(aparelhoId), Times.Once);
        }

        [Fact]
        public async Task DeleteAparelho_ThrowsException_WhenAparelhoDoesNotExist()
        {
            // Arrange
            var aparelhoId = 1L;
            _aparelhoRepositoryMock.Setup(repo => repo.GetByIdAsync(aparelhoId)).ReturnsAsync((Aparelho)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _aparelhoService.DeleteAparelho(aparelhoId));
            Assert.Equal("Aparelho não encontrado.", exception.Message);
        }
    }
}
