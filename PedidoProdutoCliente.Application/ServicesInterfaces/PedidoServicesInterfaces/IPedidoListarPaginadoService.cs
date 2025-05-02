using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.Application.ServicesInterfaces.PedidoServicesInterfaces
{
    public interface IPedidoListarPaginadoService
    {
        Task<BaseResponse<List<Pedido>>> Process(int page, int pageSize);
    }
}
