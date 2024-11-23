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
    public class ConquistaServiceTests
    {
        private readonly Mock<IRepository<Conquista>> _conquistaRepositoryMock;
        private readonly ConquistaService _conquistaService;

        public ConquistaServiceTests()
        {
            _conquistaRepositoryMock = new Mock<IRepository<Conquista>>();
            _conquistaService = new ConquistaService(_conquistaRepositoryMock.Object);
        }

        [Fact]
        public async Task GetConquistaById_ReturnsResponse_WhenConquistaExists()
        {
            // Arrange
            var conquistaId = 1L;
            var conquista = new Conquista
            {
                IdConquista = conquistaId,
                DsTitulo = "Economia de Energia",
                DsDescricao = "Primeira meta atingida",
                DtConquista = new DateTime(2023, 01, 01),
                IdUsuario = 1,
                DtCriacao = DateTime.UtcNow
            };

            _conquistaRepositoryMock.Setup(repo => repo.GetByIdAsync(conquistaId)).ReturnsAsync(conquista);

            // Act
            var result = await _conquistaService.GetConquistaById(conquistaId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(conquistaId, result.IdConquista);
            Assert.Equal("Economia de Energia", result.Titulo);
        }

        [Fact]
        public async Task GetConquistaById_ThrowsException_WhenConquistaDoesNotExist()
        {
            // Arrange
            var conquistaId = 1L;
            _conquistaRepositoryMock.Setup(repo => repo.GetByIdAsync(conquistaId)).ReturnsAsync((Conquista)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _conquistaService.GetConquistaById(conquistaId));
            Assert.Equal("Conquista não encontrada.", exception.Message);
        }

        [Fact]
        public async Task GetAllConquistas_ReturnsAllConquistas()
        {
            // Arrange
            var conquistas = new List<Conquista>
            {
                new Conquista { IdConquista = 1, DsTitulo = "Economia de Energia", DsDescricao = "Primeira meta atingida", IdUsuario = 1 },
                new Conquista { IdConquista = 2, DsTitulo = "Sustentabilidade", DsDescricao = "Economia de 50 kWh", IdUsuario = 1 }
            };

            _conquistaRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(conquistas);

            // Act
            var result = await _conquistaService.GetAllConquistas();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.Titulo == "Economia de Energia");
            Assert.Contains(result, r => r.Titulo == "Sustentabilidade");
        }

        [Fact]
        public async Task AddConquista_CreatesConquistaSuccessfully()
        {
            // Arrange
            var conquistaRequest = new ConquistaRequest
            {
                DataConquista = new DateTime(2023, 01, 01),
                Descricao = "Primeira meta atingida",
                Titulo = "Economia de Energia",
                UsuarioId = 1
            };

            _conquistaRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Conquista>())).Returns(Task.CompletedTask);

            // Act
            await _conquistaService.AddConquista(conquistaRequest);

            // Assert
            _conquistaRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Conquista>()), Times.Once);
        }

        [Fact]
        public async Task UpdateConquista_UpdatesExistingConquistaSuccessfully()
        {
            // Arrange
            var conquistaId = 1L;
            var conquistaRequest = new ConquistaRequest
            {
                DataConquista = new DateTime(2023, 01, 01),
                Descricao = "Atualizado para economia de 50 kWh",
                Titulo = "Economia Atualizada",
                UsuarioId = 1
            };

            var conquistaExistente = new Conquista
            {
                IdConquista = conquistaId,
                DsTitulo = "Economia de Energia",
                DsDescricao = "Primeira meta atingida",
                DtConquista = new DateTime(2023, 01, 01),
                IdUsuario = 1
            };

            _conquistaRepositoryMock.Setup(repo => repo.GetByIdAsync(conquistaId)).ReturnsAsync(conquistaExistente);

            // Act
            await _conquistaService.UpdateConquista(conquistaId, conquistaRequest);

            // Assert
            _conquistaRepositoryMock.Verify(repo => repo.UpdateAsync(conquistaId, It.IsAny<Conquista>()), Times.Once);
            Assert.Equal("Economia Atualizada", conquistaExistente.DsTitulo);
            Assert.Equal("Atualizado para economia de 50 kWh", conquistaExistente.DsDescricao);
        }

        [Fact]
        public async Task UpdateConquista_ThrowsException_WhenConquistaDoesNotExist()
        {
            // Arrange
            var conquistaId = 1L;
            var conquistaRequest = new ConquistaRequest
            {
                DataConquista = new DateTime(2023, 01, 01),
                Descricao = "Atualizado",
                Titulo = "Atualizado",
                UsuarioId = 1
            };

            _conquistaRepositoryMock.Setup(repo => repo.GetByIdAsync(conquistaId)).ReturnsAsync((Conquista)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _conquistaService.UpdateConquista(conquistaId, conquistaRequest));
            Assert.Equal("Conquista não encontrada.", exception.Message);
        }

        [Fact]
        public async Task DeleteConquista_DeletesConquistaSuccessfully()
        {
            // Arrange
            var conquistaId = 1L;
            var conquistaExistente = new Conquista { IdConquista = conquistaId };

            _conquistaRepositoryMock.Setup(repo => repo.GetByIdAsync(conquistaId)).ReturnsAsync(conquistaExistente);
            _conquistaRepositoryMock.Setup(repo => repo.DeleteAsync(conquistaId)).Returns(Task.CompletedTask);

            // Act
            await _conquistaService.DeleteConquista(conquistaId);

            // Assert
            _conquistaRepositoryMock.Verify(repo => repo.DeleteAsync(conquistaId), Times.Once);
        }

        [Fact]
        public async Task DeleteConquista_ThrowsException_WhenConquistaDoesNotExist()
        {
            // Arrange
            var conquistaId = 1L;
            _conquistaRepositoryMock.Setup(repo => repo.GetByIdAsync(conquistaId)).ReturnsAsync((Conquista)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _conquistaService.DeleteConquista(conquistaId));
            Assert.Equal("Conquista não encontrada.", exception.Message);
        }
    }
}
