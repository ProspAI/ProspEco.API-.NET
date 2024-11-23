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
    public class UsuarioServiceTests
    {
        private readonly Mock<IRepository<Usuario>> _usuarioRepositoryMock;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IRepository<Usuario>>();
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUsuarioById_ReturnsResponse_WhenUsuarioExists()
        {
            // Arrange
            var usuarioId = 1L;
            var usuario = new Usuario
            {
                IdUsuario = usuarioId,
                DsEmail = "test@example.com",
                DsNome = "Teste",
                DsRole = "Admin",
                VlPontuacaoEconomia = 100,
                DtCriacao = DateTime.UtcNow
            };

            _usuarioRepositoryMock.Setup(repo => repo.GetByIdAsync(usuarioId)).ReturnsAsync(usuario);

            // Act
            var result = await _usuarioService.GetUsuarioById(usuarioId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuarioId, result.IdUsuario);
            Assert.Equal("test@example.com", result.Email);
        }

        [Fact]
        public async Task GetUsuarioById_ThrowsException_WhenUsuarioDoesNotExist()
        {
            // Arrange
            var usuarioId = 1L;
            _usuarioRepositoryMock.Setup(repo => repo.GetByIdAsync(usuarioId)).ReturnsAsync((Usuario)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _usuarioService.GetUsuarioById(usuarioId));
            Assert.Equal("Usuário não encontrado.", exception.Message);
        }

        [Fact]
        public async Task GetAllUsuarios_ReturnsAllUsuarios()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { IdUsuario = 1, DsEmail = "user1@example.com", DsNome = "User1", DsRole = "Admin", VlPontuacaoEconomia = 50 },
                new Usuario { IdUsuario = 2, DsEmail = "user2@example.com", DsNome = "User2", DsRole = "User", VlPontuacaoEconomia = 30 }
            };

            _usuarioRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(usuarios);

            // Act
            var result = await _usuarioService.GetAllUsuarios();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.Email == "user1@example.com");
            Assert.Contains(result, r => r.Email == "user2@example.com");
        }

        [Fact]
        public async Task AddUsuario_AddsUsuarioSuccessfully()
        {
            // Arrange
            var usuarioRequest = new UsuarioRequest
            {
                Email = "newuser@example.com",
                Nome = "New User",
                Senha = "password123",
                Role = "User"
            };

            _usuarioRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            // Act
            await _usuarioService.AddUsuario(usuarioRequest);

            // Assert
            _usuarioRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Usuario>(u =>
                u.DsEmail == "newuser@example.com" &&
                u.DsNome == "New User" &&
                u.DsSenha == "password123" &&
                u.DsRole == "User"
            )), Times.Once);
        }

        [Fact]
        public async Task UpdateUsuario_UpdatesExistingUsuarioSuccessfully()
        {
            // Arrange
            var usuarioId = 1L;
            var usuarioRequest = new UsuarioRequest
            {
                Email = "updateduser@example.com",
                Nome = "Updated User",
                Senha = "newpassword123",
                Role = "Admin"
            };

            var usuarioExistente = new Usuario
            {
                IdUsuario = usuarioId,
                DsEmail = "olduser@example.com",
                DsNome = "Old User",
                DsSenha = "oldpassword123",
                DsRole = "User"
            };

            _usuarioRepositoryMock.Setup(repo => repo.GetByIdAsync(usuarioId)).ReturnsAsync(usuarioExistente);

            // Act
            await _usuarioService.UpdateUsuario(usuarioId, usuarioRequest);

            // Assert
            _usuarioRepositoryMock.Verify(repo => repo.UpdateAsync(usuarioId, It.IsAny<Usuario>()), Times.Once);
            Assert.Equal("updateduser@example.com", usuarioExistente.DsEmail);
            Assert.Equal("Updated User", usuarioExistente.DsNome);
            Assert.Equal("newpassword123", usuarioExistente.DsSenha);
            Assert.Equal("Admin", usuarioExistente.DsRole);
        }

        [Fact]
        public async Task UpdateUsuario_ThrowsException_WhenUsuarioDoesNotExist()
        {
            // Arrange
            var usuarioId = 1L;
            var usuarioRequest = new UsuarioRequest
            {
                Email = "updateduser@example.com",
                Nome = "Updated User",
                Senha = "newpassword123",
                Role = "Admin"
            };

            _usuarioRepositoryMock.Setup(repo => repo.GetByIdAsync(usuarioId)).ReturnsAsync((Usuario)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _usuarioService.UpdateUsuario(usuarioId, usuarioRequest));
            Assert.Equal("Usuário não encontrado.", exception.Message);
        }

        [Fact]
        public async Task DeleteUsuario_DeletesUsuarioSuccessfully()
        {
            // Arrange
            var usuarioId = 1L;
            var usuarioExistente = new Usuario { IdUsuario = usuarioId };

            _usuarioRepositoryMock.Setup(repo => repo.GetByIdAsync(usuarioId)).ReturnsAsync(usuarioExistente);
            _usuarioRepositoryMock.Setup(repo => repo.DeleteAsync(usuarioId)).Returns(Task.CompletedTask);

            // Act
            await _usuarioService.DeleteUsuario(usuarioId);

            // Assert
            _usuarioRepositoryMock.Verify(repo => repo.DeleteAsync(usuarioId), Times.Once);
        }

        [Fact]
        public async Task DeleteUsuario_ThrowsException_WhenUsuarioDoesNotExist()
        {
            // Arrange
            var usuarioId = 1L;
            _usuarioRepositoryMock.Setup(repo => repo.GetByIdAsync(usuarioId)).ReturnsAsync((Usuario)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _usuarioService.DeleteUsuario(usuarioId));
            Assert.Equal("Usuário não encontrado.", exception.Message);
        }
    }
}
