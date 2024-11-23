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
    public class MetaServiceTests
    {
        private readonly Mock<IRepository<Meta>> _metaRepositoryMock;
        private readonly MetaService _metaService;

        public MetaServiceTests()
        {
            _metaRepositoryMock = new Mock<IRepository<Meta>>();
            _metaService = new MetaService(_metaRepositoryMock.Object);
        }

        [Fact]
        public async Task GetMetaById_ReturnsResponse_WhenMetaExists()
        {
            // Arrange
            var metaId = 1L;
            var meta = new Meta
            {
                IdMeta = metaId,
                FlAtingida = false,
                VlConsumoAlvo = 100,
                DtInicio = new DateTime(2023, 01, 01),
                DtFim = new DateTime(2023, 01, 31),
                IdUsuario = 1,
                DtCriacao = DateTime.UtcNow
            };

            _metaRepositoryMock.Setup(repo => repo.GetByIdAsync(metaId)).ReturnsAsync(meta);

            // Act
            var result = await _metaService.GetMetaById(metaId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(metaId, result.IdMeta);
            Assert.Equal(100, result.ConsumoAlvo);
        }

        [Fact]
        public async Task GetMetaById_ThrowsException_WhenMetaDoesNotExist()
        {
            // Arrange
            var metaId = 1L;
            _metaRepositoryMock.Setup(repo => repo.GetByIdAsync(metaId)).ReturnsAsync((Meta)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _metaService.GetMetaById(metaId));
            Assert.Equal("Meta não encontrada.", exception.Message);
        }

        [Fact]
        public async Task GetAllMetas_ReturnsAllMetas()
        {
            // Arrange
            var metas = new List<Meta>
            {
                new Meta { IdMeta = 1, VlConsumoAlvo = 100, FlAtingida = false, DtInicio = new DateTime(2023, 01, 01), DtFim = new DateTime(2023, 01, 31) },
                new Meta { IdMeta = 2, VlConsumoAlvo = 200, FlAtingida = true, DtInicio = new DateTime(2023, 02, 01), DtFim = new DateTime(2023, 02, 28) }
            };

            _metaRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(metas);

            // Act
            var result = await _metaService.GetAllMetas();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.ConsumoAlvo == 100);
            Assert.Contains(result, r => r.ConsumoAlvo == 200);
        }

        [Fact]
        public async Task AddMeta_CreatesMetaSuccessfully()
        {
            // Arrange
            var metaRequest = new MetaRequest
            {
                Atingida = false,
                ConsumoAlvo = 100,
                DataInicio = new DateTime(2023, 01, 01),
                DataFim = new DateTime(2023, 01, 31),
                UsuarioId = 1
            };

            _metaRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Meta>())).Returns(Task.CompletedTask);

            // Act
            await _metaService.AddMeta(metaRequest);

            // Assert
            _metaRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Meta>()), Times.Once);
        }

        [Fact]
        public async Task UpdateMeta_UpdatesExistingMetaSuccessfully()
        {
            // Arrange
            var metaId = 1L;
            var metaRequest = new MetaRequest
            {
                Atingida = true,
                ConsumoAlvo = 150,
                DataInicio = new DateTime(2023, 01, 01),
                DataFim = new DateTime(2023, 01, 31),
                UsuarioId = 1
            };

            var metaExistente = new Meta
            {
                IdMeta = metaId,
                FlAtingida = false,
                VlConsumoAlvo = 100,
                DtInicio = new DateTime(2023, 01, 01),
                DtFim = new DateTime(2023, 01, 31),
                IdUsuario = 1
            };

            _metaRepositoryMock.Setup(repo => repo.GetByIdAsync(metaId)).ReturnsAsync(metaExistente);

            // Act
            await _metaService.UpdateMeta(metaId, metaRequest);

            // Assert
            _metaRepositoryMock.Verify(repo => repo.UpdateAsync(metaId, It.IsAny<Meta>()), Times.Once);
            Assert.True(metaExistente.FlAtingida);
            Assert.Equal(150, metaExistente.VlConsumoAlvo);
        }

        [Fact]
        public async Task UpdateMeta_ThrowsException_WhenMetaDoesNotExist()
        {
            // Arrange
            var metaId = 1L;
            var metaRequest = new MetaRequest
            {
                Atingida = true,
                ConsumoAlvo = 150,
                DataInicio = new DateTime(2023, 01, 01),
                DataFim = new DateTime(2023, 01, 31),
                UsuarioId = 1
            };

            _metaRepositoryMock.Setup(repo => repo.GetByIdAsync(metaId)).ReturnsAsync((Meta)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _metaService.UpdateMeta(metaId, metaRequest));
            Assert.Equal("Meta não encontrada.", exception.Message);
        }

        [Fact]
        public async Task DeleteMeta_DeletesMetaSuccessfully()
        {
            // Arrange
            var metaId = 1L;
            var metaExistente = new Meta { IdMeta = metaId };

            _metaRepositoryMock.Setup(repo => repo.GetByIdAsync(metaId)).ReturnsAsync(metaExistente);
            _metaRepositoryMock.Setup(repo => repo.DeleteAsync(metaId)).Returns(Task.CompletedTask);

            // Act
            await _metaService.DeleteMeta(metaId);

            // Assert
            _metaRepositoryMock.Verify(repo => repo.DeleteAsync(metaId), Times.Once);
        }

        [Fact]
        public async Task DeleteMeta_ThrowsException_WhenMetaDoesNotExist()
        {
            // Arrange
            var metaId = 1L;
            _metaRepositoryMock.Setup(repo => repo.GetByIdAsync(metaId)).ReturnsAsync((Meta)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _metaService.DeleteMeta(metaId));
            Assert.Equal("Meta não encontrada.", exception.Message);
        }
    }
}
