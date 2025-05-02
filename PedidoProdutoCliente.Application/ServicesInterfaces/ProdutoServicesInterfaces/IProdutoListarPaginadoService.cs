using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces
{
    public interface IProdutoListarPaginadoService
    {
        Task<BaseResponse<List<Produto>>> Process(int page, int pageSize);
    }
}
