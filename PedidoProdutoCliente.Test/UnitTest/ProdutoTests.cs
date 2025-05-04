using Moq;
using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Services.ProdutoServices;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Test.UnitTest
{
    public class ProdutoTests
    {
        [Fact]
        public async Task AdicionarProduto_DeveRetornarErro_SeValorInvalido()
        {
            // Arrange
            var produtoRepoMock = new Mock<IProdutoRepository>();

            produtoRepoMock.Setup(r => r.ObterPorId(It.IsAny<int>()))
                .ReturnsAsync(new Produto 
                { 
                    Id = 1, 
                    Nome = "Produto Teste", 
                    Quantidade = 10, 
                    Valor = 100 
                });

            var service = new ProdutoAdicionarService(produtoRepoMock.Object);

            var request = new ProdutoRequest.AdicionarProdutoRequest
            {
                Nome = "Produto Teste",
                Quantidade = 5,
                Valor = -10 
            };

            // Act
            var result = await service.Process(request);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Valor precisa ser maior que zero", result.Messages);
        }

        [Fact]
        public async Task AdicionarProduto_DeveAdicionarProduto_SeParametrosValidos()
        {
            // Arrange
            var produtoRepoMock = new Mock<IProdutoRepository>();

            produtoRepoMock.Setup(r => r.Adicionar(It.IsAny<Produto>()))
                .ReturnsAsync(true);

            var service = new ProdutoAdicionarService(produtoRepoMock.Object);

            var request = new ProdutoRequest.AdicionarProdutoRequest
            {
                Nome = "Produto Teste",
                Quantidade = 5,
                Valor = 100
            };

            // Act
            var result = await service.Process(request);

            // Assert
            Assert.True(result.Success);

            produtoRepoMock.Verify(r => r.Adicionar(It.Is<Produto>(p =>
                p.Nome == "Produto Teste" &&
                p.Quantidade == 5 &&
                p.Valor == 100)), Times.Once);
        }
    }
}
