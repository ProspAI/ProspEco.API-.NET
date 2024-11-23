// ProspEco.Tests/Services/BandeiraTarifariaServiceTests.cs
using Xunit;
using Moq;
using AutoMapper;
using ProspEco.Service.Implementations;
using ProspEco.Repository.Interfaces;
using ProspEco.Model.Entities;
using ProspEco.Model.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ProspEco.Tests.Services
{
    public class BandeiraTarifariaServiceTests
    {
        private readonly Mock<IBandeiraTarifariaRepository> _bandeiraRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BandeiraTarifariaService _bandeiraService;

        public BandeiraTarifariaServiceTests()
        {
            _bandeiraRepositoryMock = new Mock<IBandeiraTarifariaRepository>();
            _mapperMock = new Mock<IMapper>();
            _bandeiraService = new BandeiraTarifariaService(_bandeiraRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetBandeiraTarifariaByIdAsync_ReturnsBandeiraTarifariaDTO_WhenBandeiraExists()
        {
            // Arrange
            var bandeiraId = 1L;
            var bandeira = new BandeiraTarifaria { Id = bandeiraId, TipoBandeira = "Verde", DataVigencia = DateTime.UtcNow };
            var bandeiraDTO = new BandeiraTarifariaDTO { Id = bandeiraId, TipoBandeira = "Verde", DataVigencia = bandeira.DataVigencia };

            _bandeiraRepositoryMock.Setup(repo => repo.GetByIdAsync(bandeiraId)).ReturnsAsync(bandeira);
            _mapperMock.Setup(m => m.Map<BandeiraTarifariaDTO>(bandeira)).Returns(bandeiraDTO);

            // Act
            var result = await _bandeiraService.GetBandeiraTarifariaByIdAsync(bandeiraId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bandeiraId, result.Id);
            Assert.Equal("Verde", result.TipoBandeira);
        }

        [Fact]
        public async Task GetBandeiraTarifariaByIdAsync_ReturnsNull_WhenBandeiraDoesNotExist()
        {
            // Arrange
            var bandeiraId = 1L;
            _bandeiraRepositoryMock.Setup(repo => repo.GetByIdAsync(bandeiraId)).ReturnsAsync((BandeiraTarifaria)null);
            _mapperMock.Setup(m => m.Map<BandeiraTarifariaDTO>(It.IsAny<BandeiraTarifaria>())).Returns((BandeiraTarifariaDTO)null);

            // Act
            var result = await _bandeiraService.GetBandeiraTarifariaByIdAsync(bandeiraId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateBandeiraTarifariaAsync_AddsBandeiraAndReturnsBandeiraTarifariaDTO()
        {
            // Arrange
            var bandeiraDTO = new BandeiraTarifariaDTO { TipoBandeira = "Amarela", DataVigencia = DateTime.UtcNow };
            var bandeira = new BandeiraTarifaria { TipoBandeira = "Amarela", DataVigencia = bandeiraDTO.DataVigencia };
            var bandeiraDTOResult = new BandeiraTarifariaDTO { Id = 2L, TipoBandeira = "Amarela", DataVigencia = bandeira.DataVigencia };

            _mapperMock.Setup(m => m.Map<BandeiraTarifaria>(bandeiraDTO)).Returns(bandeira);
            _bandeiraRepositoryMock.Setup(repo => repo.AddAsync(bandeira)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<BandeiraTarifariaDTO>(bandeira)).Returns(bandeiraDTOResult);

            // Act
            var result = await _bandeiraService.CreateBandeiraTarifariaAsync(bandeiraDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2L, result.Id);
            Assert.Equal("Amarela", result.TipoBandeira);
        }

        [Fact]
        public async Task UpdateBandeiraTarifariaAsync_UpdatesExistingBandeira()
        {
            // Arrange
            var bandeiraId = 1L;
            var bandeiraDTO = new BandeiraTarifariaDTO { Id = bandeiraId, TipoBandeira = "Vermelha", DataVigencia = DateTime.UtcNow.AddDays(30) };
            var bandeiraExistente = new BandeiraTarifaria { Id = bandeiraId, TipoBandeira = "Verde", DataVigencia = DateTime.UtcNow };

            _bandeiraRepositoryMock.Setup(repo => repo.GetByIdAsync(bandeiraId)).ReturnsAsync(bandeiraExistente);
            _mapperMock.Setup(m => m.Map(bandeiraDTO, bandeiraExistente)).Callback<BandeiraTarifariaDTO, BandeiraTarifaria>((dto, entity) =>
            {
                entity.TipoBandeira = dto.TipoBandeira;
                entity.DataVigencia = dto.DataVigencia;
            });
            _bandeiraRepositoryMock.Setup(repo => repo.UpdateAsync(bandeiraExistente)).Returns(Task.CompletedTask);

            // Act
            await _bandeiraService.UpdateBandeiraTarifariaAsync(bandeiraId, bandeiraDTO);

            // Assert
            _mapperMock.Verify(m => m.Map(bandeiraDTO, bandeiraExistente), Times.Once);
            _bandeiraRepositoryMock.Verify(repo => repo.UpdateAsync(bandeiraExistente), Times.Once);
            Assert.Equal("Vermelha", bandeiraExistente.TipoBandeira);
            Assert.Equal(bandeiraDTO.DataVigencia, bandeiraExistente.DataVigencia);
        }

        [Fact]
        public async Task UpdateBandeiraTarifariaAsync_DoesNothing_WhenBandeiraDoesNotExist()
        {
            // Arrange
            var bandeiraId = 1L;
            var bandeiraDTO = new BandeiraTarifariaDTO { Id = bandeiraId, TipoBandeira = "Vermelha", DataVigencia = DateTime.UtcNow.AddDays(30) };

            _bandeiraRepositoryMock.Setup(repo => repo.GetByIdAsync(bandeiraId)).ReturnsAsync((BandeiraTarifaria)null);
            _mapperMock.Setup(m => m.Map<BandeiraTarifariaDTO, BandeiraTarifaria>(bandeiraDTO, It.IsAny<BandeiraTarifaria>())).Verifiable();

            // Act
            await _bandeiraService.UpdateBandeiraTarifariaAsync(bandeiraId, bandeiraDTO);

            // Assert
            _mapperMock.Verify(m => m.Map<BandeiraTarifariaDTO, BandeiraTarifaria>(bandeiraDTO, It.IsAny<BandeiraTarifaria>()), Times.Never);
            _bandeiraRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<BandeiraTarifaria>()), Times.Never);
        }
    }
}
