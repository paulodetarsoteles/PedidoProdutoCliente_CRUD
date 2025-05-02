using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoProduto.Application.Services.ProdutoServices
{
    public class ProdutoBuscarPorNomeService(IProdutoRepository produtoRepository) : IProdutoBuscarPorNomeService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<BaseResponse<List<Produto>>> Process(string nomeRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(nomeRequest))
                {
                    return new BaseResponse<List<Produto>>(false, false, ["Nome inválido ou em branco"]);
                }

                var produtos = await _produtoRepository.BuscarPorNome(nomeRequest);

                if (produtos == null || produtos.Count == 0)
                {
                    return new BaseResponse<List<Produto>>(false, "Nenhum Produto encontrado.");
                }

                return new BaseResponse<List<Produto>>(produtos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
