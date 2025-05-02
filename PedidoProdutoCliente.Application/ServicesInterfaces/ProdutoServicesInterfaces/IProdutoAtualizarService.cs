using PedidoProdutoCliente.Application.Models.Requests;
using PedidoProdutoCliente.Application.Models.Responses;

namespace PedidoProdutoCliente.Application.ServicesInterfaces.ProdutoServicesInterfaces
{
    public interface IProdutoAtualizarService
    {
        Task<BaseResponse<bool>> Process(ProdutoRequest.Atualizar request);
    }
}
