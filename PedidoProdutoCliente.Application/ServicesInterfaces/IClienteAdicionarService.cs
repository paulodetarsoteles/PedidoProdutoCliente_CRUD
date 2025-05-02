using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;

namespace PedidoProdutoCliente.Application.ServicesInterfaces
{
    public interface IClienteAdicionarService
    {
        Task<BaseResponse<bool>> Process(ClienteRequest.Adicionar request);
    }
}
