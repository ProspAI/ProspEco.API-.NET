// ProspEco.Tests/Services/UsuarioServiceTests.cs
using Xunit;
using Moq;
using AutoMapper;
using ProspEco.Service.Implementations;
using ProspEco.Repository.Interfaces;
using ProspEco.Model.Entities;
using ProspEco.Model.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

public class UsuarioServiceTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UsuarioService _usuarioService;

    public UsuarioServiceTests()
    {
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _mapperMock = new Mock<IMapper>();
        _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetUsuarioByIdAsync_ReturnsUsuarioDTO_WhenUsuarioExists()
    {
        // Arrange
        var usuarioId = 1L;
        var usuario = new Usuario { Id = usuarioId, Email = "test@example.com", Nome = "Test User" };
        var usuarioDTO = new UsuarioDTO { Id = usuarioId, Email = "test@example.com", Nome = "Test User" };

        _usuarioRepositoryMock.Setup(repo => repo.GetByIdAsync(usuarioId)).ReturnsAsync(usuario);
        _mapperMock.Setup(m => m.Map<UsuarioDTO>(usuario)).Returns(usuarioDTO);

        // Act
        var result = await _usuarioService.GetUsuarioByIdAsync(usuarioId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(usuarioId, result.Id);
        Assert.Equal("test@example.com", result.Email);
    }

    [Fact]
    public async Task GetUsuarioByIdAsync_ReturnsNull_WhenUsuarioDoesNotExist()
    {
        // Arrange
        var usuarioId = 1L;
        _usuarioRepositoryMock.Setup(repo => repo.GetByIdAsync(usuarioId)).ReturnsAsync((Usuario)null);

        // Act
        var result = await _usuarioService.GetUsuarioByIdAsync(usuarioId);

        // Assert
        Assert.Null(result);
    }

    // Adicione mais testes para outros métodos e cenários
}