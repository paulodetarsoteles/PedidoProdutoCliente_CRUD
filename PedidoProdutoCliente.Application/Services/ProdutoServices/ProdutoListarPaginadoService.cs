using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoProduto.Application.Services.ProdutoServices
{
    public class ProdutoListarPaginadoService(IProdutoRepository produtoRepository) : IProdutoListarPaginadoService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly List<string> notifications = [];

        public async Task<BaseResponse<List<Produto>>> Process(int page, int pageSize)
        {
            try
            {
                if (ValidaParametros(page, pageSize) == false)
                {
                    return new BaseResponse<List<Produto>>(false, false, notifications);
                }

                var produtos = await _produtoRepository.ListarPaginado(page, pageSize);

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

        private bool ValidaParametros(int page, int pageSize)
        {
            if (page <= 0)
            {
                notifications.Add("Page não pode ser igual ou menor que zero");
            }

            if (pageSize <= 0)
            {
                notifications.Add("PageSize não pode ser igual ou menor que zero");
            }

            if (notifications.Count != 0)
            {
                return false;
            }

            return true;
        }
    }
}
