using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces
{
    public interface IProdutoBuscarPorNomeService
    {
        Task<BaseResponse<List<Produto>>> Process(string nome);
    }
}
