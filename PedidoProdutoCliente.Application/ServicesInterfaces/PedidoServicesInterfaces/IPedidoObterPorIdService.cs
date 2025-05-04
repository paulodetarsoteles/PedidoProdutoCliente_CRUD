using PedidoProdutoCliente.Application.Models.Responses;
using PedidoProdutoCliente.Domain.Models;

namespace PedidoProdutoCliente.Application.ServicesInterfaces.PedidoServicesInterfaces
{
    public interface IPedidoObterPorIdService
    {
        Task<BaseResponse<Pedido>> Process(int id);
    }
}
