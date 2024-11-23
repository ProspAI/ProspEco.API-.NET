// ProspEco.Tests/Services/ConquistaServiceTests.cs
using Xunit;
using Moq;
using AutoMapper;
using ProspEco.Service.Implementations;
using ProspEco.Repository.Interfaces;
using ProspEco.Model.Entities;
using ProspEco.Model.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProspEco.Tests.Services
{
    public class ConquistaServiceTests
    {
        private readonly Mock<IConquistaRepository> _conquistaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ConquistaService _conquistaService;

        public ConquistaServiceTests()
        {
            _conquistaRepositoryMock = new Mock<IConquistaRepository>();
            _mapperMock = new Mock<IMapper>();
            _conquistaService = new ConquistaService(_conquistaRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetConquistaByIdAsync_ReturnsConquistaDTO_WhenConquistaExists()
        {
            // Arrange
            var conquistaId = 1L;
            var conquista = new Conquista { Id = conquistaId, Titulo = "Primeira Conquista", Descricao = "Descrição da primeira conquista", DataConquista = System.DateTime.UtcNow, UsuarioId = 1L };
            var conquistaDTO = new ConquistaDTO { Id = conquistaId, Titulo = "Primeira Conquista", Descricao = "Descrição da primeira conquista", DataConquista = conquista.DataConquista, UsuarioId = 1L };

            _conquistaRepositoryMock.Setup(repo => repo.GetByIdAsync(conquistaId)).ReturnsAsync(conquista);
            _mapperMock.Setup(m => m.Map<ConquistaDTO>(conquista)).Returns(conquistaDTO);

            // Act
            var result = await _conquistaService.GetConquistaByIdAsync(conquistaId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(conquistaId, result.Id);
            Assert.Equal("Primeira Conquista", result.Titulo);
        }

        [Fact]
        public async Task GetConquistaByIdAsync_ReturnsNull_WhenConquistaDoesNotExist()
        {
            // Arrange
            var conquistaId = 1L;
            _conquistaRepositoryMock.Setup(repo => repo.GetByIdAsync(conquistaId)).ReturnsAsync((Conquista)null);
            _mapperMock.Setup(m => m.Map<ConquistaDTO>(It.IsAny<Conquista>())).Returns((ConquistaDTO)null);

            // Act
            var result = await _conquistaService.GetConquistaByIdAsync(conquistaId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateConquistaAsync_AddsConquistaAndReturnsConquistaDTO()
        {
            // Arrange
            var conquistaDTO = new ConquistaDTO { Titulo = "Nova Conquista", Descricao = "Descrição da nova conquista", DataConquista = System.DateTime.UtcNow, UsuarioId = 1L };
            var conquista = new Conquista { Titulo = "Nova Conquista", Descricao = "Descrição da nova conquista", DataConquista = conquistaDTO.DataConquista, UsuarioId = 1L };
            var conquistaDTOResult = new ConquistaDTO { Id = 2L, Titulo = "Nova Conquista", Descricao = "Descrição da nova conquista", DataConquista = conquista.DataConquista, UsuarioId = 1L };

            _mapperMock.Setup(m => m.Map<Conquista>(conquistaDTO)).Returns(conquista);
            _conquistaRepositoryMock.Setup(repo => repo.AddAsync(conquista)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<ConquistaDTO>(conquista)).Returns(conquistaDTOResult);

            // Act
            var result = await _conquistaService.CreateConquistaAsync(conquistaDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2L, result.Id);
            Assert.Equal("Nova Conquista", result.Titulo);
        }

        [Fact]
        public async Task UpdateConquistaAsync_UpdatesExistingConquista()
        {
            // Arrange
            var conquistaId = 1L;
            var conquistaDTO = new ConquistaDTO { Id = conquistaId, Titulo = "Conquista Atualizada", Descricao = "Descrição atualizada", DataConquista = System.DateTime.UtcNow.AddDays(-1), UsuarioId = 1L };
            var conquistaExistente = new Conquista { Id = conquistaId, Titulo = "Conquista Antiga", Descricao = "Descrição antiga", DataConquista = System.DateTime.UtcNow, UsuarioId = 1L };

            _conquistaRepositoryMock.Setup(repo => repo.GetByIdAsync(conquistaId)).ReturnsAsync(conquistaExistente);
            _mapperMock.Setup(m => m.Map(conquistaDTO, conquistaExistente)).Callback<ConquistaDTO, Conquista>((dto, entity) =>
            {
                entity.Titulo = dto.Titulo;
                entity.Descricao = dto.Descricao;
                entity.DataConquista = dto.DataConquista;
                entity.UsuarioId = dto.UsuarioId;
            });
            _conquistaRepositoryMock.Setup(repo => repo.UpdateAsync(conquistaExistente)).Returns(Task.CompletedTask);

            // Act
            await _conquistaService.UpdateConquistaAsync(conquistaId, conquistaDTO);

            // Assert
            _mapperMock.Verify(m => m.Map(conquistaDTO, conquistaExistente), Times.Once);
            _conquistaRepositoryMock.Verify(repo => repo.UpdateAsync(conquistaExistente), Times.Once);
            Assert.Equal("Conquista Atualizada", conquistaExistente.Titulo);
            Assert.Equal("Descrição atualizada", conquistaExistente.Descricao);
        }

        [Fact]
        public async Task UpdateConquistaAsync_DoesNothing_WhenConquistaDoesNotExist()
        {
            // Arrange
            var conquistaId = 1L;
            var conquistaDTO = new ConquistaDTO { Id = conquistaId, Titulo = "Conquista Atualizada", Descricao = "Descrição atualizada", DataConquista = System.DateTime.UtcNow.AddDays(-1), UsuarioId = 1L };

            _conquistaRepositoryMock.Setup(repo => repo.GetByIdAsync(conquistaId)).ReturnsAsync((Conquista)null);
            _mapperMock.Setup(m => m.Map<ConquistaDTO, Conquista>(conquistaDTO, It.IsAny<Conquista>())).Verifiable();

            // Act
            await _conquistaService.UpdateConquistaAsync(conquistaId, conquistaDTO);

            // Assert
            _mapperMock.Verify(m => m.Map<ConquistaDTO, Conquista>(conquistaDTO, It.IsAny<Conquista>()), Times.Never);
            _conquistaRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Conquista>()), Times.Never);
        }
    }
}
