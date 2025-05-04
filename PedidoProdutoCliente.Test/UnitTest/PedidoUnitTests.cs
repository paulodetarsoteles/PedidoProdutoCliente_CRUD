using Moq;
using PedidoProdutoCliente.Application.Services.PedidoServices;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;
using static PedidoProdutoCliente.Application.Models.Requests.PedidoRequest;

namespace PedidoProdutoCliente.Test.UnitTest
{
    public class PedidoUnitTests
    {
        [Fact]
        public async Task ValidaParametros_DeveRetornarFalse_SeClienteNaoExistir()
        {
            // Arrange
            var pedidoRequest = new AdicionarPedidoRequest()
            {
                ClienteId = 999,
                PagamentoForma = "Cartão",
                Parcelas = 2,
                ProdutosId = [1]
            };

            var clienteRepoMock = new Mock<IClienteRepository>();

            clienteRepoMock.Setup(r => r.ObterPorId(It.IsAny<int>()))
                .ReturnsAsync((Cliente?)null);

            var pedidoAdicionarService = new PedidoAdicionarService(Mock.Of<IPedidoRepository>(), clienteRepoMock.Object, Mock.Of<IProdutoRepository>());

            // Act
            var response = await pedidoAdicionarService.Process(pedidoRequest);

            // Assert
            Assert.False(response.Success);
            Assert.Contains(response.Messages, m => m.Contains("Cliente inválido"));
        }

        [Fact]
        public async Task Process_DeveRetornarFalse_SeProdutoSemEstoque()
        {
            // Arrange
            var produtoComEstoqueZerado = new Produto { Id = 1, Nome = "Produto A", Quantidade = 0, Valor = 100 };

            var produtoRepoMock = new Mock<IProdutoRepository>();

            produtoRepoMock.Setup(r => r.BuscarProdutosPorId(It.IsAny<List<int>>()))
                           .ReturnsAsync([produtoComEstoqueZerado]);

            var clienteRepoMock = new Mock<IClienteRepository>();

            clienteRepoMock.Setup(r => r.ObterPorId(It.IsAny<int>()))
                           .ReturnsAsync(new Cliente 
                           { 
                               Id = 1, 
                               Nome = "Cliente", 
                               CPF = "123", 
                               Email = "a@a.com", 
                               DataCadastro = DateTime.Now 
                           });

            var pedidoAdicionarService = new PedidoAdicionarService(Mock.Of<IPedidoRepository>(), clienteRepoMock.Object, produtoRepoMock.Object);

            var request = new AdicionarPedidoRequest
            {
                ClienteId = 1,
                PagamentoForma = "PIX",
                Parcelas = 1,
                ProdutosId = new List<int> { 1 }
            };

            // Act
            var response = await pedidoAdicionarService.Process(request);

            // Assert
            Assert.False(response.Success);
            Assert.Contains("não tem em estoque", response.Messages.FirstOrDefault());
        }

        [Fact]
        public async Task AdicionarEntidade_DeveCalcularValoresCorretamente()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new() { Id = 1, Nome = "P1", Valor = 100, Quantidade = 5 },
                new() { Id = 2, Nome = "P2", Valor = 50, Quantidade = 3 }
            };

            var clienteRepoMock = new Mock<IClienteRepository>();

            clienteRepoMock.Setup(r => r.ObterPorId(It.IsAny<int>()))
                           .ReturnsAsync(new Cliente
                           {
                               Id = 1,
                               Nome = "Cliente Teste",
                               CPF = "12345678900",
                               Email = "teste@teste.com",
                               DataCadastro = DateTime.Now
                           });

            var pedidoRepoMock = new Mock<IPedidoRepository>();

            pedidoRepoMock.Setup(r => r.Adicionar(It.IsAny<Pedido>()))
                .ReturnsAsync(true);

            var produtoRepoMock = new Mock<IProdutoRepository>();

            produtoRepoMock.Setup(r => r.Atualizar(It.IsAny<Produto>()))
                .ReturnsAsync(true);

            produtoRepoMock.Setup(r => r.BuscarProdutosPorId(It.IsAny<List<int>>()))
                .ReturnsAsync(produtos); 

            var pedidoAdicionarService = new PedidoAdicionarService(pedidoRepoMock.Object, clienteRepoMock.Object, produtoRepoMock.Object);

            var request = new AdicionarPedidoRequest
            {
                ClienteId = 1,
                PagamentoForma = "Cartão",
                Parcelas = 3,
                ProdutosId = new List<int> { 1, 2 },
            };

            // Act
            var result = await pedidoAdicionarService.Process(request);

            // Assert
            Assert.True(result.Success);

            pedidoRepoMock.Verify(r => r.Adicionar(It.Is<Pedido>(p => p.ValorTotal == 150 && p.ValorParcela == 50)), Times.Once);
        }
    }
}