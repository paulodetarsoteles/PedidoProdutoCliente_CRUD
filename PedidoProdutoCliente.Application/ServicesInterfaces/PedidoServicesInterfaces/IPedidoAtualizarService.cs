using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;

namespace PedidoProdutoCliente.Application.ServicesInterfaces.PedidoServicesInterfaces
{
    public interface IPedidoAtualizarService
    {
        Task<BaseResponse<bool>> Process(PedidoRequest.AtualizarPedidoRequest request);
    }
}
