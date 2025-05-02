using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;

namespace PedidoProdutoCliente.Application.ServicesInterfaces.ClienteServicesInterfaces
{
    public interface IClienteAtualizarService
    {
        Task<BaseResponse<bool>> Process(ClienteRequest.Atualizar request);
    }
}
