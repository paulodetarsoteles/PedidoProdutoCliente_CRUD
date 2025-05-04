using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces;
using PedidoProdutoCliente.Domain.Models;
using PedidoProdutoCliente.Infrastructure.RepositoryInterfaces;

namespace PedidoProdutoCliente.Application.Services.ProdutoServices
{
    public class ProdutoObterPorIdService(IProdutoRepository produtoRepository) : IProdutoObterPorIdService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<BaseResponse<Produto>> Process(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new BaseResponse<Produto>(false, false, ["O Id precisa ser maior que zero"]);
                }

                var produto = await _produtoRepository.ObterPorId(id);

                if (produto == null)
                {
                    return new BaseResponse<Produto>(false, "Nenhum Produto encontrado.");
                }

                return new BaseResponse<Produto>(produto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
