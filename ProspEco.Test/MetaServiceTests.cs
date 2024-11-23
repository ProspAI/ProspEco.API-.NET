// ProspEco.Tests/Services/MetaServiceTests.cs
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
    public class MetaServiceTests
    {
        private readonly Mock<IMetaRepository> _metaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MetaService _metaService;

        public MetaServiceTests()
        {
            _metaRepositoryMock = new Mock<IMetaRepository>();
            _mapperMock = new Mock<IMapper>();
            _metaService = new MetaService(_metaRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetMetaByIdAsync_ReturnsMetaDTO_WhenMetaExists()
        {
            // Arrange
            var metaId = 1L;
            var meta = new Meta { Id = metaId, ConsumoAlvo = 100.5, Atingida = false, DataInicio = DateTime.UtcNow.AddMonths(-1), DataFim = DateTime.UtcNow.AddMonths(1), UsuarioId = 1L };
            var metaDTO = new MetaDTO { Id = metaId, ConsumoAlvo = 100.5, Atingida = false, DataInicio = meta.DataInicio, DataFim = meta.DataFim, UsuarioId = 1L };

            _metaRepositoryMock.Setup(repo => repo.GetByIdAsync(metaId)).ReturnsAsync(meta);
            _mapperMock.Setup(m => m.Map<MetaDTO>(meta)).Returns(metaDTO);

            // Act
            var result = await _metaService.GetMetaByIdAsync(metaId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(metaId, result.Id);
            Assert.Equal(100.5, result.ConsumoAlvo);
            Assert.False(result.Atingida);
        }

        [Fact]
        public async Task GetMetaByIdAsync_ReturnsNull_WhenMetaDoesNotExist()
        {
            // Arrange
            var metaId = 1L;
            _metaRepositoryMock.Setup(repo => repo.GetByIdAsync(metaId)).ReturnsAsync((Meta)null);
            _mapperMock.Setup(m => m.Map<MetaDTO>(It.IsAny<Meta>())).Returns((MetaDTO)null);

            // Act
            var result = await _metaService.GetMetaByIdAsync(metaId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateMetaAsync_AddsMetaAndReturnsMetaDTO()
        {
            // Arrange
            var metaDTO = new MetaDTO { ConsumoAlvo = 200.0, Atingida = false, DataInicio = DateTime.UtcNow, DataFim = DateTime.UtcNow.AddMonths(2), UsuarioId = 1L };
            var meta = new Meta { ConsumoAlvo = 200.0, Atingida = false, DataInicio = metaDTO.DataInicio, DataFim = metaDTO.DataFim, UsuarioId = 1L };
            var metaDTOResult = new MetaDTO { Id = 2L, ConsumoAlvo = 200.0, Atingida = false, DataInicio = meta.DataInicio, DataFim = meta.DataFim, UsuarioId = 1L };

            _mapperMock.Setup(m => m.Map<Meta>(metaDTO)).Returns(meta);
            _metaRepositoryMock.Setup(repo => repo.AddAsync(meta)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<MetaDTO>(meta)).Returns(metaDTOResult);

            // Act
            var result = await _metaService.CreateMetaAsync(metaDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2L, result.Id);
            Assert.Equal(200.0, result.ConsumoAlvo);
            Assert.False(result.Atingida);
        }

        [Fact]
        public async Task UpdateMetaAsync_UpdatesExistingMeta()
        {
            // Arrange
            var metaId = 1L;
            var metaDTO = new MetaDTO { Id = metaId, ConsumoAlvo = 150.0, Atingida = true, DataInicio = DateTime.UtcNow.AddMonths(-2), DataFim = DateTime.UtcNow.AddMonths(1), UsuarioId = 1L };
            var metaExistente = new Meta { Id = metaId, ConsumoAlvo = 100.0, Atingida = false, DataInicio = DateTime.UtcNow.AddMonths(-1), DataFim = DateTime.UtcNow.AddMonths(2), UsuarioId = 1L };

            _metaRepositoryMock.Setup(repo => repo.GetByIdAsync(metaId)).ReturnsAsync(metaExistente);
            _mapperMock.Setup(m => m.Map(metaDTO, metaExistente)).Callback<MetaDTO, Meta>((dto, entity) =>
            {
                entity.ConsumoAlvo = dto.ConsumoAlvo;
                entity.Atingida = dto.Atingida;
                entity.DataInicio = dto.DataInicio;
                entity.DataFim = dto.DataFim;
                entity.UsuarioId = dto.UsuarioId;
            });
            _metaRepositoryMock.Setup(repo => repo.UpdateAsync(metaExistente)).Returns(Task.CompletedTask);

            // Act
            await _metaService.UpdateMetaAsync(metaId, metaDTO);

            // Assert
            _mapperMock.Verify(m => m.Map(metaDTO, metaExistente), Times.Once);
            _metaRepositoryMock.Verify(repo => repo.UpdateAsync(metaExistente), Times.Once);
            Assert.Equal(150.0, metaExistente.ConsumoAlvo);
            Assert.True(metaExistente.Atingida);
            Assert.Equal(metaDTO.DataInicio, metaExistente.DataInicio);
            Assert.Equal(metaDTO.DataFim, metaExistente.DataFim);
        }

        [Fact]
        public async Task UpdateMetaAsync_DoesNothing_WhenMetaDoesNotExist()
        {
            // Arrange
            var metaId = 1L;
            var metaDTO = new MetaDTO { Id = metaId, ConsumoAlvo = 150.0, Atingida = true, DataInicio = DateTime.UtcNow.AddMonths(-2), DataFim = DateTime.UtcNow.AddMonths(1), UsuarioId = 1L };

            _metaRepositoryMock.Setup(repo => repo.GetByIdAsync(metaId)).ReturnsAsync((Meta)null);
            _mapperMock.Setup(m => m.Map<MetaDTO, Meta>(metaDTO, It.IsAny<Meta>())).Verifiable();

            // Act
            await _metaService.UpdateMetaAsync(metaId, metaDTO);

            // Assert
            _mapperMock.Verify(m => m.Map<MetaDTO, Meta>(metaDTO, It.IsAny<Meta>()), Times.Never);
            _metaRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Meta>()), Times.Never);
        }
    }
}
