using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;

namespace PedidoProdutoCliente.Application.ServicesInterfaces.ClienteServicesInterfaces
{
    public interface IClienteAdicionarService
    {
        Task<BaseResponse<bool>> Process(ClienteRequest.AdicionarClienteRequest request);
    }
}
