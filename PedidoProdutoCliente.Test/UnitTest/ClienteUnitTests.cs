using Moq;
using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Services.ClienteServices;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Test.UnitTest
{
    public class ClienteUnitTests
    {
        [Fact]
        public async Task AdicionarCliente_DeveRetornarErro_SeEmailForInvalido()
        {
            // Arrange
            var clienteRepoMock = new Mock<IClienteRepository>();

            clienteRepoMock.Setup(r => r.ValidaCpfCadastrado(It.IsAny<string>()))
                .ReturnsAsync(false);

            var clienteAdicionarService = new ClienteAdicionarService(clienteRepoMock.Object);

            var request = new ClienteRequest.AdicionarClienteRequest
            {
                Nome = "Teste Email",
                CPF = "12345678900", 
                Email = "email-invalido",
                Telefone = "11999999999",
                Endereco = "Rua A"
            };

            // Act
            var result = await clienteAdicionarService.Process(request);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("E-mail inválido", result.Messages);
        }

        [Fact]
        public async Task AdicionarCliente_DeveAceitarEmailValido()
        {
            // Arrange
            var clienteRepoMock = new Mock<IClienteRepository>();

            clienteRepoMock.Setup(r => r.ValidaCpfCadastrado(It.IsAny<string>()))
                .ReturnsAsync(false);

            clienteRepoMock.Setup(r => r.Adicionar(It.IsAny<Cliente>()))
                .ReturnsAsync(true);

            var clienteAdicionarService = new ClienteAdicionarService(clienteRepoMock.Object);

            var request = new ClienteRequest.AdicionarClienteRequest
            {
                Nome = "Teste Email",
                CPF = "12345678909", 
                Email = "teste@email.com",
                Telefone = "11999999999",
                Endereco = "Rua A"
            };

            // Act
            var result = await clienteAdicionarService.Process(request);

            // Assert
            Assert.True(result.Success);
            Assert.Empty(result.Messages);
        }

        [Fact]
        public async Task AdicionarCliente_DeveRetornarErro_SeCPFForInvalido()
        {
            // Arrange
            var clienteRepoMock = new Mock<IClienteRepository>();

            clienteRepoMock.Setup(r => r.ValidaCpfCadastrado(It.IsAny<string>()))
                .ReturnsAsync(false);

            var clienteAdicionarService = new ClienteAdicionarService(clienteRepoMock.Object);

            var request = new ClienteRequest.AdicionarClienteRequest
            {
                Nome = "Teste CPF",
                CPF = "12345678",
                Email = "teste@teste.com"
            };

            // Act
            var result = await clienteAdicionarService.Process(request);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("CPF inválido", result.Messages);
        }

        [Fact]
        public async Task AdicionarCliente_DeveAceitarCPFValido()
        {
            // Arrange
            var clienteRepoMock = new Mock<IClienteRepository>();

            clienteRepoMock.Setup(r => r.ValidaCpfCadastrado(It.IsAny<string>()))
                .ReturnsAsync(false);

            clienteRepoMock.Setup(r => r.Adicionar(It.IsAny<Cliente>()))
                .ReturnsAsync(true);

            var clienteAdicionarService = new ClienteAdicionarService(clienteRepoMock.Object);

            var request = new ClienteRequest.AdicionarClienteRequest
            {
                Nome = "Teste CPF",
                CPF = "12345678900",
                Email = "teste@email.com"
            };

            // Act
            var result = await clienteAdicionarService.Process(request);

            // Assert
            Assert.True(result.Success);
            Assert.Empty(result.Messages);
        }
    }
}
