// ProspEco.Tests/Services/AparelhoServiceTests.cs
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
    public class AparelhoServiceTests
    {
        private readonly Mock<IAparelhoRepository> _aparelhoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AparelhoService _aparelhoService;

        public AparelhoServiceTests()
        {
            _aparelhoRepositoryMock = new Mock<IAparelhoRepository>();
            _mapperMock = new Mock<IMapper>();
            _aparelhoService = new AparelhoService(_aparelhoRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAparelhoByIdAsync_ReturnsAparelhoDTO_WhenAparelhoExists()
        {
            // Arrange
            var aparelhoId = 1L;
            var aparelho = new Aparelho { Id = aparelhoId, Nome = "Lâmpada", Tipo = "Iluminação", Potencia = 60, UsuarioId = 1 };
            var aparelhoDTO = new AparelhoDTO { Id = aparelhoId, Nome = "Lâmpada", Tipo = "Iluminação", Potencia = 60, UsuarioId = 1 };

            _aparelhoRepositoryMock.Setup(repo => repo.GetByIdAsync(aparelhoId)).ReturnsAsync(aparelho);
            _mapperMock.Setup(m => m.Map<AparelhoDTO>(aparelho)).Returns(aparelhoDTO);

            // Act
            var result = await _aparelhoService.GetAparelhoByIdAsync(aparelhoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(aparelhoId, result.Id);
            Assert.Equal("Lâmpada", result.Nome);
        }

        [Fact]
        public async Task GetAparelhoByIdAsync_ReturnsNull_WhenAparelhoDoesNotExist()
        {
            // Arrange
            var aparelhoId = 1L;
            _aparelhoRepositoryMock.Setup(repo => repo.GetByIdAsync(aparelhoId)).ReturnsAsync((Aparelho)null);
            _mapperMock.Setup(m => m.Map<AparelhoDTO>(It.IsAny<Aparelho>())).Returns((AparelhoDTO)null);

            // Act
            var result = await _aparelhoService.GetAparelhoByIdAsync(aparelhoId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAparelhoAsync_AddsAparelhoAndReturnsAparelhoDTO()
        {
            // Arrange
            var aparelhoDTO = new AparelhoDTO { Nome = "Ventilador", Tipo = "Climatização", Potencia = 75, UsuarioId = 1 };
            var aparelho = new Aparelho { Nome = "Ventilador", Tipo = "Climatização", Potencia = 75, UsuarioId = 1 };
            var aparelhoDTOResult = new AparelhoDTO { Id = 2L, Nome = "Ventilador", Tipo = "Climatização", Potencia = 75, UsuarioId = 1 };

            _mapperMock.Setup(m => m.Map<Aparelho>(aparelhoDTO)).Returns(aparelho);
            _aparelhoRepositoryMock.Setup(repo => repo.AddAsync(aparelho)).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<AparelhoDTO>(aparelho)).Returns(aparelhoDTOResult);

            // Act
            var result = await _aparelhoService.CreateAparelhoAsync(aparelhoDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2L, result.Id);
            Assert.Equal("Ventilador", result.Nome);
        }

        [Fact]
        public async Task UpdateAparelhoAsync_UpdatesExistingAparelho()
        {
            // Arrange
            var aparelhoId = 1L;
            var aparelhoDTO = new AparelhoDTO { Id = aparelhoId, Nome = "Ar Condicionado", Tipo = "Climatização", Potencia = 120, UsuarioId = 1 };
            var aparelhoExistente = new Aparelho { Id = aparelhoId, Nome = "Ventilador", Tipo = "Climatização", Potencia = 75, UsuarioId = 1 };

            _aparelhoRepositoryMock.Setup(repo => repo.GetByIdAsync(aparelhoId)).ReturnsAsync(aparelhoExistente);
            _mapperMock.Setup(m => m.Map(aparelhoDTO, aparelhoExistente)).Callback<AparelhoDTO, Aparelho>((dto, entity) =>
            {
                entity.Nome = dto.Nome;
                entity.Tipo = dto.Tipo;
                entity.Potencia = dto.Potencia;
                entity.UsuarioId = dto.UsuarioId;
            });
            _aparelhoRepositoryMock.Setup(repo => repo.UpdateAsync(aparelhoExistente)).Returns(Task.CompletedTask);

            // Act
            await _aparelhoService.UpdateAparelhoAsync(aparelhoId, aparelhoDTO);

            // Assert
            _mapperMock.Verify(m => m.Map(aparelhoDTO, aparelhoExistente), Times.Once);
            _aparelhoRepositoryMock.Verify(repo => repo.UpdateAsync(aparelhoExistente), Times.Once);
            Assert.Equal("Ar Condicionado", aparelhoExistente.Nome);
            Assert.Equal("Climatização", aparelhoExistente.Tipo);
            Assert.Equal(120, aparelhoExistente.Potencia);
        }

        [Fact]
        public async Task DeleteAparelhoAsync_CallsDeleteAsync()
        {
            // Arrange
            var aparelhoId = 1L;
            _aparelhoRepositoryMock.Setup(repo => repo.DeleteAsync(aparelhoId)).Returns(Task.CompletedTask);

            // Act
            await _aparelhoService.DeleteAparelhoAsync(aparelhoId);

            // Assert
            _aparelhoRepositoryMock.Verify(repo => repo.DeleteAsync(aparelhoId), Times.Once);
        }

        // Adicione mais testes para outros métodos e cenários conforme necessário
    }
}
