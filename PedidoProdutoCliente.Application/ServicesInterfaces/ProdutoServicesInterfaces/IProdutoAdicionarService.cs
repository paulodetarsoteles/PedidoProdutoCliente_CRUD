using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;

namespace PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces
{
    public interface IProdutoAdicionarService
    {
        Task<BaseResponse<bool>> Process(ProdutoRequest.Adicionar request);
    }
}
