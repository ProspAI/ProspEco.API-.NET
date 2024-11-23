// ProspEco.Tests/Controllers/UsuariosControllerTests.cs
using Xunit;
using Moq;
using AutoMapper;
using ProspEco.API.Controllers;
using ProspEco.Service.Interfaces;
using ProspEco.Model.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProspEco.Tests.Controllers
{
    public class UsuariosControllerTests
    {
        private readonly Mock<IUsuarioService> _usuarioServiceMock;
        private readonly UsuariosController _usuariosController;

        public UsuariosControllerTests()
        {
            _usuarioServiceMock = new Mock<IUsuarioService>();
            _usuariosController = new UsuariosController(_usuarioServiceMock.Object);
        }

        [Fact]
        public async Task GetUsuarioById_ReturnsOkObjectResult_WhenUsuarioExists()
        {
            // Arrange
            var usuarioId = 1L;
            var usuarioDTO = new UsuarioDTO { Id = usuarioId, Email = "user@example.com", Nome = "User Example" };

            _usuarioServiceMock.Setup(service => service.GetUsuarioByIdAsync(usuarioId)).ReturnsAsync(usuarioDTO);

            // Act
            var result = await _usuariosController.GetUsuario(usuarioId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsuario = Assert.IsType<UsuarioDTO>(okResult.Value);
            Assert.Equal(usuarioId, returnedUsuario.Id);
            Assert.Equal("user@example.com", returnedUsuario.Email);
        }

        [Fact]
        public async Task GetUsuarioById_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            // Arrange
            var usuarioId = 1L;
            _usuarioServiceMock.Setup(service => service.GetUsuarioByIdAsync(usuarioId)).ReturnsAsync((UsuarioDTO)null);

            // Act
            var result = await _usuariosController.GetUsuario(usuarioId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal($"Usuário com ID {usuarioId} não encontrado.", ((dynamic)notFoundResult.Value).message);
        }

        [Fact]
        public async Task CreateUsuario_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var usuarioDTO = new UsuarioDTO { Email = "newuser@example.com", Nome = "New User", Senha = "password123", Role = "User" };
            var createdUsuarioDTO = new UsuarioDTO { Id = 2L, Email = "newuser@example.com", Nome = "New User", Senha = "password123", Role = "User" };

            _usuarioServiceMock.Setup(service => service.CreateUsuarioAsync(usuarioDTO)).ReturnsAsync(createdUsuarioDTO);

            // Act
            var result = await _usuariosController.CreateUsuario(usuarioDTO);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedUsuario = Assert.IsType<UsuarioDTO>(createdAtActionResult.Value);
            Assert.Equal(2L, returnedUsuario.Id);
            Assert.Equal("newuser@example.com", returnedUsuario.Email);
        }

        // Adicione mais testes para outros métodos e cenários conforme necessário
    }
}
