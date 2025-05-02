using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;

namespace PedidoProdutoCliente.Application.ServicesInterfaces.PedidoServicesInterfaces
{
    public interface IPedidoAdicionarService
    {
        Task<BaseResponse<bool>> Process(PedidoRequest.AdicionarPedidoRequest request);
    }
}
