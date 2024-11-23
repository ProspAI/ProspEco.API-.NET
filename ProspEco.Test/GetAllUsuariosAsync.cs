using ProspEco.Model.DTOs;
using ProspEco.Model.Entities;

[Fact]
public async Task GetAllUsuariosAsync_ReturnsListOfUsuarioDTO()
{
    // Arrange
    var usuarios = new List<Usuario>
    {
        new Usuario { Id = 1L, Email = "user1@example.com", Nome = "User One" },
        new Usuario { Id = 2L, Email = "user2@example.com", Nome = "User Two" }
    };
    var usuariosDTO = new List<UsuarioDTO>
    {
        new UsuarioDTO { Id = 1L, Email = "user1@example.com", Nome = "User One" },
        new UsuarioDTO { Id = 2L, Email = "user2@example.com", Nome = "User Two" }
    };

    _usuarioRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(usuarios);
    _mapperMock.Setup(m => m.Map<IEnumerable<UsuarioDTO>>(usuarios)).Returns(usuariosDTO);

    // Act
    var result = await _usuarioService.GetAllUsuariosAsync();

    // Assert
    Assert.NotNull(result);
    Assert.Collection(result,
        item =>
        {
            Assert.Equal(1L, item.Id);
            Assert.Equal("user1@example.com", item.Email);
            Assert.Equal("User One", item.Nome);
        },
        item =>
        {
            Assert.Equal(2L, item.Id);
            Assert.Equal("user2@example.com", item.Email);
            Assert.Equal("User Two", item.Nome);
        });
}