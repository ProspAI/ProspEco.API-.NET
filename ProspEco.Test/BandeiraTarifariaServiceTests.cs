using Xunit;
using Moq;
using ProspEco.Service;
using ProspEco.Repository;
using ProspEco.Model.Entities;
using ProspEco.Model.DTO.Request;
using ProspEco.Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProspEco.Tests.Services
{
    public class BandeiraTarifariaServiceTests
    {
        private readonly Mock<IRepository<BandeiraTarifaria>> _bandeiraTarifariaRepositoryMock;
        private readonly BandeiraTarifariaService _bandeiraTarifariaService;

        public BandeiraTarifariaServiceTests()
        {
            _bandeiraTarifariaRepositoryMock = new Mock<IRepository<BandeiraTarifaria>>();
            _bandeiraTarifariaService = new BandeiraTarifariaService(_bandeiraTarifariaRepositoryMock.Object);
        }

        [Fact]
        public async Task GetBandeiraTarifariaById_ReturnsResponse_WhenBandeiraExists()
        {
            // Arrange
            var bandeiraId = 1L;
            var bandeira = new BandeiraTarifaria
            {
                IdBandeira = bandeiraId,
                DtVigencia = new DateTime(2023, 01, 01),
                DsTipoBandeira = "Verde",
                DtCriacao = DateTime.UtcNow
            };

            _bandeiraTarifariaRepositoryMock.Setup(repo => repo.GetByIdAsync(bandeiraId)).ReturnsAsync(bandeira);

            // Act
            var result = await _bandeiraTarifariaService.GetBandeiraTarifariaById(bandeiraId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bandeiraId, result.IdBandeira);
            Assert.Equal("Verde", result.TipoBandeira);
        }

        [Fact]
        public async Task GetBandeiraTarifariaById_ThrowsException_WhenBandeiraDoesNotExist()
        {
            // Arrange
            var bandeiraId = 1L;
            _bandeiraTarifariaRepositoryMock.Setup(repo => repo.GetByIdAsync(bandeiraId)).ReturnsAsync((BandeiraTarifaria)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _bandeiraTarifariaService.GetBandeiraTarifariaById(bandeiraId));
            Assert.Equal("Bandeira tarifária não encontrada.", exception.Message);
        }

        [Fact]
        public async Task GetAllBandeirasTarifarias_ReturnsAllBandeiras()
        {
            // Arrange
            var bandeiras = new List<BandeiraTarifaria>
            {
                new BandeiraTarifaria { IdBandeira = 1, DsTipoBandeira = "Verde", DtVigencia = new DateTime(2023, 01, 01) },
                new BandeiraTarifaria { IdBandeira = 2, DsTipoBandeira = "Amarela", DtVigencia = new DateTime(2023, 02, 01) }
            };

            _bandeiraTarifariaRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bandeiras);

            // Act
            var result = await _bandeiraTarifariaService.GetAllBandeirasTarifarias();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.TipoBandeira == "Verde");
            Assert.Contains(result, r => r.TipoBandeira == "Amarela");
        }

        [Fact]
        public async Task AddBandeiraTarifaria_CreatesBandeiraSuccessfully()
        {
            // Arrange
            var bandeiraRequest = new BandeiraTarifariaRequest
            {
                DataVigencia = new DateTime(2023, 01, 01),
                TipoBandeira = "Verde"
            };

            _bandeiraTarifariaRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<BandeiraTarifaria>())).Returns(Task.CompletedTask);

            // Act
            await _bandeiraTarifariaService.AddBandeiraTarifaria(bandeiraRequest);

            // Assert
            _bandeiraTarifariaRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<BandeiraTarifaria>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBandeiraTarifaria_UpdatesExistingBandeiraSuccessfully()
        {
            // Arrange
            var bandeiraId = 1L;
            var bandeiraRequest = new BandeiraTarifariaRequest
            {
                DataVigencia = new DateTime(2023, 01, 01),
                TipoBandeira = "Amarela"
            };

            var bandeiraExistente = new BandeiraTarifaria
            {
                IdBandeira = bandeiraId,
                DsTipoBandeira = "Verde",
                DtVigencia = new DateTime(2023, 01, 01),
                DtCriacao = DateTime.UtcNow
            };

            _bandeiraTarifariaRepositoryMock.Setup(repo => repo.GetByIdAsync(bandeiraId)).ReturnsAsync(bandeiraExistente);

            // Act
            await _bandeiraTarifariaService.UpdateBandeiraTarifaria(bandeiraId, bandeiraRequest);

            // Assert
            _bandeiraTarifariaRepositoryMock.Verify(repo => repo.UpdateAsync(bandeiraId, It.IsAny<BandeiraTarifaria>()), Times.Once);
            Assert.Equal("Amarela", bandeiraExistente.DsTipoBandeira);
        }

        [Fact]
        public async Task UpdateBandeiraTarifaria_ThrowsException_WhenBandeiraDoesNotExist()
        {
            // Arrange
            var bandeiraId = 1L;
            var bandeiraRequest = new BandeiraTarifariaRequest
            {
                DataVigencia = new DateTime(2023, 01, 01),
                TipoBandeira = "Amarela"
            };

            _bandeiraTarifariaRepositoryMock.Setup(repo => repo.GetByIdAsync(bandeiraId)).ReturnsAsync((BandeiraTarifaria)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _bandeiraTarifariaService.UpdateBandeiraTarifaria(bandeiraId, bandeiraRequest));
            Assert.Equal("Bandeira tarifária não encontrada.", exception.Message);
        }

        [Fact]
        public async Task DeleteBandeiraTarifaria_DeletesBandeiraSuccessfully()
        {
            // Arrange
            var bandeiraId = 1L;
            var bandeiraExistente = new BandeiraTarifaria { IdBandeira = bandeiraId };

            _bandeiraTarifariaRepositoryMock.Setup(repo => repo.GetByIdAsync(bandeiraId)).ReturnsAsync(bandeiraExistente);
            _bandeiraTarifariaRepositoryMock.Setup(repo => repo.DeleteAsync(bandeiraId)).Returns(Task.CompletedTask);

            // Act
            await _bandeiraTarifariaService.DeleteBandeiraTarifaria(bandeiraId);

            // Assert
            _bandeiraTarifariaRepositoryMock.Verify(repo => repo.DeleteAsync(bandeiraId), Times.Once);
        }

        [Fact]
        public async Task DeleteBandeiraTarifaria_ThrowsException_WhenBandeiraDoesNotExist()
        {
            // Arrange
            var bandeiraId = 1L;
            _bandeiraTarifariaRepositoryMock.Setup(repo => repo.GetByIdAsync(bandeiraId)).ReturnsAsync((BandeiraTarifaria)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _bandeiraTarifariaService.DeleteBandeiraTarifaria(bandeiraId));
            Assert.Equal("Bandeira tarifária não encontrada.", exception.Message);
        }
    }
}
